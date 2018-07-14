using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarmingAssistant.Models
{
    public class Item
    {

        private Product pr=new Product();
       private ProductStock ps=new ProductStock();

        public ProductStock Ps
        {
            get { return ps; }
            set { ps = value; }
        }

        public Product Pr
        {
            get { return pr; }
            set { pr = value; }
        }

        private int qty;

        public int Qty
        {
            get { return qty; }
            set { qty = value; }
        }

        public Item()
        {

        }

        public Item(Product pr, int qty)
        {
            this.pr = pr;
            this.qty = qty;
        }
    }
    
}