using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeReimbursement.Models;

namespace EmployeeReimbursement.Controllers
{
    public class UsersController : Controller
    {
        private EmployeeReimbursementEntities2 db = new EmployeeReimbursementEntities2();

        // GET: Users
        public ActionResult UserList()
        {
            return View(db.tblUsers.ToList());
        }

        // GET: Users/Details/5
        public ActionResult UserDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // GET: Users/Create
        [HttpGet]
        public ActionResult Register()
        {
            var deptlist = db.tblDepartments.ToList();

            ViewBag.DepartmentId = new SelectList(deptlist, "DepartmentId", "DepartmentName");

            var deslist = db.tblDesignations.ToList();
            ViewBag.DesignationId = new SelectList(deslist, "DesignationId", "Designation");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserId,FirstName,MiddleName,LastName,Gender,PasswordHash,DepartmentId,DesignationId,ReportingManager,Email")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                var deptlist = db.tblDepartments.ToList();
                ViewBag.DepartmentId = new SelectList(deptlist, "DepartmentId", "DepartmentName");
                db.tblUsers.Add(tblUser);
                db.SaveChanges();
                return RedirectToAction("UserList");
            }

            return View(tblUser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }

            var deptlist = db.tblDepartments.ToList();

            ViewBag.DepartmentId = new SelectList(deptlist, "DepartmentId", "DepartmentName");

            var deslist = db.tblDesignations.ToList();
            ViewBag.DesignationId = new SelectList(deslist, "DesignationId", "Designation");

            return View(tblUser);
        }

        // POST: Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,MiddleName,LastName,Gender,PasswordHash,DepartmentId,DesignationId,ReportingManager,Email")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                var deptlist = db.tblDepartments.ToList();
                ViewBag.DepartmentId = new SelectList(deptlist, "DepartmentId", "DepartmentName");
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            return View(tblUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            db.tblUsers.Remove(tblUser);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult UserDashboard()
        {
            return View();
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
