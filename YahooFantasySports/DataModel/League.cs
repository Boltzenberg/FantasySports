using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace YahooFantasySports.DataModel
{
    public class League
    {
        public static async Task<League> Create(string leagueId)
        {
            HttpWebRequest req = WebRequest.CreateHttp(UrlGen.LeagueSettingsUrl(Constants.Leagues.Rounders2018));
            await AuthManager.Instance.AuthorizeRequestAsync(req);

            using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
            using (StreamReader responseBody = new StreamReader(res.GetResponseStream()))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(await responseBody.ReadToEndAsync());
                NSMgr nsmgr = new NSMgr(doc);

                return new League();
            }
        }

        private League()
        {

        }
    }
}
