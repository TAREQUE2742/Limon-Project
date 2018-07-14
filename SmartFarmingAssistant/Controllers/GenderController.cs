using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartFarmingAssistant.Models;

namespace SmartFarmingAssistant.Controllers
{
    public class GenderController : Controller
    {

        //authenticate
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
        //authenticate
        private Innovation002Entities db = new Innovation002Entities();

        // GET: Gender
        public ActionResult Index()
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            return View(db.Genders.ToList());
        }

        // GET: Gender/Details/5
        public ActionResult Details(int? id)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // GET: Gender/Create
        public ActionResult Create()
        {  //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            return View();
        }

        // POST: Gender/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Gender gender)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (ModelState.IsValid)
            {
                db.Genders.Add(gender);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gender);
        }

        // GET: Gender/Edit/5
        public ActionResult Edit(int? id)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // POST: Gender/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Gender gender)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (ModelState.IsValid)
            {
                db.Entry(gender).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gender);
        }

        // GET: Gender/Delete/5
        public ActionResult Delete(int? id)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gender gender = db.Genders.Find(id);
            if (gender == null)
            {
                return HttpNotFound();
            }
            return View(gender);
        }

        // POST: Gender/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {  //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            Gender gender = db.Genders.Find(id);
            db.Genders.Remove(gender);
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
