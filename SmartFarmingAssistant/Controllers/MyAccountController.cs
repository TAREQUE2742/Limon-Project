using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartFarmingAssistant.Models;

namespace SmartFarmingAssistant.Controllers
{
    public class MyAccountController : Controller
    {
        private Innovation002Entities db = new Innovation002Entities();

        // GET: MyAccount
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var v = db.Users.SingleOrDefault(u => u.email.ToLower() == login.Email && u.password == login.Password);
                if (v == null)
                {
                    ViewBag.Error = "Your Email Or password is Invalid";
                    return RedirectToAction("MasterLogin", "MyAccount");
                }

                if (v.type == "Admin")
                {
                    Session["id"] = v.id;
                    Session["name"] = v.name;
                    Session["type"] = v.type;
                    ViewBag.message = "Welcome to admin pannel";
                    return RedirectToAction("Successfull", "Admin");
                }

                if (v.type == "user")
                {
                    Session["id"] = v.id;
                    Session["name"] = v.name;
                    Session["type"] = v.type;
                    ViewBag.message = "Welcome to user apnnel";
                    return RedirectToAction("CustomerLogin", "Public");
                }
                //Session["id"] = v.id;
                //Session["name"] = v.name;
                //Session["type"] = v.type;
                //ViewBag.message = "Login SucessFully";
                return RedirectToAction("Product", "Public");
            }
            ViewBag.Error = "Invalid User";
            return RedirectToAction("MasterLogin", "MyAccount");
        }
        public ViewResult MasterLogin()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["id"] = "";
            Session["name"] = "";
            Session["type"] = "";
            ViewBag.message = "LogOut SucessFully";
            return RedirectToAction("MasterLogout","MyAccount");
        }

        public ActionResult AccountDetails()
        {
            var us = db.Users.Find(Session["id"]);
            return View(us);
        }

        public ActionResult Edit()
        {
            var v = db.Users.Find(Session["id"]);
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", v.cityId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", v.genderId);
            return View(v);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,contact,email,password,createDate,genderId,type,address,cityId")] User user)
        {

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Session["name"] = user.name;
                return RedirectToAction("Index","Public");
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", user.cityId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", user.genderId);
            return View(user);
        }

        public ActionResult Register()
        {
            ViewBag.cityId = new SelectList(db.Cities, "id", "name");
            ViewBag.genderId = new SelectList(db.Genders, "id", "name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "id,name,contact,email,password,createDate,genderId,type,address,cityId")] User user)
        {
            user.createDate=DateTime.Now;
            user.type = "user";
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login","MyAccount");
            }

            ViewBag.cityId = new SelectList(db.Cities, "id", "name", user.cityId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", user.genderId);
            return View(user);
            
        }
        //Admin details
        public ActionResult AdminDetails()
        {
            var us = db.Users.Find(Session["id"]);
            return View(us);
        }

        public ActionResult AdminEdit()
        {
            var v = db.Users.Find(Session["id"]);
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", v.cityId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", v.genderId);
            return View(v);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminEdit([Bind(Include = "id,name,contact,email,password,createDate,genderId,type,address,cityId")] User user)
        {

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Session["name"] = user.name;
                return RedirectToAction("Index", "Admin");
            }
            ViewBag.cityId = new SelectList(db.Cities, "id", "name", user.cityId);
            ViewBag.genderId = new SelectList(db.Genders, "id", "name", user.genderId);
            return View(user);
        }
        public ViewResult MasterLogout()
        {
            return View();
        }

    }
}
