using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Work_01.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public int Roll { get; set; }
        [Required, StringLength(50, ErrorMessage = "The field is required!!"), Display(Name = "Student Name")]
        
        public string StudentName { get; set; }
        [Required, Display(Name = "StudentDob"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StudentDob { get; set; }
        [Required, StringLength(14, ErrorMessage = "The field is required!!")]
        public string phone { get; set; }
        [Required, StringLength(40, ErrorMessage = "The field is required!!")]
        public string email { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
        [ForeignKey("Department"), Display(Name ="Department")]

        public int DepartmentId { get; set; }
        [ForeignKey("Institute"), Display(Name ="Institute")]
        public int InstituteId { get; set; }

        public bool isActive { get; set; }
        //nav
        public virtual Department Department { get; set; }
        public virtual Institute Institute { get; set; }
    }

    public class StudentDbContext : DbContext 
    {
        //public StudentDbContext() 
        //{
        //    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StudentDbContext>());
        //}
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<InstituteCost> InstituteCosts { get; set; }
    }
 
}