using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventClassLibrary.Models
{
    public class DOEvent
    {
        [Key]
        public string event_id { get; set; }
        public string user_id { get; set; }
        public string event_name { get; set; }
        public string order_id { get; set; }
        public DateTime event_datetime { get; set; }
        public DOParameter parameters { get; set; }
    }
}
