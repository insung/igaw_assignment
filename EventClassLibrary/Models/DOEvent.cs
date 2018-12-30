using System;
using System.Collections.Generic;
using System.Text;

namespace EventClassLibrary.Models
{
    public class DOEvent
    {
        public string event_id { get; set; }
        public string user_id { get; set; }
        public string event_name { get; set; }
        public DOParameter parameters { get; set; }
    }
}
