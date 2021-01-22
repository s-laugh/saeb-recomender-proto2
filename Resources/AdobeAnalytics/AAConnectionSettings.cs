namespace SAEBRecommender.Resources.AdobeAnalytics
{
    public class AAConnectionSettings : IAAConnectionSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TechnicalAccountId { get; set; }
        public string OrganizationId { get; set; }
        public string Metascopes { get; set; }
        public int ExpirySeconds { get; set; }
        public string Audience { get; set; }
        public string AABaseAuthUrl { get; set; }
        public string AAAuthPath { get; set; }
        public string PfxPath { get; set; }
        public string PfxKeyPass { get; set; }
    }
}
