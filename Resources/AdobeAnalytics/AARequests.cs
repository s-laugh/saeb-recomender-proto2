using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SAEBRecommender.Resources.AdobeAnalytics
{
    public class AARequests : IAARequests
    {
        private readonly IAAConnectionSettings settings;
        private static string AuthenticatedJwtToken;

        public AARequests(IOptions<AAConnectionSettings> aaSettings)
        {
            settings = aaSettings.Value;
        }

        public async Task MakeACall_TempAsync()
        {
            EstabilishAAConnection();

            try
            {
                var client = new RestClient(settings.APIBasePath + "/discovery/me");
                var request = new RestRequest(Method.GET);

                request.AddHeader("Accept", "application/json");
                request.AddHeader("x-api-key", settings.ClientId);
                request.AddHeader("Authorization", "Bearer " + AuthenticatedJwtToken);

                IRestResponse response = client.Execute(request);
                _ = response.Content;
            }
            catch
            {
                throw;
            }
        }

        private void EstabilishAAConnection()
        {
            if (AuthenticatedJwtToken != null) return;

            var jwtToken = GenerateJwtToken();
            try
            {
                var client = new RestClient(settings.AABaseAuthUrl + settings.AAAuthPath);

                var request = new RestRequest(Method.POST);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "multipart/form-data; boundary=----boundary");
                request.AddParameter("multipart/form-data; boundary=----boundary",
                    "------boundary\r\nContent-Disposition: form-data; name=\"client_id\"\r\n\r\n" + settings.ClientId +
                    "\r\n------boundary\r\nContent-Disposition: form-data; name=\"client_secret\"\r\n\r\n" + settings.ClientSecret +
                    "\r\n------boundary\r\nContent-Disposition: form-data; name=\"jwt_token\"\r\n\r\n" + jwtToken +
                    "\r\n------boundary--", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                AuthenticatedJwtToken = response.Content;
            }
            catch
            {
                throw;
            }
        }

        private string GenerateJwtToken()
        {
            string token;

            Dictionary<object, object> test = new Dictionary<object, object>
            {
                { "exp", DateTimeOffset.Now.ToUnixTimeSeconds() + settings.ExpirySeconds },
                { "iss", settings.OrganizationId },
                { "sub", settings.TechnicalAccountId },
                { "aud", settings.Audience + settings.ClientId }
            };
            string[] scopes = settings.Metascopes.Split(',');
            foreach (var scope in scopes)
            {
                test.Add(scope, true);
            }

            try
            {
                X509Certificate2 cert = new X509Certificate2(settings.PfxPath, settings.PfxKeyPass);
                token = Jose.JWT.Encode(test, cert.GetRSAPrivateKey(), Jose.JwsAlgorithm.RS256);
            }
            catch
            {
                throw;
            }

            return token;
        }
    }
}
