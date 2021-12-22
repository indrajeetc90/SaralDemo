using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaralDemo.Models;

namespace SaralDemo.Controllers
{[Authorize]
    public class Salary_SlipController : Controller
    {
        private Employee_Management_SystemEntities db = new Employee_Management_SystemEntities();

        // GET: Salary_Slip
        public ActionResult Index()
        {
            if (User.IsInRole("IT Head"))
            {
                var salary_Slip = db.Salary_Slip.Include(s => s.Employee);
                return View(salary_Slip.ToList());
            }

            else
            {
                int userid = Convert.ToInt32(Session["EmployeeID"].ToString());
                var salary_Slip = db.Salary_Slip.Include(s => s.Employee);
                var memberinfo = db.Salary_Slip.Where(id => id.EmployeeID == userid).ToList();
                return View(memberinfo);
            }
        }

          

        // GET: Salary_Slip/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary_Slip salary_Slip = db.Salary_Slip.Find(id);
            if (salary_Slip == null)
            {
                return HttpNotFound();
            }
            return View(salary_Slip);
        }


        [Authorize(Roles = "IT Head")]
        // GET: Salary_Slip/Create
        public ActionResult Create()
        {
            Salary_Slip obj = new Salary_Slip();
            obj.TravelAllowance = 500;
            obj.EsicCharges = 452;
            obj.PfCharges = 1200;
            obj.MedicalAllowance = 500;
            
            //obj.Date = DateTime.Today;

            //ViewBag.Total = obj.TravelAllowance + obj.TravelAllowance+ obj.PfCharges+ obj.MedicalAllowance;


            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");
            
            return View(obj);
        }

        // POST: Salary_Slip/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaryID,EmployeeID,Date,BasicPay,TravelAllowance,EsicCharges,PfCharges,MedicalAllowance,Tax,LeaveDeduction,TotalAmount")] Salary_Slip salary_Slip, string actionType)
        {
          

            if (ModelState.IsValid)
            {
                db.Salary_Slip.Add(salary_Slip);
                db.SaveChanges();
                if (actionType == "Save")
                {
                    TempData["Message"] = "Credited";
                    return RedirectToAction("Create");
                }
                else if (actionType == "Save and View")
                {
                    TempData["Message"] = "Credited";
                    return RedirectToAction("Index");
                }
                
                return RedirectToAction("Create");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", salary_Slip.EmployeeID);
            return View(salary_Slip);
        }

       
       
        // GET: Salary_Slip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary_Slip salary_Slip = db.Salary_Slip.Find(id);
            if (salary_Slip == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", salary_Slip.EmployeeID);
            return View(salary_Slip);
        }

        // POST: Salary_Slip/Edit/5
        [Authorize(Roles = "IT Head")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryID,EmployeeID,Date,BasicPay,TravelAllowance,EsicCharges,PfCharges,MedicalAllowance,Tax,LeaveDeduction,TotalAmount")] Salary_Slip salary_Slip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary_Slip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", salary_Slip.EmployeeID);
            return View(salary_Slip);
        }

        [Authorize(Roles = "IT Head")]
        // GET: Salary_Slip/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary_Slip salary_Slip = db.Salary_Slip.Find(id);
            if (salary_Slip == null)
            {
                return HttpNotFound();
            }
            return View(salary_Slip);
        }

        // POST: Salary_Slip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salary_Slip salary_Slip = db.Salary_Slip.Find(id);
            db.Salary_Slip.Remove(salary_Slip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult leaveSalary(int EmplyeeID)
        {
            if (Session["EmployeeID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {

                int userid = Convert.ToInt32(Session["EmployeeID"].ToString());
                var userdata = db.Leave_Request.Where(lid => lid.LeaveID == EmplyeeID).FirstOrDefault();
               
                db.Entry(userdata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
