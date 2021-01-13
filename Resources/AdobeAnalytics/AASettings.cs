using System.Security.Claims;

namespace SAEBRecommender.Resources.AdobeAnalytics
{
    public class AASettings : IAASettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string OrganizationId { get; set; }
        public string TechnicalAccountId { get; set; }
        public string Audience { get; set; }
        public string AABaseAuthUrl { get; set; }
        public string AAAuthPath { get; set; }
        public int ExpiryDays { get; set; }
        public string AuthenticationRequestHeaderType { get; set; }
        public string Metascope { get; set; }        
        public string ApiKey { get; set; }
    }
}
