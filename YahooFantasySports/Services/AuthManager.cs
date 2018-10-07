using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YahooFantasySports.Services
{
    public class AuthManager
    {
        public static AuthManager Instance = new AuthManager();

        private static string RequestAuthUrl = "https://api.login.yahoo.com/oauth2/request_auth";
        private static string GetTokenUrl = "https://api.login.yahoo.com/oauth2/get_token";
        private GetTokenResponse getTokenResponse = null;

        public static Uri GetAuthUrl()
        {
            UriBuilder uriBuilder = new UriBuilder(RequestAuthUrl);
            uriBuilder.Query = string.Format("client_id={0}&redirect_uri=oob&response_type=code&language=en-us", Constants.OAuth.ClientID);
            return uriBuilder.Uri;
        }

        public async Task<bool> InitializeAuthorizationAsync(string authorizationCode)
        {
            if (this.getTokenResponse != null)
            {
                return true;
            }

            this.getTokenResponse = new GetTokenResponse(await GetNewAuthTokenAsync(authorizationCode));
            return this.getTokenResponse != null;
        }

        public async Task AuthorizeRequestAsync(HttpWebRequest req)
        {
            if (this.getTokenResponse.IsExpired)
            {
                this.getTokenResponse = new GetTokenResponse(await RefreshAuthTokenAsync(this.getTokenResponse));
            }

            req.Headers[HttpRequestHeader.Authorization] = "Bearer " + this.getTokenResponse.AccessToken;
        }

        private static async Task<string> GetNewAuthTokenAsync(string authorizationCode)
        { 
            Uri uri = new Uri(GetTokenUrl);

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.Method = "POST";
            byte[] authData = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Constants.OAuth.ClientID, Constants.OAuth.ClientSecret));
            req.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", Convert.ToBase64String(authData));
            req.ContentType = "application/x-www-form-urlencoded";

            using (StreamWriter writer = new StreamWriter(await req.GetRequestStreamAsync()))
            {
                string requestBody = string.Format("grant_type=authorization_code&redirect_uri=oob&code={0}", authorizationCode);
                await writer.WriteAsync(requestBody);
                await writer.FlushAsync();
            }

            string response = string.Empty;
            using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
            using (StreamReader responseBody = new StreamReader(res.GetResponseStream()))
            {
                response = await responseBody.ReadToEndAsync();
            }

            return response;
        }

        private static async Task<string> RefreshAuthTokenAsync(GetTokenResponse oldToken)
        {
            Uri uri = new Uri(GetTokenUrl);

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.Method = "POST";
            byte[] authData = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Constants.OAuth.ClientID, Constants.OAuth.ClientSecret));
            req.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", Convert.ToBase64String(authData));
            req.ContentType = "application/x-www-form-urlencoded";

            using (StreamWriter writer = new StreamWriter(await req.GetRequestStreamAsync()))
            {
                string requestBody = string.Format("grant_type=refresh_token&redirect_uri=oob&refresh_token={0}", oldToken.RefreshToken);
                await writer.WriteAsync(requestBody);
                await writer.FlushAsync();
            }

            string response = string.Empty;
            using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
            using (StreamReader responseBody = new StreamReader(res.GetResponseStream()))
            {
                response = await responseBody.ReadToEndAsync();
            }

            return response;
        }

        private class GetTokenResponse
        {
            private DateTime expires;

            public GetTokenResponse(string response)
            {
                JObject item = JObject.Parse(response);
                this.AccessToken = item.Value<string>("access_token");
                this.TokenType = item.Value<string>("token_type");
                this.RefreshToken = item.Value<string>("refresh_token");
                this.XOAuthYahooGuid = item.Value<string>("xoauth_yahoo_guid");

                int lifetimeInSeconds = item.Value<int>("expires_in");
                this.expires = DateTime.Now.AddSeconds(lifetimeInSeconds);
            }

            public string AccessToken { get; }
            public string TokenType { get; }
            public string RefreshToken { get; }
            public string XOAuthYahooGuid { get; }

            public bool IsExpired
            {
                get
                {
                    return this.expires < DateTime.Now;
                }
            }
        }
    }
}
