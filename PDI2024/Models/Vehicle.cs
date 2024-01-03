using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PDI2024.Models
{
    public class Vehicle
    {
        [DisplayName("User ID")]
        public string VEHICLEID { get; set; }

        [DisplayName("User ID1")]
        public string LOCATION { get; set; }

        [DisplayName("User ID2")]
        public string REMARK { get; set; }
    }
}