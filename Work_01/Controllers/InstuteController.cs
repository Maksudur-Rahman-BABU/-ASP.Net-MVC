using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Work_01.Models;
using Work_01.Models.ViewModel;

namespace Work_01.Controllers
{
    public class InstuteController : Controller
    {
        StudentDbContext db = new StudentDbContext();
        // GET: Instute

        public ActionResult Index()
        {
            var InstInfo = (
                           from c in db.Institutes
                           join co in db.InstituteCosts on c.InstituteId equals co.InstituteId
                           select new InstituteVM
                           {
                               InstituteId = c.InstituteId,
                               InstituteName = c.InstituteName,
                               Cost = co.Cost

                           }).ToList();


           

            
            return View(InstInfo);


        }

        private ActionResult View(List<InstituteVM> instInfo, IPagedList<InstituteVM> instituteVMs)
        {
            throw new NotImplementedException();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(InstituteVM vm)
        {
            if (ModelState.IsValid)
            {
                Institute i = new Institute
                {
                    InstituteName = vm.InstituteName
                };
                InstituteCost a = new InstituteCost
                {
                    Cost = vm.Cost,
                    Institute = i
                };

                db.InstituteCosts.Add(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var i = db.Institutes.FirstOrDefault(x => x.InstituteId == id);
                var a = db.InstituteCosts.FirstOrDefault(x => x.InstituteId == id);

                InstituteVM vm = new InstituteVM
                {
                    InstituteId = i.InstituteId,
                    InstituteName = i.InstituteName,
                    Cost = a.Cost
                   
                };
                return View(vm);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(InstituteVM vm)
        {
            if (ModelState.IsValid)
            {

                Institute i = new Institute
                {
                    InstituteId = vm.InstituteId,
                    InstituteName = vm.InstituteName
                    
                   
                };
                InstituteCost a = new InstituteCost
                {
                    InstituteId=vm.InstituteId,
                    Cost = vm.Cost,
                    Institute = i
                };

                db.Entry(i).State = System.Data.Entity.EntityState.Modified;
                db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Institute i = db.Institutes.FirstOrDefault(x => x.InstituteId == id);
                InstituteCost a = db.InstituteCosts.FirstOrDefault(x => x.InstituteId == id);

                db.Institutes.Remove(i);
                db.InstituteCosts.Remove(a);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}