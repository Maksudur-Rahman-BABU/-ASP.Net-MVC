using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_01.Models;

namespace Work_01.Controllers
{
    public class DepartmentsController : Controller
    {
        StudentDbContext db = new StudentDbContext();
        // GET: Departments
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            

            //if (searchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentFilter;
            //}

            ViewBag.CurrentFilter = searchString;

            var department = from d in db.Departments
                           select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                department = department.Where(x => x.DeptName.ToLower().StartsWith(searchString.ToLower()));

            }
            switch (sortOrder)
            {
                case "name":
                    department = department.OrderBy(s => s.DeptName);
                    break;
            
                    default:  // Name ascending 
                    department = department.OrderByDescending(s => s.DeptName);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(department.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department d)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(d);
                db.SaveChanges();
                return PartialView("_success");
            }
            return PartialView("_error");
        }

        public ActionResult Edit(int? id)
        {
            Department d = db.Departments.Find(id);
            return View(d);
        }
        [HttpPost]
        public ActionResult Edit(Department d)
        {
            if (ModelState.IsValid)
            {
                db.Entry(d).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return PartialView("_success");
            }
            return PartialView("_error");
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Department d = db.Departments.Find(id);

                db.Entry(d).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}