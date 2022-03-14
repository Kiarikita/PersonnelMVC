using PersonnelMVC.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonnelMVC.Controllers
{
    [AllowAnonymous] //login satfasına girilebilsin diye 

    public class SecurityController : Controller
    {
        StudentDBEntities db = new StudentDBEntities();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(User users)
        {
            var user = db.Users.FirstOrDefault(x => x.Username == users.Username && x.Password == users.Password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(users.Username, false);
                return RedirectToAction("Index", "Departments");
               
            }
            else
            {
                ViewBag.Message = "Geçersiz kullanıcı adı ve/veya şifre!";
                return View();
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }
    }
}