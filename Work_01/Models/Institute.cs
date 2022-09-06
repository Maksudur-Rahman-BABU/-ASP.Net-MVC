using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work_01.Models
{
    public class Institute
    {
        public Institute() 
        {
            this.Students = new List<Student>();
        }
        public int InstituteId { get; set; }

        [Required, StringLength(50, ErrorMessage = "The field is required!!"), Display(Name = "Institute Name")]
        public string InstituteName { get; set; }
        //nav
        public virtual InstituteCost InstituteCost { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }

    public class InstituteCost 
    {
        [Key, ForeignKey("Institute")]
        public int InstituteId { get; set; }
        public double Cost { get; set; }
        public virtual Institute Institute { get; set; }
    }
}