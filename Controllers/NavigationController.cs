using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEBRecommender.Models;
using SAEBRecommender.Extensions;

namespace SAEBRecommender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationController : ControllerBase
    {
        [HttpGet(nameof(NextBest))]
        public async Task<ActionResult<string>> NextBest(string currentUrl)
        {
            if (string.IsNullOrEmpty(currentUrl) || !currentUrl.IsValidUri()) return BadRequest(nameof(currentUrl));

            return Ok("Your URL: " + currentUrl);
        }
    }
}
