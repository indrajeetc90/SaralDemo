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
    public class Leave_RequestController : Controller
    {
        private Employee_Management_SystemEntities db = new Employee_Management_SystemEntities();

        // GET: Leave_Request
       
        public ActionResult Index()
        {
            if (User.IsInRole("IT Head")|| User.IsInRole("Project Manager"))
            {
                var leave_Request = db.Leave_Request.Include(l => l.Employee);

                return View(leave_Request.ToList());
            }
            else
            {
                int userid = Convert.ToInt32(Session["EmployeeID"].ToString());
                var leave_Request = db.Leave_Request.Include(l => l.Employee);
                var memberinfo = db.Leave_Request.Where(id => id.EmployeeID == userid).ToList();
                return View(memberinfo);
            }
                
        }



        // GET: Leave_Request/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave_Request leave_Request = db.Leave_Request.Find(id);
            if (leave_Request == null)
            {
                return HttpNotFound();
            }
            return View(leave_Request);
        }

        // GET: Leave_Request/Create
        public ActionResult Create()
        {
            Leave_Request obj = new Leave_Request();
            obj.LeaveStatus = "Pending";
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName");

            int id = Convert.ToInt32(Session["EmployeeID"].ToString());
            var empname = db.Employees.Where(name => name.EmployeeID ==id ).FirstOrDefault();
            ViewBag.empname = empname.FullName;

            return View(obj);
        }

        // POST: Leave_Request/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,FromDate,ToDate,LeaveType,ReasonOfLeave,HalfDay,FullDay,LeaveStatus,LeaveID,TotalDays")] Leave_Request leave_Request)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["EmployeeID"].ToString());
                var empname = db.Employees.Where(name => name.EmployeeID == id).FirstOrDefault();
                leave_Request.EmployeeID = empname.EmployeeID;
                db.Leave_Request.Add(leave_Request);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", leave_Request.EmployeeID);
            return View(leave_Request);
        }

        // GET: Leave_Request/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave_Request leave_Request = db.Leave_Request.Find(id);
            if (leave_Request == null)
            {
                return HttpNotFound();
            }
          
           
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", leave_Request.EmployeeID);
            return View(leave_Request);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaveID,EmployeeID,FromDate,ToDate,LeaveType,ReasonOfLeave,HalfDay,FullDay,LeaveStatus,TotalDays")] Leave_Request leave_Request)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["EmployeeID"].ToString());
                var empname = db.Employees.Where(name => name.EmployeeID == id).FirstOrDefault();
                leave_Request.EmployeeID = empname.EmployeeID;
                db.Entry(leave_Request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "FullName", leave_Request.EmployeeID);
            return View(leave_Request);
        }

        // GET: Leave_Request/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave_Request leave_Request = db.Leave_Request.Find(id);
            if (leave_Request == null)
            {
                return HttpNotFound();
            }
            return View(leave_Request);
        }

        // POST: Leave_Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leave_Request leave_Request = db.Leave_Request.Find(id);
            db.Leave_Request.Remove(leave_Request);
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
       
       

        [HttpGet]
        public ActionResult ApproveLeave(int LeaveID)
        {
            if (Session["EmployeeID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {

                int userid = Convert.ToInt32(Session["EmployeeID"].ToString());
                var userdata = db.Leave_Request.Where(lid => lid.LeaveID == LeaveID).FirstOrDefault();
               
                var leavecount= db.LeaveCounts.Where(id=>id.EmployeeID==userdata.EmployeeID).FirstOrDefault();
                var user = db.Leave_Request.Where(u => u.LeaveType == userdata.LeaveType).Where(id=>id.EmployeeID==userdata.EmployeeID).Where(status=> status.LeaveStatus=="Pending").FirstOrDefault();
                db.Entry(userdata).State = EntityState.Modified;
                db.SaveChanges();
              
                if (user.LeaveType == 1)
                {
                    if (leavecount.CasualLeave >= user.TotalDays)
                    {
                        userdata.LeaveStatus = "Approved";
                        leavecount.CasualLeave = leavecount.CasualLeave - user.TotalDays;
                        leavecount.TotalLeave = leavecount.TotalLeave - user.TotalDays;
                        db.Entry(leavecount).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        TempData["Message"] = "Casual";
                    }


                }
                else if(user.LeaveType == 2)
                    
                {
                    if (leavecount.OptionalHoliday >= user.TotalDays)
                    {
                        userdata.LeaveStatus = "Approved";
                        leavecount.OptionalHoliday = leavecount.OptionalHoliday - user.TotalDays;
                        leavecount.TotalLeave = leavecount.TotalLeave - user.TotalDays;
                        db.Entry(leavecount).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        TempData["Message"] = "Optional";
                    }
                }
                else if (user.LeaveType == 3)
                {
                        userdata.LeaveStatus = "Approved";
                        leavecount.LoosOfPay = leavecount.LoosOfPay + user.TotalDays;
                        db.Entry(leavecount).State = EntityState.Modified;
                        db.SaveChanges();

                }
                else
                {
                    TempData["Message"] = "Loss";
                }


                return RedirectToAction("Index", "Leave_Request");
            }
        }

        [HttpGet]
        public ActionResult DeclineLeave(int LeaveID)
        {
            if (Session["EmployeeID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {

                int userid = Convert.ToInt32(Session["EmployeeID"].ToString());
                var userdata = db.Leave_Request.Where(lid => lid.LeaveID == LeaveID).FirstOrDefault();
                userdata.LeaveStatus = "Rejected";
                db.Entry(userdata).State = EntityState.Modified; 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //public ActionResult Byleaveid(int EmployeeID)
        //{
        //    if (Session["EmployeeID"] == null)
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //    else
        //    {
        //        int userid = Convert.ToInt32(Session["EmployeeID"].ToString());
        //        var memberinfo = db.Leave_Request.Where(id => id.EmployeeID == userid).ToList();
        //        return View(memberinfo);
        //    }

           
        //}


    }
}
