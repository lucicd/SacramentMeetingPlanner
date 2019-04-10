using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SacramentMeetingPlanner.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SacramentMeetingPlanner.Controllers
{
    [Route("api/speaker")]
    public class SpeakersApiController : Controller
    {
        private readonly SacramentMeetingPlannerContext _context;

        public SpeakersApiController(SacramentMeetingPlannerContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string query = HttpContext.Request.Query["term"].ToString();
                var speakerNames = _context.Speakers
                    .Where(s => s.SpeakerName.Contains(query))
                    .Select(s => s.SpeakerName).ToList();

                return Ok(speakerNames);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
