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
    public class OrderController : Controller
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

        // GET: Order
        public ActionResult Index(int page=1,string search="")
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            var orders = db.Orders.Include(o => o.City).Include(o => o.PaymentMethod).Include(o => o.User);
            //search
            if (search != "")
                orders = orders.Where(ps => ps.User.name.ToLower().Contains(search.ToLower()) || ps.City.name.ToLower().Contains(search.ToLower()) || ps.status.ToLower().Contains(search.ToLower()) || ps.number.ToLower().Contains(search.ToLower()) || ps.total.ToString().Contains(search.ToLower()));
            ViewBag.search = search;
            //end serach
            int numberofiteam = 2;
            int skip = (page - 1) * numberofiteam;
            
            orders = orders.OrderBy(b => b.number).Skip(skip).Take(numberofiteam);
            int total = db.Orders.Count();
            int pageNumber = total / numberofiteam;
            if (total % numberofiteam != 0)
                pageNumber++;
            ViewBag.pageNumber = pageNumber;
            return View(orders);
        }

        // GET: Order/Details/5 Make invoice
        public ActionResult Invoice(int? id)
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
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        // GET: Order/Details/5 Make invoice
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
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            ViewBag.cityId = new SelectList(db.Cities, "id", "name");
            ViewBag.paymentMethodId = new SelectList(db.PaymentMethods, "id", "name");
            ViewBag.userId = new SelectList(db.Users, "id", "name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,dateTime,number,userId,deliveryCharge,otherCharge,total,paymentMethodId,deliveryAddress,cityId,status")] Order order)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cityId = new SelectList(db.Cities, "id", "name", order.cityId);
            ViewBag.paymentMethodId = new SelectList(db.PaymentMethods, "id", "name", order.paymentMethodId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", order.userId);
            return View(order);
        }

        // GET: Order/Edit/5
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
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", order.cityId);
            ViewBag.paymentMethodId = new SelectList(db.PaymentMethods, "id", "name", order.paymentMethodId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", order.userId);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dateTime,number,userId,deliveryCharge,otherCharge,total,paymentMethodId,deliveryAddress,cityId,status")] Order order)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", order.cityId);
            ViewBag.paymentMethodId = new SelectList(db.PaymentMethods, "id", "name", order.paymentMethodId);
            ViewBag.userId = new SelectList(db.Users, "id", "name", order.userId);
            return View(order);
        }

        // GET: Order/Delete/5
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
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Report()
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
