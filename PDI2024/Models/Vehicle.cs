using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PDI2024.Models
{
    public class Vehicle
    {
        [Required(ErrorMessage = "VEHICLEID is required")]
        [DisplayName("User ID")]
        public string VEHICLEID { get; set; }

        [Required(ErrorMessage = "LOCATION is required")]
        [DisplayName("User ID1")]
        public string LOCATION { get; set; }

        //[Required(ErrorMessage = "Column1 is required")]
        [DisplayName("User ID2")]
        public string REMARK { get; set; }

        [DisplayName("User ID3")]
        public string STATUS { get; set; }
    }
}