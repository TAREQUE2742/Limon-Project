using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartFarmingAssistant.Controllers
{
    public class AdminController : Controller
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
        // GET: Admin
        public ActionResult Index()
        {   //autheticate
            if (!IsAuthunticate())
            {

                return RedirectToAction("Login", "MyAccount");
            }
            //authenticate
            return View();
        }
        public ViewResult UpdateSuccessfull()
        {

            return View();
        }
        public ViewResult Successfull()
        {
            
            return View();
        }
    }
}