using Raven.Client.Documents.Indexes;
using Raven_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raven_Project.Helpers
{
    public class Indexes
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
        public class Product_Search : AbstractIndexCreationTask<Product>
        {
            public Product_Search()
            {
                Map = products => from product in products
                                  select new
                                  {
                                      product.Name
                                  };

                Indexes.Add(x => x.Name, FieldIndexing.Search);
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
        public class Orders_TotalPrice : AbstractIndexCreationTask<Order, Orders_TotalPrice.Result>
        {
            public class Result
            {
                public string OrderId { get; set; }
                public decimal TotalPrice { get; set; }
            }
            public Orders_TotalPrice()
            {
                Map = orders => from order in orders
                                select new
                                {
                                    OrderId = order.Id,
                                    TotalPrice = order.OrderPrice
                                };
            }
        }

        public class Customers_TotalOrders : AbstractIndexCreationTask<Order, Customers_TotalOrders.Result>
        {
            public class Result
            {
                public string CustomerId { get; set; }
                public int TotalOrders { get; set; }
            }
            public Customers_TotalOrders()
            {
                Map = orders => from order in orders
                                select new
                                {
                                    CustomerId = order.Id,
                                    TotalOrders = 1
                                };

                Reduce = results => from result in results
                                    group result by result.CustomerId into g
                                    select new
                                    {
                                        CustomerId = g.Key,
                                        TotalOrders = g.Sum(x => x.TotalOrders)
                                    };
            }
        }
    }
}
