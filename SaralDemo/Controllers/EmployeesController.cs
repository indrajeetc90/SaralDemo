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
{  
    [Authorize]
    public class EmployeesController : Controller
    {
        private Employee_Management_SystemEntities db = new Employee_Management_SystemEntities();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }
        [Authorize(Roles ="IT Head")]
        public ActionResult AddEmployee()
        {
            return View();
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "IT Head")]
        public ActionResult Addemp()
        {

            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "IT Head")]
        public ActionResult Addemp(Employee emp, Documents_BankDetails bank, TechnicalDetail tech, string actionType)
        {
            if (ModelState.IsValid)
            {
                var isEmailAlreadyExists = db.Employees.Any(x => x.Email == emp.Email);

                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "User with this email already exists");

                    return View();
                }
                else
                {
                    Session["EmployeeID"] = emp.EmployeeID.ToString().SingleOrDefault();
                    db.Employees.Add(emp);
                    db.SaveChanges();

                    bank.EmployeeID = emp.EmployeeID;
                    db.Documents_BankDetails.Add(bank);
                    db.SaveChanges();

                    tech.EmployeeID = emp.EmployeeID;
                    db.TechnicalDetails.Add(tech);
                    db.SaveChanges();
                  

                    if (actionType == "Save")
                    {
                        TempData["Message"] = "Added";
                        return RedirectToAction("Addemp");
                    }
                    else if (actionType == "Save and View")
                    {
                        TempData["Message"] = "Added";
                        return RedirectToAction("Index");
                    }

                    return RedirectToAction("Addemp");

                }


            }
            TempData["Message"] = "failed";
            return View();
        }




        // GET: Employees/Edit/5
        [Authorize(Roles = "IT Head")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,FullName,Email,Password,Gender,Contact,DOB,BloodGroup,DOJ,MaritalStatus,FatherName,MotherName,CurrentAddress,PermanentAddress,Designation,Role,RoleType")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Employee Edited Successfully ";
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "IT Head")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
    }
}
