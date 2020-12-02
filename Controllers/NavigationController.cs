using System;
using Microsoft.AspNetCore.Mvc;
using SAEBRecommender.Models;

namespace SAEBRecommender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController : ControllerBase
    {
        [HttpGet(nameof(NextBest))]
        public ActionResult<string> NextBest(string currentUrl)
        {
            if (string.IsNullOrEmpty(currentUrl) 
                || !Uri.TryCreate(currentUrl, UriKind.Absolute, out _)) 
                return BadRequest(nameof(currentUrl));
            
            var siteDetails = new SiteDetails { Url = currentUrl };
            siteDetails.LoadDetails();

            return Ok(siteDetails);
        }
    }
}
