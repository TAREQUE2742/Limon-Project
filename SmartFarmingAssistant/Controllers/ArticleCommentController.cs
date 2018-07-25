using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartFarmingAssistant.Models;

namespace LimonSelling.Controllers
{
    public class ArticleCommentController : Controller
    {
        private Innovation002Entities db = new Innovation002Entities();

        // GET: ArticleComment
        public ActionResult Index()
        {
            var articleComments = db.ArticleComments.Include(a => a.Article).Include(a => a.User);
            return View(articleComments.ToList());
        }

        // GET: ArticleComment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleComment articleComment = db.ArticleComments.Find(id);
            if (articleComment == null)
            {
                return HttpNotFound();
            }
            return View(articleComment);
        }

        // GET: ArticleComment/Create
        public ActionResult Create()
        {
            ViewBag.articleId = new SelectList(db.Articles, "id", "title");
            ViewBag.userId = new SelectList(db.Users, "id", "name");
            return View();
        }

        // POST: ArticleComment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,articleId,userId,comment,commentDate")] ArticleComment articleComment)
        {
            if (ModelState.IsValid)
            {
                db.ArticleComments.Add(articleComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.articleId = new SelectList(db.Articles, "id", "title", articleComment.articleId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", articleComment.userId);
            return View(articleComment);
        }

        // GET: ArticleComment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleComment articleComment = db.ArticleComments.Find(id);
            if (articleComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.articleId = new SelectList(db.Articles, "id", "title", articleComment.articleId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", articleComment.userId);
            return View(articleComment);
        }

        // POST: ArticleComment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,articleId,userId,comment,commentDate")] ArticleComment articleComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articleComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.articleId = new SelectList(db.Articles, "id", "title", articleComment.articleId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", articleComment.userId);
            return View(articleComment);
        }

        // GET: ArticleComment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticleComment articleComment = db.ArticleComments.Find(id);
            if (articleComment == null)
            {
                return HttpNotFound();
            }
            return View(articleComment);
        }

        // POST: ArticleComment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticleComment articleComment = db.ArticleComments.Find(id);
            db.ArticleComments.Remove(articleComment);
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
