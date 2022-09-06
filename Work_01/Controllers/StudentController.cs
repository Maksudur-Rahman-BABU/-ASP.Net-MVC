using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_01.Models;
using Work_01.Models.ViewModel;

namespace Work_01.Controllers
{
    public class StudentController : Controller
    {
        StudentDbContext db = new StudentDbContext();
        // GET: Student
        public ActionResult Index()
        {
            
            ViewBag.departments = new SelectList(db.Departments, "DepartmentId", "DeptName");
            ViewBag.institutes = new SelectList(db.Institutes, "InstituteId", "InstituteName");
            return View(db.Students.ToList());
        }

        public ActionResult Create() 
        {
            ViewBag.departments = new SelectList(db.Departments, "DepartmentId", "DeptName");
            ViewBag.institutes = new SelectList(db.Institutes, "InstituteId", "InstituteName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentVM vm) 
        {
            //if (ModelState.IsValid) 
            //{
            //    if (vm.Pictures != null) 
            //    {
            //        string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(vm.Pictures.FileName));
            //        vm.Pictures.SaveAs(Server.MapPath(filePath));

            //        Student s = new Student 
            //        {
            //            Roll=vm.Roll,
            //            StudentName=vm.StudentName,
            //            StudentDob=vm.StudentDob,
            //            phone=vm.phone,
            //            DepartmentId=vm.DepartmentId,
            //            InstituteId=vm.InstituteId,
            //            email=vm.email,
            //            Picture=filePath,
            //            isActive=vm.isActive
            //        };

            //        db.Students.Add(s);
            //        db.SaveChanges();
            //        return PartialView("_success");
            //    }
            //}
            using (var context = new StudentDbContext())
            {
                using (DbContextTransaction dbTran = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (vm.Pictures != null)
                            {
                                string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(vm.Pictures.FileName));
                                vm.Pictures.SaveAs(Server.MapPath(filePath));

                                Student s = new Student
                                {
                                    Roll = vm.Roll,
                                    StudentName = vm.StudentName,
                                    StudentDob = vm.StudentDob,
                                    phone = vm.phone,
                                    DepartmentId = vm.DepartmentId,
                                    InstituteId = vm.InstituteId,
                                    email = vm.email,
                                    Picture = filePath,
                                    isActive = vm.isActive
                                };
                                db.Students.Add(s);
                                db.SaveChanges();
                                ViewBag.msg = "Data inserted Successfully";
                                return PartialView("_success");
                            }
                        }
                        dbTran.Commit();
                    }
                    catch (Exception)
                    {
                        dbTran.Rollback();
                        ViewBag.msg = "Data insertion Failed for one or more missing";
                        return PartialView("_error");
                    }
                }
            }
            ViewBag.departments = new SelectList(db.Departments, "DepartmentId", "DeptName");
            ViewBag.institutes = new SelectList(db.Institutes, "InstituteId", "InstituteName");
            return PartialView("_error");
        }

        public ActionResult Edit(int? id) 
        {
            Student s = db.Students.Find(id);
            StudentVM vm = new StudentVM 
            {
                StudentId=s.StudentId,
                Roll = s.Roll,
                StudentName =s.StudentName,
                StudentDob=s.StudentDob,
                phone=s.phone,
                DepartmentId=s.DepartmentId,
                InstituteId=s.InstituteId,
                email=s.email,
                Picture=s.Picture,
                 isActive = s.isActive
            };
            ViewBag.departments = new SelectList(db.Departments, "DepartmentId", "DeptName");
            ViewBag.institutes = new SelectList(db.Institutes, "InstituteId", "InstituteName");
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(StudentVM vm) 
        {
            if (ModelState.IsValid) 
            {
                if (vm.Pictures != null)
                {
                    string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(vm.Pictures.FileName));
                    vm.Pictures.SaveAs(Server.MapPath(filePath));

                    Student s = new Student
                    {
                        StudentId=vm.StudentId,
                        Roll = vm.Roll,
                        StudentName = vm.StudentName,
                        StudentDob = vm.StudentDob,
                        phone = vm.phone,
                        DepartmentId = vm.DepartmentId,
                        InstituteId = vm.InstituteId,
                        email = vm.email,
                        Picture = filePath,
                        isActive = vm.isActive
                    };
                    db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_success");
                }
                else
                {

                    Student s = new Student
                    {
                        
                        StudentId = vm.StudentId,
                        Roll=vm.Roll,
                        StudentName = vm.StudentName,
                        StudentDob = vm.StudentDob,
                        phone = vm.phone,
                        DepartmentId = vm.DepartmentId,
                        InstituteId = vm.InstituteId,
                        email = vm.email,
                        Picture = vm.Picture,
                        isActive=vm.isActive
                    };
                    db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            ViewBag.departments = new SelectList(db.Departments, "DepartmentId", "DeptName");
            ViewBag.institutes = new SelectList(db.Institutes, "InstituteId", "InstituteName");
            return PartialView("_error");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Student s = db.Students.FirstOrDefault(x => x.StudentId == id);
                

                db.Students.Remove(s);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //public ActionResult Report() 
        //{
        //    return View(db.Students.ToList());
        //}

        //// Report
        //public ActionResult ExportReport() 
        //{
        //    List<Student> allStudent = new List<Student>();
        //    allStudent = db.Students.ToList();

        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/CrystalReport"), "rptStudentReport.rpt"));
        //    rd.SetDataSource(allStudent);

        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();

        //    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    return File(stream, "application/pdf", "StudentList.pdf");
        //}
    }
}