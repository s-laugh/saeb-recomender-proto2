using System.Security.Claims;

namespace SAEBRecommender.Resources.AdobeAnalytics
{
    public interface IAASettings
    {
        string AAAuthPath { get; set; }
        string AABaseAuthUrl { get; set; }
        string Audience { get; set; }
        string AuthenticationRequestHeaderType { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        int ExpiryDays { get; set; }
        string OrganizationId { get; set; }
        string Metascope { get; set; }
        string TechnicalAccountId { get; set; }
        string ApiKey { get; set; }
    }
}