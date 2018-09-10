using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace YahooFantasySports.DataModel
{
    public class Player
    {
        public string Key { get; }
        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Name { get { return FirstName + " " + LastName; } }
        public IReadOnlyList<string> Positions { get; }
        public IReadOnlyDictionary<int, string> Stats { get; }

        public static async Task<List<Player>> GetAllPlayers(string leagueId)
        {
            List<Player> players = new List<Player>();
            int start = 1;
            while (true)
            {
                HttpWebRequest req = WebRequest.CreateHttp(UrlGen.PaginatedPlayers(leagueId, start));
                await YahooFantasySports.AuthManager.Instance.AuthorizeRequestAsync(req);

                using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
                using (StreamReader responseBody = new StreamReader(res.GetResponseStream()))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(await responseBody.ReadToEndAsync());
                    NSMgr nsmgr = new NSMgr(doc);

                    XmlNodeList playerRoots = doc.SelectNodes("//" + NSMgr.GetNodeName("player"), nsmgr);
                    if (playerRoots.Count == 0)
                    {
                        break;
                    }

                    start += playerRoots.Count;
                    foreach (XmlNode playerRoot in playerRoots)
                    {
                        players.Add(Player.Create(playerRoot, nsmgr));
                    }
                }
            }

            return players;
        }

        private static Player Create(XmlNode node, NSMgr nsmgr)
        {
            string key = nsmgr.GetValue(node, "player_key");
            string id = nsmgr.GetValue(node, "player_id");

            return new Player(key, id);
        }

        private Player(string key, string id)
        {
            this.Key = key;
            this.Id = id;
        }
    }
}
