using SmartFarmingAssistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartFarmingAssistant.View_Model
{
    public class Report
    {
        public Order order { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual City City { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual User User { get; set; }
    }
}