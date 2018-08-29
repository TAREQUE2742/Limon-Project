using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.WebPages;
using SmartFarmingAssistant.Models;

namespace SmartFarmingAssistant.Controllers
{
    public class PublicController : Controller
    {
        
        private Innovation002Entities db = new Innovation002Entities();
        
        
        // GET: Public
        public ActionResult Brand()
        {
            var brands = db.Brands;
            return View(brands);
        }
        // GET: Public
        public ActionResult Category()
        {
            var cat = db.Categories.Where(c => c.categoryId == null);
            return View(cat);
        }
        // GET: Public

        



        public ActionResult Product(int brand=0,int category=0,int add=0,string search="",int page=1)
        {
            
            var product = db.Products.ToList();
            if (brand > 0)
            {
                product = product.Where(p => p.brandId == brand).ToList();

            }

            if (category > 0)
            {
                product = product.Where(p => p.categoryId == category).ToList();
            }
            if (search != "")
                product = product.Where(ps => ps.name.ToLower().Contains(search.ToLower()) ||ps.Brand.name.ToLower().Contains(search.ToLower()) || ps.Category.name.ToLower().Contains(search.ToLower())|| ps.price.ToString().Contains(search.ToLower())).ToList();
            if (search == "")
            {
                ViewBag.notfound = "No match product found";
            }

            //pagenumber
            int numberofiteam = 8;
            int skip = (page - 1) * numberofiteam;
            product = product.OrderBy(b => b.name).Skip(skip).Take(numberofiteam).ToList();
            int total = db.Products.Count();
            int pageNumber = total / numberofiteam;
            if (total % numberofiteam != 0)
                pageNumber++;
            ViewBag.pageNumber = pageNumber;

            ViewBag.category = category;
            return View(product);
        }

        private void AddItem(int add)
        {
            throw new NotImplementedException();
        }


      
       
      
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {   
            return View();
        }

        

        public ActionResult ProductDetails(int Product,string comment="",int add=0)
        {
            if (add > 0)
            {
                AddItem(add);
            }
            ;
            if (comment != "")
            {
                int userId = (int)Session["id"];
                db.ProductComments.Add(new ProductComment() { productId = Product, userId = userId, dateTime = DateTime.Now, comment = comment });
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return RedirectToAction("ProductDetails", "Public");
                }
            } 
            var p = db.Products.Find(Product);
            ViewBag.product = Product;
            return View(p);
        }

        private int IsExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for(int i=0;i<cart.Count;i++)
                if (cart[i].Pr.id == id)
                    return i;
            return -1;
        }

        public ActionResult DeleteOrder(int id)
        {
            int index = IsExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            
            return RedirectToAction("EmptyOrder","Public");
        }
        public ActionResult OrderNow(int id)
        {
            if (Session["cart"] == null)
            {
               List<Item> cart=new List<Item>();
               cart.Add(new Item(db.Products.Find(id),1));
                Session["cart"] = cart;
                
            }
            else
            {

                List<Item> cart = (List<Item>)Session["cart"];
                int index = IsExisting(id);
                if (index == -1)
                {
                    cart.Add(new Item(db.Products.Find(id), 1));
                    
                }
                    

                else
                    cart[index].Qty++;
                Session["cart"] = cart;
                
            }
            return View("OrderNow");
        }

        public ActionResult UpdateOrder(FormCollection fc)
        {
            string[] qty = fc.GetValues("Qty");
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].Qty = Convert.ToInt32(qty[i]);
               
            }
            Session["cart"] = cart;
            //Session["count"] = Convert.ToInt32(Session["count"])+Convert.ToInt32(qty);
            return View("OrderNow");
        }
        
        public ActionResult Checkout()
        {
            int c = (int)Session["count"];
            if (c <= 0)
            {
                return RedirectToAction("EmptyOrder","Public");
            }
            else
            {
                if (Session["id"].ToString() == "")
                {
                    return RedirectToAction("Login", "MyAccount");
                }
                else
                {
                    ViewBag.cityId = new SelectList(db.Cities, "id", "name");
                    ViewBag.paymentMethodId = new SelectList(db.PaymentMethods, "id", "name");
                    return View();
                }
            } 
        }
        
        public ActionResult ConfirmOrder(FormCollection fc,ProductStock productStock)
        {  
            List<Item> cart = (List<Item>)Session["cart"];
            
            Order order=new Order();
            order.dateTime = DateTime.Now;
            order.number = fc["number"].ToString();
            order.userId = (int)Session["id"];
            order.deliveryCharge = (float)fc["Delivery"].AsFloat();
            order.otherCharge = 0;
            order.total = (float)fc["total"].AsFloat();
            order.paymentMethodId = fc["paymentMethodId"].AsInt();
            order.deliveryAddress = fc["deliveryAddress"].ToString();
            order.cityId = fc["cityId"].AsInt();
            order.status = "pending";
            try
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("MessageError", "Public");
            }
            


            int lastorderid = db.Orders.Max(item => item.id);
            foreach (Item iteam in cart)
            {
               OrderDetail orderDetail= new OrderDetail();
                orderDetail.orderId = lastorderid;
                orderDetail.productId = iteam.Pr.id;
                orderDetail.quantity = iteam.Qty;
                orderDetail.rate = iteam.Pr.price;
                orderDetail.remarks = "We will in form you as soon as possible";
                try
                {
                    db.OrderDetails.Add(orderDetail);
                    var v = db.Products.Where(p => p.id == iteam.Pr.id).First();
                    v.discount -= iteam.Qty;
                    db.Entry(v).State = EntityState.Modified;

                    db.SaveChanges();
                   
                }
                catch
                {
                    return RedirectToAction("MessageError", "Public");
                }
                
            }
            
            Session["cart"]=null;
            Session["count"] =null;
            

            return RedirectToAction("OrderSuccess", "Public");
        }
        
         public ActionResult Myorder()
        {
            if (Session["cart"]==null || Session["count"]==null)
            {
                try
                {
                    return RedirectToAction("Myorder", "Public");
                }
                catch (Exception)
                {
                    return RedirectToAction("MessageError", "Public");
                    
                }
                
                

            }
            else
            {
                try
                {
                    return RedirectToAction("Myorder", "Public");
                    //return View((List<Item>)Session["cart"]);
                }
                catch (Exception)
                {
                    return RedirectToAction("MessageError", "Public");
                }
                
            }
            
        }
        public ViewResult EmptyOrder()
        {
            ViewBag.message = "Your Cart is Empty";
            return View();
        }
       

        public ViewResult MessageError()
        {
            return View();
        }
        public ViewResult OrderSuccess()
        {
            return View();
        }
        public ViewResult CustomerLogin()
        {
            return View();
        }



        //sfa routiing problem solution
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
        public ActionResult ArticleShow(string search = "", int page = 1)
        {
            var articles = db.Articles.ToList();
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

        public ActionResult ArticleDetails(int serial, string comment = "", int add = 0)
        {
           
            if (comment != "")
            {
                int userId = (int)Session["id"];
                db.ArticleComments.Add(new ArticleComment() { articleId = serial, userId = userId, comment = comment, commentDate = DateTime.Now });
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Index", "Public");
                }
            }

            Article article = db.Articles.Find(serial);
           
            return View(article);
        }

    }
}