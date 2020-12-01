using System;
// using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace SAEBRecommender.Models 
{
    public class SiteDetails
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Keywords { get; set; }

        protected internal void LoadTitle()
        {            
            var webClient = new WebClient();
            var urlSource = webClient.DownloadString(Url);
            Title = Regex.Match(urlSource, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",
                RegexOptions.IgnoreCase).Groups["Title"].Value;                
        }

        protected internal void LoadDetails()
        {
            var web = new HtmlWeb();
            var siteDoc = web.Load(Url);
            Title = siteDoc.DocumentNode.SelectSingleNode("//head/title");
            Description = siteDoc.DocumentNode.SelectNodes("//head/meta");
        }
    }
}
