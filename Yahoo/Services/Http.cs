using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Yahoo.Services
{
    public static class Http
    {
        public static async Task<string> GetRawDataAsync(Uri url)
        {
            // Check the cache first.
            const string CacheDir = "LocalCache";
            string fileName = Path.Combine(CacheDir, url.GetHashCode() + ".txt");
            if (File.Exists(fileName))
            {
                return File.ReadAllText(fileName);
            }

            string data = string.Empty;
            HttpWebRequest req = await Http.GetRequest(url);
            using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
            using (StreamReader responseBody = new StreamReader(res.GetResponseStream()))
            {
                data = await responseBody.ReadToEndAsync();
            }
         
            // Cache the results
            if (!Directory.Exists(CacheDir))
            {
                Directory.CreateDirectory(CacheDir);
            }

            File.WriteAllText(fileName, data);
            return data;
        }

        private static async Task<HttpWebRequest> GetRequest(Uri url)
        {
            HttpWebRequest req = WebRequest.CreateHttp(url);
            await AuthManager.Instance.AuthorizeRequestAsync(req);
            return req;
        }
    }
}
