using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventClassLibrary.Models;
using EventSearchAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSearchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly EventDBContext _context;

        public SearchController(EventDBContext context)
        {
            _context = context;
            if (_context.event_collects.Count() == 0)
            {
                _context.event_collects.Add(new DOEvent { event_id = Guid.NewGuid().ToString() });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<DOEvent>> GetAll()
        {
            var result = from events in _context.event_collects
                         join param in _context.parameters on events.order_id equals param.order_id
                         select new
                         {
                             event_id = events.event_id,
                             user_id = events.user_id,
                             event_name = events.event_name,
                             parameters = new
                             {
                                 order_id = param.order_id,
                                 currency = param.currency,
                                 price = param.price
                             },
                             event_datetime = events.event_datetime.ToString("yyyy-MM-dd HH:mm:ss")
                         };
            
            return Ok(result.ToList());
        }

        [HttpGet("orders/{user_id}")]
        public ActionResult<List<DOEvent>> GetOrdersByUserId(string user_id)
        {
            var result = from events in _context.event_collects
                         join param in _context.parameters on events.order_id equals param.order_id
                         where events.user_id == user_id
                         select new
                         {
                             event_id = events.event_id,
                             user_id = events.user_id,
                             event_name = events.event_name,
                             parameters = new
                             {
                                 order_id = param.order_id,
                                 currency = param.currency,
                                 price = param.price
                             },
                             event_datetime = events.event_datetime.ToString("yyyy-MM-dd HH:mm:ss")
                         };

            return Ok(result.ToList());
        }
    }
}