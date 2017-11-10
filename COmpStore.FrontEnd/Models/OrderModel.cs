using COmpStore.FrontEnd.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.FrontEnd.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }

        public UserModel User {get;set;}
        public virtual IEnumerable<OrderDetailModel> OrderDetails { get; set; }
    }
}
