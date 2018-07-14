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
    public class ProductCommentController : Controller
    {
        private Innovation002Entities db = new Innovation002Entities();

        // GET: ProductComment
        public ActionResult Index()
        {
            var productComments = db.ProductComments.Include(p => p.Product).Include(p => p.User);
            return View(productComments.ToList());
        }

        // GET: ProductComment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductComment productComment = db.ProductComments.Find(id);
            if (productComment == null)
            {
                return HttpNotFound();
            }
            return View(productComment);
        }

        // GET: ProductComment/Create
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.Products, "id", "name");
            ViewBag.userId = new SelectList(db.Users, "id", "name");
            return View();
        }

        // POST: ProductComment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,productId,userId,dateTime,comment")] ProductComment productComment)
        {
            productComment.dateTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ProductComments.Add(productComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Products, "id", "name", productComment.productId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", productComment.userId);
            return View(productComment);
        }

        // GET: ProductComment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductComment productComment = db.ProductComments.Find(id);
            if (productComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", productComment.productId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", productComment.userId);
            return View(productComment);
        }

        // POST: ProductComment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,productId,userId,dateTime,comment")] ProductComment productComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", productComment.productId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", productComment.userId);
            return View(productComment);
        }

        // GET: ProductComment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductComment productComment = db.ProductComments.Find(id);
            if (productComment == null)
            {
                return HttpNotFound();
            }
            return View(productComment);
        }

        // POST: ProductComment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductComment productComment = db.ProductComments.Find(id);
            db.ProductComments.Remove(productComment);
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
