using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Work_01.Models;

namespace Work_01.Models.ViewModel
{
    public class StudentVM
    {
        public int StudentId { get; set; }
        public int Roll { get; set; }
        [Required, StringLength(50, ErrorMessage = "The field is required!!"), Display(Name = "Student Name")]
        public string StudentName { get; set; }
        [Required, Display(Name = "StudentDob"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StudentDob { get; set; }
        [Required, StringLength(14, ErrorMessage = "The field is required!!")]
        public string phone { get; set; }
        [Display(Name ="Department")]
        public int DepartmentId { get; set; }
        [Display(Name ="Institute")]
        public int InstituteId { get; set; }
        [Required, StringLength(40, ErrorMessage = "The field is required!!")]
        public string email { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
        public bool isActive { get; set; }
        public HttpPostedFileBase Pictures { get; set; }
    }
}