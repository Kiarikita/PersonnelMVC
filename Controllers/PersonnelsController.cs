using PersonnelMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PersonnelMVC.ViewModels;

namespace PersonnelMVC.Controllers
{
    

    public class PersonnelsController : Controller
    {
        // GET: Personnels
        StudentDBEntities db = new StudentDBEntities();

        [OutputCache(Duration = 30)]

        [Authorize(Roles = "M, A, D")]

        public ActionResult Index()
        {
            var model = db.Personnels.Include(x=>x.Department).ToList();
            
            return View(model);
        }

        

        public ActionResult Update(int id)
        {
            var model = new PersonnelDepartmentViewModel() {
                Departments = db.Departments.ToList(),
                Personnel = db.Personnels.Find(id)
            };
            
            return View("Manager", model);
        }

        public ActionResult Delete(int id)
        {
            var model = db.Personnels.Find(id);
            if (model == null)
                return HttpNotFound();

            db.Personnels.Remove(model);
            db.SaveChanges();

            return RedirectToAction("Index", "Personnels");
        }

        public ActionResult Create()
        {
            var model = new PersonnelDepartmentViewModel()
            {
                Departments = db.Departments.ToList(),
                Personnel = new Personnel()
            };
            return View("Manager", model);
        }

        [ValidateAntiForgeryToken]

        public ActionResult Save(Personnel personnel) 
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonnelDepartmentViewModel()
                {
                    Departments = db.Departments.ToList(),
                    Personnel = new Personnel()
                };
                return View("Manager", model);
            }

            MessageViewModel message = new MessageViewModel();

            if (personnel.ID == 0)//ekleme işlemi
            {
                db.Personnels.Add(personnel);
                message.Message = personnel.Name + " " + personnel.Surname +" başarıyla eklendi.";
            }
            else
            {
                //güncelleme işlemi
                db.Entry(personnel).State = EntityState.Modified;
                message.Message = personnel.Name + " " + personnel.Surname + " başarıyla güncellendi.";
            }
            db.SaveChanges();

            message.Status = true;
            message.LinkText = "Personel Listesi";
            message.Url = "/Personnels";

            return View("_Message", message);

        }

        public int? SumOfSalary() //soru işareti nullable olduğunu gösterir
        {
            return db.Personnels.Sum(x=> x.Salary);
        }
    }
}
//List<SelectListItem> departments = (from i in db.Departments.ToList()
//                                    select new SelectListItem
//                                    {
//                                        Text = i.Name,
//                                        Value = i.ID.ToString()
//                                    }).ToList();

//ViewBag.dprt = departments;