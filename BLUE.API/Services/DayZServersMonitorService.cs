using Steamworks.Data;
using Steamworks.ServerList;
using Steamworks;
using System;
using Serilog;
using BLUE.SHARED.Models;
using System.Linq;
using System.Data.Entity.Migrations;

namespace BLUE.API
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

        public void StartMonitoring()
        {
            _logger.Debug("Started monitoring!");

            try
            {
                _request.OnChanges += OnServersChanges;

                _request.RunQueryAsync();

                //var skyfallServers = _request.Responsive.Concat(_request.Unresponsive).Where(s => s.Name.ToLower().Contains("skyfall")).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("Error while loading DayZ servers from steam!", ex.Message);
            }

            _logger.Debug("Finished monitoring!");
        }

        private void OnServersChanges()
        {
            try
            {
                var serversInfo = _request.Responsive.Concat(_request.Unresponsive);

                foreach (var serverInfo in serversInfo)
                {
                    ProcessServer(serverInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DayZ servers!", ex.Message);
            }

            _request.Responsive.Clear();
            _request.Unresponsive.Clear();
        }

        private void ProcessServer(ServerInfo serverInfo)
        {
            try
            {
                DayZServer server = null;

                bool serverExist = _dbContext.IsServerExist(serverInfo.SteamId);

                if (serverExist)
                {
                    server = _dbContext.DayZServers.Find((long)serverInfo.SteamId);
                    server.MergeFrom(serverInfo);
                }
                else
                {
                    server = new DayZServer(serverInfo);
                }

                _dbContext.DayZServers.AddOrUpdate(server);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.Error($"Error while processing DayZ server!", ex.Message);
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
    }
}
