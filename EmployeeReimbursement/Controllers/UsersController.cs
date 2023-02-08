using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeReimbursement.Models;

namespace EmployeeReimbursement.Controllers
{
    public class UsersController : Controller
    {
        private readonly EmployeeClaimReimbursementSystemEntities2 db;

        public UsersController()
        {
            db = new EmployeeClaimReimbursementSystemEntities2();
        }
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

            ViewBag.DepartmentID = new SelectList(deptlist, "DepartmentID", "DepartmentName");

            var rolelist = db.tblRoles.ToList();

            ViewBag.RoleID = new SelectList(rolelist, "RoleID", "RoleType");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserID,Forename,Surname,Address,Email,PasswordHash,DepartmentID,RoleID,PhoneNumber,PhoneNumberConfirmed,ManagerID,TwoFactorEnabled,LastLogin,Active")] tblUser tblUser)
        {
                var deptlist = db.tblDepartments.ToList();
                ViewBag.DepartmentID = new SelectList(deptlist, "DepartmentID", "DepartmentName");

                var rolelist = db.tblRoles.ToList();
                ViewBag.RoleID = new SelectList(rolelist, "RoleID", "RoleType");

                db.tblUsers.Add(tblUser);
                db.SaveChanges();
                return RedirectToAction("UserList");
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
            ViewBag.DepartmentId = new SelectList(deptlist, "DepartmentID", "DepartmentName");


            var rolelist = db.tblRoles.ToList();
            ViewBag.RoleID = new SelectList(rolelist, "RoleID", "RoleType");

            return View(tblUser);
        }

        // POST: Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Forename,Surname,Address,Email,PasswordHash,DepartmentID,PhoneNumber,PhoneNumberConfirmed,ManagerID,TwoFactorEnabled,LastLogin,Active")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                var deptlist = db.tblDepartments.ToList();
                ViewBag.DepartmentId = new SelectList(deptlist, "DepartmentID", "DepartmentName");


                var rolelist = db.tblRoles.ToList();
                ViewBag.RoleID = new SelectList(rolelist, "RoleID", "RoleType");

                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            return View(tblUser);
        }

        //// GET: Users/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblUser tblUser = db.tblUsers.Find(id);
        //    if (tblUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblUser);
        //}

        //POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")] 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            //tblUser tblUser = db.tblUsers.Find(id);
            //db.tblUsers.Remove(tblUser);
            //db.SaveChanges();
            //return RedirectToAction("UserList");

            var res = db.tblUsers.Where(x => x.UserID == id.ToString()).SingleOrDefault();
            db.tblUsers.Remove((tblUser)res);
            db.SaveChanges();

            var list = db.tblUsers.ToList();
            return View("UserList", list);
        }

        public ActionResult UserDashboard(int? id)
        {
            //tblClaimMaster tblClaimMaster = db.tblClaimMasters.Find(id);
            //db.tblClaimMasters.Add(tblClaimMaster);
            //db.SaveChanges();
            return View(db.tblClaimMasters.ToList());
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult NewClaim()
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
