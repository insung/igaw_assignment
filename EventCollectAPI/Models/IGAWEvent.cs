using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCollectAPI.Models
{
    public class IGAWEvent
    {
        public string event_id { get; set; }
        public string user_id { get; set; }
        public string event_name { get; set; }
        public IGAWOrder parameters { get; set; }
    }
}
