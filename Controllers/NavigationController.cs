using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEBRecommender.Models;

namespace SAEBRecommender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController : ControllerBase
    {
        [HttpGet(nameof(NextBest))]
        public async Task<ActionResult<string>> NextBest(string currentUrl)
        {
            Uri validatedUri;
            if (string.IsNullOrEmpty(currentUrl) || !Uri.TryCreate(currentUrl, UriKind.Absolute, out validatedUri)) return BadRequest(nameof(currentUrl));
            
            var siteDetails = new SiteDetails { Url = currentUrl };
            siteDetails.LoadTitle();

            return Ok(siteDetails);
        }
    }
}
