using PersonnelMVC.Models.EntityFramework;
using PersonnelMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonnelMVC.Controllers
{
    //[Authorize] //class bazında authorization - globale ekledik gerek kalmadı

    

    public class DepartmentsController : Controller
    {
        // GET: Departments
        StudentDBEntities db = new StudentDBEntities();

        [Authorize(Roles = "M, A, D")]

        public ActionResult Index()
        {
            var model = db.Departments.ToList();
            return View(model);
        }

        public ActionResult Update(int id) 
        {
            var model = db.Departments.Find(id);
            if (model == null)
                return HttpNotFound();
            
            return View("Manager", model);
        }

        [Authorize(Roles = "D")]

        public ActionResult Delete(int id)
        {
            var model = db.Departments.Find(id);
            if (model == null)
                return HttpNotFound();

            db.Departments.Remove(model);
            db.SaveChanges();
            
            return RedirectToAction("Index", "Departments");
        }

        [Authorize(Roles = "A")]
        [HttpGet]

        public ActionResult Create() 
        {
            return View("Manager", new Department());
        }

        [ValidateAntiForgeryToken]

        public ActionResult Save(Department department) 
        {
            if (!ModelState.IsValid)
            {
                return View("Manager");
            }

            MessageViewModel message = new MessageViewModel();

            if (department.ID == 0)
            {
                db.Departments.Add(department);
                message.Message = department.Name + " başarıyla eklendi.";
            }
            else
            {

                var model = db.Departments.Find(department.ID);
                if (model == null)
                    return HttpNotFound();
                model.Name = department.Name;
                message.Message = department.Name + " başarıyla güncellendi.";
            }

            //db.Departments.Add(department);
            db.SaveChanges();

            message.Status = true;
            message.LinkText = "Departman Listesi";
            message.Url = "/Departments";

            return View("_Message", message);
        }

        public ActionResult ListPersonnel(int id) 
        {
            var model = db.Personnels.Where(m => m.DepartmentId == id);
            return PartialView("ListPersonnel", model);
        }
        
    }
}