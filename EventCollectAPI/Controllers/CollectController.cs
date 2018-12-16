using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCollectAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventCollectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectController : ControllerBase
    {
        private readonly IGAWContext _context;

        public CollectController(IGAWContext context)
        {
            _context = context;

            if (_context.IGAWEvents.Count() == 0)
            {
                _context.IGAWEvents.Add(new IGAWEvent { event_id = Guid.NewGuid().ToString() });
                _context.SaveChanges();
            }
        }

        public async Task<ActionResult<IEnumerable<IGAWEvent>>> GetEvents()
        {
            return await _context.IGAWEvents.ToListAsync();
        }

        [HttpGet("{event_id}")]
        public async Task<ActionResult<IGAWEvent>> GetEvent(string event_id)
        {
            var e = await _context.IGAWEvents.FindAsync(event_id);

            if (e == null)
            {
                return NotFound();
            }

            return e;
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent(IGAWEvent e)
        {
            _context.IGAWEvents.Add(e);
            await _context.SaveChangesAsync();

            return Ok(new { is_success = true });
        }
    }
}