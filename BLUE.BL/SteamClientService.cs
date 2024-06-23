using Serilog;
using Steamworks;
using System;

namespace BLUE.BL
{
    public static class SteamClientService
    {
        private static readonly ILogger _logger = Log.Logger;

        public const UInt32 DayZAppId = 221100;

        public static void InitializeSteamClient()
        {
            try
            {
                _logger.Debug("Initializing steam client!");

                SteamClient.Init(DayZAppId);

                _logger.Debug($"Steam client initialized for appId {DayZAppId}!");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error connecting to Steam. An error occurred while trying to connect to Steam.");
            }
        }

        public static void ShutdownSteamClient()
        {
            if (SteamClient.State != FriendState.Offline)
            {
                SteamClient.Shutdown();
            }
        }
    }
}
