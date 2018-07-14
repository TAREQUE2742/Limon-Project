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
    public class wishlistController : Controller
    {
        private Innovation002Entities db = new Innovation002Entities();

        // GET: wishlist
        public ActionResult Index()
        {
            var wishlists = db.wishlists.Include(w => w.Product).Include(w => w.User);
            return View(wishlists.ToList());
        }

        // GET: wishlist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishlist wishlist = db.wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // GET: wishlist/Create
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.Products, "id", "name");
            ViewBag.userId = new SelectList(db.Users, "id", "name");
            return View();
        }

        // POST: wishlist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,productId,dateTime")] wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                db.wishlists.Add(wishlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Products, "id", "name", wishlist.productId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", wishlist.userId);
            return View(wishlist);
        }

        // GET: wishlist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishlist wishlist = db.wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", wishlist.productId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", wishlist.userId);
            return View(wishlist);
        }

        // POST: wishlist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,productId,dateTime")] wishlist wishlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", wishlist.productId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", wishlist.userId);
            return View(wishlist);
        }

        // GET: wishlist/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wishlist wishlist = db.wishlists.Find(id);
            if (wishlist == null)
            {
                return HttpNotFound();
            }
            return View(wishlist);
        }

        // POST: wishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            wishlist wishlist = db.wishlists.Find(id);
            db.wishlists.Remove(wishlist);
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
