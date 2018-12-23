using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCollectAPI.Models
{
    public class IGAWOrder
    {
        public string order_id { get; set; }
        public string currency { get; set; }
        public long price { get; set; }
    }
}
