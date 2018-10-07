using System.Collections.Generic;
using System.Xml;

namespace YahooFantasySports.DataModel
{
    public class Team
    {
        public string Key { get; }
        public string Id { get; }
        public string Name { get; }
        public string ManagerName { get; }
        public string ManagerEmail { get; }
        public IReadOnlyCollection<string> PlayerKeys { get; }

        internal static Team Create(XmlNode node, NSMgr nsmgr)
        {
            string key = nsmgr.GetValue(node, "team_key");
            string id = nsmgr.GetValue(node, "team_id");
            string name = nsmgr.GetValue(node, "name");
            string managerName = nsmgr.GetValue(node, "managers", "manager", "nickname");
            string managerEmail = nsmgr.GetValue(node, "managers", "manager", "email");
            List<string> playerKeys = new List<string>();
            foreach (XmlNode player in node.SelectNodes(nsmgr.GetXPath("roster", "players", "player"), nsmgr))
            {
                playerKeys.Add(nsmgr.GetValue(player, "player_key"));
            }

            return new Team(key, id, name, managerName, managerEmail, playerKeys);
        }

        private Team(string key, string id, string name, string managerName, string managerEmail, IReadOnlyCollection<string> playerKeys)
        {
            this.Key = key;
            this.Id = id;
            this.Name = name;
            this.ManagerName = managerName;
            this.ManagerEmail = managerEmail;
            this.PlayerKeys = playerKeys;
        }
    }
}
