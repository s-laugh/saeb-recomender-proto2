namespace SAEBRecommender.Resources.AdobeAnalytics
{
    public class AASettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string AABaseAuthUrl { get; set; }
        public string AAAuthPath { get; set; }
        public int ExpiryDays { get; set; }
        public string AuthenticationRequestHeaderType { get; set; }
    }
}
