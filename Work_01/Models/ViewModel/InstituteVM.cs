using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Work_01.Models;

namespace Work_01.Models.ViewModel
{
    public class InstituteVM
    {
        public int InstituteId { get; set; }

        [Required, StringLength(50, ErrorMessage = "The field is required!!"), Display(Name = "Institute Name")]
        public string InstituteName { get; set; }

        public double Cost { get; set; }
    }
}