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
    public class ArticleController : Controller
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
        private Innovation002Entities db = new Innovation002Entities();

        // GET: Article
        public ActionResult Index(string search = "", int page = 1)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
           
            var articles = db.Articles.Include(a => a.Product).ToList();
            if (search != "")
                articles = articles.Where(ps => ps.title.ToLower().Contains(search.ToLower()) || ps.Product.name.ToLower().Contains(search.ToLower())).ToList();
            if (search == "")
            {
                ViewBag.message = "No match article found";
            }
            //pagenumber
            int numberofiteam = 3;
            int skip = (page - 1) * numberofiteam;
            articles = articles.OrderBy(b => b.title).Skip(skip).Take(numberofiteam).ToList();
            int total = db.Articles.Count();
            int pageNumber = total / numberofiteam;
            if (total % numberofiteam != 0)
                pageNumber++;
            ViewBag.pageNumber = pageNumber;

            return View(articles);
        }

        // GET: Article/Details/5
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
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,body,publishDate,productId")] Article article)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            article.publishDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Products, "id", "name", article.productId);
            return View(article);
        }

        // GET: Article/Edit/5
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
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", article.productId);
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,body,publishDate,productId")] Article article)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            article.publishDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", article.productId);
            return View(article);
        }

        // GET: Article/Delete/5
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
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteComment(int id)
        {
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            ArticleComment ac = db.ArticleComments.Find(id);
            db.ArticleComments.Remove(ac);
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
