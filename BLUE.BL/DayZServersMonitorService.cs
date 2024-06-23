using Steamworks.Data;
using Steamworks.ServerList;
using Steamworks;
using System;
using Serilog;
using BLUE.DAL;
using BLUE.SHARED.Models;

namespace BLUE.BL
{
    public sealed class DayZServersMonitorService
    {
        public static DayZServersMonitorService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DayZServersMonitorService();
                }

                return _instance;
            }
        }

        private static DayZServersMonitorService _instance;

        private readonly ILogger _logger;

        private readonly Internet _request;

        private readonly DatabaseContext _dbContext;

        private DayZServersMonitorService()
        {
            _logger = Log.Logger;

            _request = new Internet() { AppId = SteamClientService.DayZAppId };

            _dbContext = new DatabaseContext();

            Dispatch.OnDebugCallback = (type, str, server) =>
            {
                _logger.Debug($"[Callback {type} {(server ? "server" : "client")}]");
                _logger.Debug(str);
            };

            Dispatch.OnException = (e) =>
            {
                _logger.Error(e.Message, e.StackTrace);
            };
        }

        public async void StartMonitoring()
        {
            try
            {
                _request.RunQueryAsync();

                _request.OnResponsiveServer += OnResponsiveServer;
            }
            catch (Exception ex)
            {
                _logger.Error("Error while loading DayZ servers from steam!", ex.Message);
            }
        }

        public void StopMonitoring()
        {
            if (_request != null)
            {
                _request.Cancel();
                _request.Dispose();
            }
        }
        private void OnResponsiveServer(ServerInfo serverInfo)
        {
            try
            {
                _logger.Debug("Detected server: " + serverInfo.Name);
                ProcessServer(serverInfo);
                _logger.Debug("Processed server: " + serverInfo.Name);
            }
            catch(Exception ex)
            {
                _logger.Error($"Error while processing DayZ server: [{serverInfo.Name}]!", ex.Message);
            }
        }

        private void ProcessServer(ServerInfo serverInfo)
        {
            var server = new DayZServer(serverInfo);

            _dbContext.AddOrUpdateServer(server);
        }
    }
}
