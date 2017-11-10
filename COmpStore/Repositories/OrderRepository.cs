using COmpStore.Schema;
using COmpStore.Schema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.Repositories
{
    public interface IOrderRepository
    {
        bool Add(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private StoreDbContext DbContext;

        public OrderRepository(StoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public bool Add(Order order)
        {
            try
            {
                DbContext.Orders.Add(order);
                DbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
