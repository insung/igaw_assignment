using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventClassLibrary.Models
{
    public class DOParameter
    {
        [Key]
        public string order_id { get; set; }
        public string currency { get; set; }
        public long price { get; set; }
    }
}
