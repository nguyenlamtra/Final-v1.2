using COmpStore.Schema.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Schema.Entities
{
    public class Order : BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual User User { get; set; }
        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
