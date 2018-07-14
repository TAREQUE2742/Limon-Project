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
    public class BrandController : Controller
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

        // GET: Brand
        public ActionResult Index()
        {  //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            return View(db.Brands.ToList());
        }

        // GET: Brand/Details/5
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
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Brand/Create
        public ActionResult Create()
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            return View();
        }

        // POST: Brand/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,description")] Brand brand)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (ModelState.IsValid)
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(int? id)
        {  //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brand/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,description")] Brand brand)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Brand/Delete/5
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
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            Brand brand = db.Brands.Find(id);
            try
            {
                db.Brands.Remove(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                 return RedirectToAction("DbError","Brand");
            }
           
        }
        public ViewResult DbError()
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
