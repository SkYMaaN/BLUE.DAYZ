using Steamworks.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace BLUE.SHARED.Models
{
    public class DayZServer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public /*u*/long SteamId { get; set; }

        public string Name { get; set; }

        public int Ping { get; set; }

        public string GameDir { get; set; }

        public string Map { get; set; }

        public string Description { get; set; }

        public uint AppId { get; set; }

        public int Players { get; set; }

        public int MaxPlayers { get; set; }

        public int BotPlayers { get; set; }

        public bool Passworded { get; set; }

        public bool Secure { get; set; }

        public uint LastTimePlayed { get; set; }

        public int Version { get; set; }

        public string TagString { get; set; }

        public uint AddressRaw { get; set; }

        public int ConnectionPort { get; set; }

        public int QueryPort { get; set; }

        //Custom Fields
        public bool IsAdvertised { get; set; }

        public int AdRating { get; set; }

        public int AdImpressionsCount { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsHidden { get; set; }

        public DayZServer(ServerInfo serverInfo)
        {
            SteamId = (long)serverInfo.SteamId;
            Name = serverInfo.Name;
            Ping = serverInfo.Ping;
            GameDir = serverInfo.GameDir;
            Map = serverInfo.Map;
            Description = serverInfo.Description;
            AppId = serverInfo.AppId;
            Players = serverInfo.Players;
            MaxPlayers = serverInfo.MaxPlayers;
            BotPlayers = serverInfo.BotPlayers;
            Passworded = serverInfo.Passworded;
            Secure = serverInfo.Secure;
            LastTimePlayed = serverInfo.LastTimePlayed;
            Version = serverInfo.Version;
            TagString = serverInfo.TagString;
            AddressRaw = serverInfo.AddressRaw;
            ConnectionPort = serverInfo.ConnectionPort;
            QueryPort = serverInfo.QueryPort;

            IsAdvertised = false;
            AdRating = -1;
            AdImpressionsCount = 0;
            IsBlocked = false;
            IsHidden = false;
        }

        public DayZServer()
        {
            IsAdvertised = false;
            AdRating = -1;
            AdImpressionsCount = 0;
            IsBlocked = false;
            IsHidden = false;
        }

        public void MergeFrom(ServerInfo serverInfo)
        {
            this.Name = serverInfo.Name;
            this.Ping = serverInfo.Ping;
            this.GameDir = serverInfo.GameDir;
            this.Map = serverInfo.Map;
            this.Description = serverInfo.Description;
            this.AppId = serverInfo.AppId;
            this.Players = serverInfo.Players;
            this.MaxPlayers = serverInfo.MaxPlayers;
            this.BotPlayers = serverInfo.BotPlayers;
            this.Passworded = serverInfo.Passworded;
            this.Secure = serverInfo.Secure;
            this.LastTimePlayed = serverInfo.LastTimePlayed;
            this.Version = serverInfo.Version;
            this.TagString = serverInfo.TagString;
            this.AddressRaw = serverInfo.AddressRaw;
            this.ConnectionPort = serverInfo.ConnectionPort;
            this.QueryPort = serverInfo.QueryPort;
        }

        /*public void MergeFrom(DayZServer mergeDayZServer)
        {
            this.Name = mergeDayZServer.Name;
            this.Ping = mergeDayZServer.Ping;
            this.GameDir = mergeDayZServer.GameDir;
            this.Map = mergeDayZServer.Map;
            this.Description = mergeDayZServer.Description;
            this.AppId = mergeDayZServer.AppId;
            this.Players = mergeDayZServer.Players;
            this.MaxPlayers = mergeDayZServer.MaxPlayers;
            this.BotPlayers = mergeDayZServer.BotPlayers;
            this.Passworded = mergeDayZServer.Passworded;
            this.Secure = mergeDayZServer.Secure;
            this.LastTimePlayed = mergeDayZServer.LastTimePlayed;
            this.Version = mergeDayZServer.Version;
            this.TagString = mergeDayZServer.TagString;
            this.AddressRaw = mergeDayZServer.AddressRaw;
            this.ConnectionPort = mergeDayZServer.ConnectionPort;
            this.QueryPort = mergeDayZServer.QueryPort;
        }
        */
    }
}
