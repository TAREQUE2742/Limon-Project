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
    public class ProductImageController : Controller
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

        // GET: ProductImage
        public ActionResult Index(int product=0,int page=1)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            var productImages = db.ProductImages.Include(p => p.Product);
            if (product > 0)
                productImages = productImages.Where(pi => pi.productId == product);
            //page number
            int numberofiteam = 5;
            int skip = (page - 1) * numberofiteam;
            productImages = productImages.OrderBy(b => b.title).Skip(skip).Take(numberofiteam);
            int total = db.ProductImages.Count();
            int pageNumber = total / numberofiteam;
            if (total % numberofiteam != 0)
                pageNumber++;
            ViewBag.pageNumber = pageNumber;
            return View(productImages.ToList());
        }

        // GET: ProductImage/Details/5
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
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // GET: ProductImage/Create
        public ActionResult Create()
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            ViewBag.productId = new SelectList(db.Products, "id", "name");
            return View();
        }

        // POST: ProductImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,productId,title,createDate")] ProductImage productImage,HttpPostedFileBase image1)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            productImage.createDate=DateTime.Now;
            productImage.image1 = System.IO.Path.GetFileName(image1.FileName);
            if (ModelState.IsValid)
            {
                db.ProductImages.Add(productImage);
                db.SaveChanges();
                image1.SaveAs(Server.MapPath("../uploads/productimages/")+productImage.id.ToString()+"_"+productImage.image1);
                return RedirectToAction("Index");
            }

            ViewBag.productId = new SelectList(db.Products, "id", "name", productImage.productId);
            return View(productImage);
        }

        // GET: ProductImage/Edit/5
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
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", productImage.productId);
            return View(productImage);
        }

        // POST: ProductImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,productId,image1,title,createDate")] ProductImage productImage)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (ModelState.IsValid)
            {
                db.Entry(productImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productId = new SelectList(db.Products, "id", "name", productImage.productId);
            return View(productImage);
        }

        // GET: ProductImage/Delete/5
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
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: ProductImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            ProductImage productImage = db.ProductImages.Find(id);
            db.ProductImages.Remove(productImage);
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
