using SmartFarmingAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartFarmingAssistant.Controllers
{
    public class SfaController : Controller
    {
        private Innovation002Entities db = new Innovation002Entities();
        // GET: Sfa
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KrishiAnchal()
        {
            return View();
        }
        public ActionResult Mati()
        {
            return View();
        }
        public ActionResult Language()
        {
            return View();
        }
        public ActionResult EmasherKrishi()
        {
            return View();
        }
        public ActionResult KrishiWeather()
        {
            return View();
        }
        public ActionResult BishesPoramorsho()
        {
            return View();
        }
        public ActionResult SamprotikUdvabon()
        {
            return View();
        }
        public ActionResult FosholerUtpadon()
        {
            return View();
        }
        public ActionResult KrishiVittikVideo()
        {
            return View();
        }
        public ActionResult KrishiVittikTotthoUpatto()
        {
            return View();
        }
        public ActionResult KhatoNarikel()
        {
            return View();
        }
        public ActionResult UnnotoPat()
        {
            return View();
        }
        public ActionResult VejalSharChenarUpay()
        {
            return View();
        }
        public ActionResult IdurDomon()
        {
            return View();
        }
        public ActionResult ArticleShow()
        {
            var articles = db.Articles;
            return View(articles.ToList());
        }
        public ActionResult ArticleDetails()
        {
            return View();
        }


    }
}