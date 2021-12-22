using SaralDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaralDemo.Controllers
{
   
    public class HomeController : Controller

    {
        public Employee_Management_SystemEntities db = new Employee_Management_SystemEntities();
        [Authorize]
        public ActionResult Index()
        {
            if (Session["EmployeeID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                int userid = Convert.ToInt32(Session["EmployeeID"].ToString());
                var memberinfo = db.LeaveCounts.Where(id => id.EmployeeID == userid).ToList();
                return View(memberinfo);
            }

            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message =  "Contact page.";

            return View();
        }
        public ActionResult profile()
        {
            if (Session["EmployeeID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                int userid = Convert.ToInt32(Session["EmployeeID"].ToString());
                var memberinfo = db.Employees.Where(id => id.EmployeeID == userid).ToList();
                return View(memberinfo);
            }
        }


        //public ActionResult LeaveInfo()
        //{
        //    if (Session["EmployeeID"] == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    else
        //    {
        //        int userid = Convert.ToInt32(Session["EmployeeID"].ToString());
        //        var memberinfo = db.LeaveCounts.Where(id => id.EmployeeID == userid).ToList();
        //        return View(memberinfo);
        //    }

            
        //}

       
    }
}