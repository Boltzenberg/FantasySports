using FantasySports.DataModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESPNProjections
{
    public class ESPNPlayerData : IPlayerData
    {
        public string DisplayName { get; private set; }

        public FantasySports.DataModels.Constants.StatSource StatSource { get { return FantasySports.DataModels.Constants.StatSource.ESPNProjections; } }

        public string SourceID { get; private set; }

        public List<Position> Positions { get; private set; }

        public Dictionary<FantasySports.DataModels.Constants.StatID, float> Stats { get; private set; }

        public string Outlook { get; private set; }

        public ESPNPlayerData()
        {
            this.DisplayName = string.Empty;
            this.SourceID = string.Empty;
            this.Positions = new List<Position>();
            this.Stats = new Dictionary<FantasySports.DataModels.Constants.StatID, float>();
            this.Outlook = string.Empty;
        }

        public static void LoadProjectionsIntoRoot(Root root)
        {
            List<string> jsons = new List<string>()
            {
                ESPNAPI.LoadBatterProjections(),
                ESPNAPI.LoadPitcherProjections()
            };

            Dictionary<string, IPlayerData> espnPlayerData = new Dictionary<string, IPlayerData>();
            foreach (string json in jsons)
            {
                if (!string.IsNullOrEmpty(json))
                {
                    JObject file = JObject.Parse(json);
                    JArray players = (JArray)file["players"];
                    foreach (JToken player in players)
                    {
                        IPlayerData playerData = ESPNPlayerData.CreateFromData(player);
                        espnPlayerData[playerData.SourceID] = playerData;
                    }
                }
            }

            Dictionary<string, FantasySports.DataModels.Player> dmPlayers = new Dictionary<string, FantasySports.DataModels.Player>();
            foreach (FantasySports.DataModels.Player player in root.Players.Values)
            {
                FantasySports.DataModels.IPlayerData playerData;
                if (player.PlayerData.TryGetValue(FantasySports.DataModels.Constants.StatSource.ESPNProjections, out playerData))
                {
                    dmPlayers[playerData.SourceID] = player;
                }
            }

            foreach (string sourceId in espnPlayerData.Keys)
            {
                FantasySports.DataModels.Player player;
                if (!dmPlayers.TryGetValue(sourceId, out player))
                {
                    IPlayerData espnPlayer = espnPlayerData[sourceId];
                    player = new FantasySports.DataModels.Player(espnPlayer.DisplayName, root.Players.Count);
                    root.Players[player.Id] = player;
                }
                player.PlayerData[FantasySports.DataModels.Constants.StatSource.ESPNProjections] = espnPlayerData[sourceId];
            }
        }

        public static IPlayerData CreateFromData(JToken player)
        {
            ESPNPlayerData stats = new ESPNPlayerData();
            
            stats.DisplayName = (string)player["player"]["fullName"];
            stats.SourceID = (string)player["player"]["id"];
            stats.Outlook = (string)player["player"]["seasonOutlook"];

            HashSet<Position> allPositions = new HashSet<Position>();
            List<int> espnPositions = new List<int>(((JArray)player["player"]["eligibleSlots"]).Select(s => (int)s).ToArray());
            foreach (int espnPosition in espnPositions)
            {
                List<Position> positions;
                if (PositionMapping.TryGetValue(espnPosition, out positions))
                {
                    foreach (Position position in positions)
                    {
                        allPositions.Add(position);
                    }
                }
            }
            stats.Positions = allPositions.ToList();

            JToken statsRoot = player["player"]["stats"];
            if (statsRoot != null && statsRoot.Count() > 0)
            {
                bool foundStats = false;
                foreach (JToken statSet in player["player"]["stats"].Children())
                {
                    foreach (Tuple<FantasySports.DataModels.Constants.StatID, string> stat in RawStatMapping)
                    {
                        string statValueStr = (string)statSet["stats"][stat.Item2];
                        if (!string.IsNullOrEmpty(statValueStr))
                        {
                            float statValue;
                            if (float.TryParse(statValueStr, out statValue))
                            {
                                stats.Stats[stat.Item1] = statValue;
                                foundStats = true;
                            }
                        }
                    }

                    if (foundStats)
                    {
                        break;
                    }
                }
            }

            return stats;
        }

        private static List<Tuple<FantasySports.DataModels.Constants.StatID, string>> RawStatMapping = new List<Tuple<FantasySports.DataModels.Constants.StatID, string>>()
        {
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.B_AtBats, "0"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.B_Hits, "1"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.B_HomeRuns, "5"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.B_Runs, "20"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.B_RunsBattedIn, "21"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.B_StolenBases, "23"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.B_Walks, "10"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.P_EarnedRuns, "45"),
            //new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.P_Hits, "37"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.P_Holds, "60"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.P_Losses, "54"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.P_OutsRecorded, "34"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.P_Saves, "57"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.P_Strikeouts, "48"),
            //new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.P_Walks, "39"),
            new Tuple<FantasySports.DataModels.Constants.StatID, string>(FantasySports.DataModels.Constants.StatID.P_Wins, "53")
        };


        private static Dictionary<int, List<Position>> PositionMapping = new Dictionary<int, List<Position>>()
        {
            { 0, new List<Position>() { Position.C, Position.Util } }, // Catcher
            { 1, new List<Position>() { Position.B1, Position.CI, Position.Util } }, // First Base
            { 2, new List<Position>() { Position.B2, Position.MI, Position.Util } }, // Second Base
            { 3, new List<Position>() { Position.B3, Position.CI, Position.Util } }, // Third Base
            { 4, new List<Position>() { Position.SS, Position.MI, Position.Util } }, // Short Stop
            { 5, new List<Position>() { Position.OF, Position.Util } }, // Outfield
            { 12, new List<Position>() { Position.Util } }, // DH
            { 14, new List<Position>() { Position.SP, Position.P } }, // Starting Pitcher
            { 15, new List<Position>() { Position.RP, Position.P } }, // Relief Pitcher
        };
    }
}
