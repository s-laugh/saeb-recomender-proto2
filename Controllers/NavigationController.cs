using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAEBRecommender.Models;
using SAEBRecommender.Resources.AdobeAnalytics;

namespace SAEBRecommender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController : ControllerBase
    {
        private readonly AARequests aaRequests;

        public NavigationController(AARequests aaRequests)
        {
            this.aaRequests = aaRequests;
        }

        [HttpGet(nameof(NextBest))]
        public ActionResult<string> NextBest(string currentUrl)
        {
            if (string.IsNullOrEmpty(currentUrl) 
                || !Uri.TryCreate(currentUrl, UriKind.Absolute, out _)) 
                return BadRequest(nameof(currentUrl));

            Task qurryAa = aaRequests.MakeACall_TempAsync();

            var siteDetails = new SiteDetails { Url = currentUrl };
            siteDetails.LoadDetails();

            return Ok(siteDetails);
        }
    }
}
