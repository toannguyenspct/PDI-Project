using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PDI2024.Models
{
    public class importReceive
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Column1 is required")]
        public string Column1 { get; set; }

        [Required(ErrorMessage = "Column2 is required")]
        public string Column2 { get; set; }


        [Required(ErrorMessage = "Column3 is required")]
        public string Column3 { get; set; }
    }
}