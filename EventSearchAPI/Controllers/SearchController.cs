using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EventClassLibrary.Models;
using EventSearchAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        [HttpPost]
        public ActionResult<List<DOEvent>> GetOrdersByUserId([FromBody] JObject data)
        {
            string user_id = "";

            if (data == null)
                return StatusCode(400); // Bad Request

            try
            {
                dynamic parsed = data;
                user_id = parsed.user_id.ToString();
            }
            catch (Exception ex)
            {
                var failedMessage = new
                {
                    is_success = false,
                    results = ex.Message
                };

                return Ok(failedMessage);
            }

            var result = from events in _context.event_collects
                         join param in _context.parameters on events.order_id equals param.order_id
                         where events.user_id == user_id
                         orderby events.event_datetime descending
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
                             event_datetime = events.event_datetime
                         };

            bool isSuccess = false;

            if (result.Count() > 0)
                isSuccess = true;

            var messageFormat = new
            {
                is_sucess = isSuccess,
                results = result
            };

            return Ok(messageFormat);
        }

        [HttpPost("order")]
        public ActionResult<DOEvent> GetOrder([FromBody] JObject data)
        {
            string order_id = "";

            if (data == null)
                return StatusCode(400); // Bad Request

            try
            {
                dynamic parsed = data;
                order_id = parsed.order_id.ToString();
            }
            catch (Exception ex)
            {
                var failedMessage = new
                {
                    is_success = false,
                    results = ex.Message
                };

                return Ok(failedMessage);
            }

            var result = from events in _context.event_collects
                         join param in _context.parameters on events.order_id equals param.order_id
                         where param.order_id == order_id
                         orderby events.event_datetime descending
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
                             event_datetime = events.event_datetime
                         };

            bool isSuccess = false;

            if (result.Count() > 0)
                isSuccess = true;

            var messageFormat = new
            {
                is_sucess = isSuccess,
                results = result
            };

            return Ok(messageFormat);
        }

        [HttpGet("all")]
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
    }
}