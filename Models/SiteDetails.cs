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

        protected internal void LoadDetails()
        {
            var siteDoc = new HtmlWeb().Load(Url);
            Title = ExtractNodeText(siteDoc, "//head/title");
            Description = ExtractNodeText(siteDoc, "//head/meta[@name='description']", "content");
            var keys = ExtractNodeText(siteDoc, "//head/meta[@name='keywords']", "content");
            if (keys != null)
                Keywords = keys.Split(new char[] { ',', ';' });
        }

        private static string ExtractNodeText(HtmlDocument doc, string xPath, string attribute = "")
        {
            string nodeText = null;
            if (doc != null)
            {
                var node = doc.DocumentNode.SelectSingleNode(xPath);
                if (node != null)
                {
                    if (string.IsNullOrWhiteSpace(attribute))
                    {
                        nodeText = node.InnerText;
                    }
                    else
                    {
                        var att = node.Attributes[attribute];
                        if (att != null)
                        {
                            nodeText = att.Value;
                        }
                    }
                }
            }
            return nodeText;
        }
    }
}
