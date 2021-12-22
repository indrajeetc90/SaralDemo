using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaralDemo.Models;
using System.Web.Security;
using System.Security.Principal;

namespace SaralDemo.Controllers
{
  
    public class AccountController : Controller
    {
        
        Employee_Management_SystemEntities db = new Employee_Management_SystemEntities();
        // GET: Account
      
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        

        public ActionResult Login(Employee models)
        {
            
            using (var context = new Employee_Management_SystemEntities())
            {

                var data = db.Employees.Where(x => x.Email == models.Email).FirstOrDefault();
                var result = db.Roles.Where(x => x.EmployeeID == data.EmployeeID).FirstOrDefault();
                bool  isValid = context.Employees.Any(x => x.Email == models.Email && x.Password == models.Password);
                if(isValid)
                {
                    if (result.Role1 == "IT Head")
                    {
                      
                        Session["EmployeeID"] = data.EmployeeID.ToString();
                        FormsAuthentication.SetAuthCookie(models.Email, false);
                        TempData["Message"] = "Added";
                        return RedirectToAction("Index", "AdminDashboard");
                       
                    }
                    else if (result.Role1 == "Employee" || result.Role1=="Project Manager")
                    {
                        Session["EmployeeID"] = data.EmployeeID.ToString();
                        FormsAuthentication.SetAuthCookie(models.Email, false);
                        TempData["Message"] = "Added";
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Message"] = "Failed";
                ModelState.AddModelError("", "Invalid Email And Password");

                return View();


            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}