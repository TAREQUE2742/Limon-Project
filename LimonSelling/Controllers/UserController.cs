using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LimonSelling.Models;

namespace LimonSelling.Controllers
{
    public class UserController : Controller
    {
        public bool IsAuthunticate()
        {
            if (Session["type"] == null || Session["type"].ToString() == "")
            { 
                return false;
            }
            else if (Session["type"].ToString() != "Admin")
            {
                return false;
            }
            return true;
        }

        private Innovation002Entities db = new Innovation002Entities();

        // GET: User
       
        public ActionResult Index()
        {   //autheticate
            if (!IsAuthunticate())
            {
                
                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            var users = db.Users.Include(u => u.City).Include(u => u.Gender);
            return View(users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name");
            ViewBag.genderId = new SelectList(db.Genders, "id", "name");
            return View();
        }

        // POST: Customer/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,contact,email,password,createDate,genderId,type,address,cityId")] User user)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }

            user.createDate = DateTime.Now;
            user.type = "user";
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cityId = new SelectList(db.Cities, "id", "name", user.cityId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", user.genderId);
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", user.cityId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", user.genderId);
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,contact,email,password,createDate,genderId,type,address,cityId")] User user)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", user.cityId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", user.genderId);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
