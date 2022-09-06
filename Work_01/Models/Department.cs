using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work_01.Models
{
    public class Department
    {
        public Department() 
        {
            this.Students = new List<Student>();
        }
        public int DepartmentId { get; set; }
        [Required, StringLength(50, ErrorMessage = "The field is required!!"), Display(Name = "Depart Name")]
        public string DeptName { get; set; }

        public virtual ICollection<Student> Students { get; set; }

    }
}