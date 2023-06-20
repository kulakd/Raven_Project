using Raven.Client.Documents.Indexes;
using Raven_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raven_Project.Helpers
{
    internal class Indexes
    {
        public class ProductIndex : AbstractIndexCreationTask<Product>
        {
            public ProductIndex()
            {
                Map = products => from product in products
                                  select new
                                  {
                                      product.Name,
                                      product.Price
                                  };
            }
        }
        public class OrderIndex : AbstractIndexCreationTask<Order>
        {
            public OrderIndex()
            {
                Map = orders => from order in orders
                                select new
                                {
                                    order.OrderNumber,
                                    order.OrderDate
                                };
            }
        }
        public class CustomerIndex : AbstractIndexCreationTask<Customer>
        {
            public CustomerIndex()
            {
                Map = customers => from customer in customers
                                   select new
                                   {
                                       customer.Name,
                                       customer.Email
                                   };
            }
        }
    }
}
