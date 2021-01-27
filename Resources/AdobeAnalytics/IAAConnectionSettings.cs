namespace SAEBRecommender.Resources.AdobeAnalytics
{
    public interface IAAConnectionSettings
    {
        string AAAuthPath { get; set; }
        string AABaseAuthUrl { get; set; }
        string Audience { get; set; }
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        int ExpirySeconds { get; set; }
        string OrganizationId { get; set; }
        string Metascopes { get; set; }
        string TechnicalAccountId { get; set; }
        string PfxPath { get; set; }
        string PfxKeyPass { get; set; }
        string APIBasePath { get; set; }
    }
}