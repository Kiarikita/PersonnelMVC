using PersonnelMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonnelMVC.Controllers
{
    

    public class BranchesController : Controller
    {
        // GET: Branches
        StudentDBEntities db = new StudentDBEntities();

        [Authorize(Roles = "M, A, D")]

        public ActionResult Index()
        {
            var model = db.Branches.ToList();
            return View(model);
        }
        [Authorize(Roles = "A")]

        public ActionResult Update(int id)
        {
            var model = db.Branches.Find(id);
            if (model == null)
                return HttpNotFound();

            return View("Manager", model);
        }

        [Authorize(Roles = "A")]

        public ActionResult Delete(int id)
        {
            var model = db.Branches.Find(id);
            if (model == null)
                return HttpNotFound();

            db.Branches.Remove(model);
            db.SaveChanges();

            return RedirectToAction("Index", "Branches");
        }

        [Authorize(Roles = "A")]

        [HttpGet]

        public ActionResult Create()
        {
            return View("Manager", new Branch());
        }

        [ValidateAntiForgeryToken]

        public ActionResult Save(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return View("Manager");
            }

            if (branch.ID == 0)
            {
                db.Branches.Add(branch);
            }
            else
            {

                var model = db.Branches.Find(branch.ID);
                if (model == null)
                    return HttpNotFound();
                model.Name = branch.Name;
            }

            //db.Branches.Add(branch);
            db.SaveChanges();
            return RedirectToAction("Index", "Branches");
        }
    }
}