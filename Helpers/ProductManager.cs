using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven_Project.Models;
using Sparrow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Raven.Client.Constants.Documents.PeriodicBackup;
using static Raven_Project.Helpers.Indexes;

namespace Raven_Project
{
    public class ProductManager
    {
        private readonly IDocumentStore _store;
        public ProductManager(IDocumentStore store)
        {
            _store = store;
            IndexCreation.CreateIndexes(typeof(ProductIndex).Assembly, _store);
            IndexCreation.CreateIndexes(typeof(OrderIndex).Assembly, _store);
            IndexCreation.CreateIndexes(typeof(CustomerIndex).Assembly, _store);
            IndexCreation.CreateIndexes(typeof(Product_Search).Assembly, _store);
            IndexCreation.CreateIndexes(typeof(Customers_TotalOrders).Assembly, _store);
            IndexCreation.CreateIndexes(typeof(Orders_TotalPrice).Assembly, _store);
        }
        #region Product
        public void CreateProduct(Product product)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(product);
                session.SaveChanges();
            }
        }
        public void DeleteProduct(string productId)
        {
            using (var session = _store.OpenSession())
            {
                var product = session.Load<Product>(productId);
                if (product != null)
                {
                    session.Delete(product);
                    session.SaveChanges();
                }
            }
        }
        public void UpdateProduct(string productId, Product updatedProduct)
        {
            using (var session = _store.OpenSession())
            {
                var product = session.Load<Product>(productId);
                if (product != null)
                {
                    product.Name = updatedProduct.Name;
                    product.Price = updatedProduct.Price;
                    session.SaveChanges();
                    Console.WriteLine("Product updated successfully.");
                }
                else
                    Console.WriteLine("Product not found.");
            }
        }
        public void ListAllProducts()
        {
            using (var session = _store.OpenSession())
            {
                var products = session.Query<Product>().ToList();
                Console.WriteLine("Product List:");
                Console.WriteLine("ID  -> NAME  -> PRICE ");
                foreach (var product in products)
                    Console.WriteLine($"{product.Id}  -> {product.Name}  -> {product.Price}zl");
            }
        }
        public void ListAllProductsSortedByName()
        {
            var productsSortedByName = GetProductsSortedByName();
            if (productsSortedByName.Any())
            {
                Console.WriteLine("Products sorted by name:");
                Console.WriteLine("Product List:");
                Console.WriteLine("ID  -> NAME  -> PRICE ");
                foreach (var product in productsSortedByName)
                    Console.WriteLine($"{product.Id}  -> {product.Name}  -> {product.Price}zl");
            }
            else
                Console.WriteLine("No products found.");
            
        }
        public void ListAllProductsSortedByPrice()
        {
            var productsSortedByPrice = GetProductsSortedByPrice();

            if (productsSortedByPrice.Any())
            {
                Console.WriteLine("Products sorted by price:");
                Console.WriteLine("Product List:");
                Console.WriteLine("ID  -> NAME  -> PRICE ");
                foreach (var product in productsSortedByPrice)
                    Console.WriteLine($"{product.Id}  -> {product.Name}  -> {product.Price}zl");
            }
            else
                Console.WriteLine("No products found.");
            
        }
        public List<Product> GetProductsSortedByName()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Product>()
                              .OrderBy(p => p.Name)
                              .ToList();
            }
        }
        public List<Product> GetProductsSortedByPrice()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Product>()
                              .OrderBy(p => p.Price)
                              .ToList();
            }
        }
        public decimal AddProductToOrder(string productId, int quantity)
        {
            using (var session = _store.OpenSession())
            {
                var product = session.Load<Product>(productId);
                return product.Price * quantity;
            }
        }
        public List<Product> SearchProducts(string searchTerm)
        {
            using (var session = _store.OpenSession())
            {
                return session.Advanced.DocumentQuery<Product>().Search("Name", searchTerm).ToList();
            }
        }
        #endregion
        #region Order
        public void CreateOrder(Order order)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(order);
                session.SaveChanges();
            }
        }
        public void DeleteOrder(string orderId)
        {
            using (var session = _store.OpenSession())
            {
                var order = session.Load<Order>(orderId);
                if (order != null)
                {
                    session.Delete(order);
                    session.SaveChanges();
                }
            }
        }
        public void UpdateOrder(string orderId, Order updatedOrder)
        {
            using (var session = _store.OpenSession())
            {
                var order = session.Load<Order>(orderId);
                if (order != null)
                {
                    order.OrderNumber = updatedOrder.OrderNumber;
                    order.OrderDate = updatedOrder.OrderDate;
                    session.SaveChanges();
                    Console.WriteLine("Order updated successfully.");
                }
                else
                    Console.WriteLine("Order not found.");
            }
        }
        public void ListAllOrders()
        {
            using (var session = _store.OpenSession())
            {
                var orders = session.Query<Order>().ToList();
                Console.WriteLine("Order List:");
                Console.WriteLine("ID  -> ORDER NUMBER  -> ORDER DATE -> ORDER PRICE");
                Console.WriteLine("");
                foreach (var order in orders)
                    Console.WriteLine($"{order.Id}  -> {order.OrderNumber}  -> {order.OrderDate} -> {order.OrderPrice}zl");
            }
        }
        public void ListAllOrdersSorted()
        {
            var ordersSortedByPrice = GetOrdersSortedByPrice();

            if (ordersSortedByPrice.Any())
            {
                Console.WriteLine("Orders sorted by price:");
                Console.WriteLine("ID  -> ORDER NUMBER  -> ORDER DATE -> ORDER PRICE");
                Console.WriteLine("");
                foreach (var order in ordersSortedByPrice)
                    Console.WriteLine($"{order.Id}  -> {order.OrderNumber}  -> {order.OrderDate} -> {order.OrderPrice}zl");
            }
            else
                Console.WriteLine("No orders found.");
        }
        public List<Order> GetOrdersSortedByPrice()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Order>()
                              .OrderBy(o => o.OrderPrice)
                              .ToList();
            }
        }
        #endregion
        #region Customer
        public void CreateCustomer(Customer customer)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(customer);
                session.SaveChanges();
            }
        }
        public void DeleteCustomer(string customerId)
        {
            using (var session = _store.OpenSession())
            {
                var customer = session.Load<Customer>(customerId);
                if (customer != null)
                {
                    session.Delete(customer);
                    session.SaveChanges();
                }
            }
        }
        public void UpdateCustomer(string customerId, Customer updatedCustomer)
        {
            using (var session = _store.OpenSession())
            {
                var customer = session.Load<Customer>(customerId);
                if (customer != null)
                {
                    customer.Name = updatedCustomer.Name;
                    customer.Email = updatedCustomer.Email;
                    session.SaveChanges();
                    Console.WriteLine("Customer updated successfully.");
                }
                else
                    Console.WriteLine("Customer not found.");
            }
        }
        public void ListAllCustomers()
        {
            using (var session = _store.OpenSession())
            {
                var customers = session.Query<Customer>().ToList();
                Console.WriteLine("Customer List:");
                Console.WriteLine("ID  -> NAME  -> EMAIL  -> ORDERS ");
                Console.WriteLine("");
                foreach (var customer in customers)
                    Console.WriteLine($"{customer.Id} -> {customer.Name} -> {customer.Email} -> {customer.orders.Count()}");
            }
        }
        public Customer GetCustomerById(string customerId)
        {
            using (var session = _store.OpenSession())
            {
                return session.Load<Customer>(customerId);
            }
        }
        public void AddOrdersToCustomer(string customerId, List<string> orderIds)
        {
            using (var session = _store.OpenSession())
            {
                var customer = session.Load<Customer>(customerId);
                customer.orders.AddRange(orderIds);
                session.SaveChanges();
            }
        }
        #endregion
        #region Menu_Product
        public void Menu_Product()
        {
            Console.Clear();
            Console.WriteLine("Dawid Kułakowski || RavenDB Project CRUD");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("1 -- List products");
            Console.WriteLine("2 -- List products sorted by name");
            Console.WriteLine("3 -- List products sorted by price");
            Console.WriteLine("4 -- Create product");
            Console.WriteLine("5 -- Delete product");
            Console.WriteLine("6 -- Update product");
            Console.WriteLine("7 -- Query products by price range");
            Console.WriteLine("8 -- Perform paging on products");
            Console.WriteLine("9 -- Close");
        }
        public void CreMenu_Product()
        {
            ListAllProducts();
            Console.Write("Enter product name: ");
            var productName = Console.ReadLine();
            Console.Write("Enter product price: ");
            var productPriceInput = Console.ReadLine();
            if (decimal.TryParse(productPriceInput, out decimal productPrice))
            {
                var product = new Product
                {
                    Name = productName,
                    Price = productPrice
                };
                CreateProduct(product);
                Console.WriteLine("Product created successfully.");
            }
            else
                Console.WriteLine("Invalid price format.");
        }
        public void DelMenu_Product()
        {
            ListAllProducts();
            Console.Write("Enter product ID: ");
            var deleteProductId = "products/"+Console.ReadLine();
            DeleteProduct(deleteProductId);
            Console.WriteLine("Product deleted successfully.");
        }
        public void SeaMenu_Product()
        {
            Console.WriteLine("Enter name of products you are looking for:");
            string searchTerm = Console.ReadLine();
            List<Product> searchResults = SearchProducts(searchTerm);
            if (searchResults.Any())
            {
                Console.WriteLine($"Search results for '{searchTerm}':");
                foreach (var product in searchResults)
                    Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
            }
            else
                Console.WriteLine($"No search results found for '{searchTerm}'.");
        }
        public void UpdMenu_Product()
        {
            ListAllProducts();
            Console.Write("Enter product ID: ");
            var updateProductId = "products/"+Console.ReadLine();
            Console.Write("Enter new product name: ");
            var updatedProductName = Console.ReadLine();
            Console.Write("Enter new product price: ");
            var updatedProductPriceInput = Console.ReadLine();
            if (decimal.TryParse(updatedProductPriceInput, out decimal updatedProductPrice))
            {
                var updatedProduct = new Product
                {
                    Name = updatedProductName,
                    Price = updatedProductPrice
                };

                UpdateProduct(updateProductId, updatedProduct);
            }
            else
                Console.WriteLine("Invalid price format.");
        }
        public void QueryProductsByPrice()
        {
            Console.Write("Enter minimum price: ");
            var minPriceInput = Console.ReadLine();
            Console.Write("Enter maximum price: ");
            var maxPriceInput = Console.ReadLine();
            if (decimal.TryParse(minPriceInput, out decimal minPrice) && decimal.TryParse(maxPriceInput, out decimal maxPrice))
            {
                using (var session = _store.OpenSession())
                {
                    var query = session.Query<Product>().Where(p => p.Price >= minPrice && p.Price <= maxPrice);
                    var products = query.ToList();
                    foreach (var product in products)
                        Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
                }
            }
            else
                Console.WriteLine("Invalid price format.");
        }
        public void PerformPagingOnProducts()
        {
            Console.Write("Enter page number: ");
            var pageNumberInput = Console.ReadLine();
            Console.Write("Enter page size: ");
            var pageSizeInput = Console.ReadLine();
            if (int.TryParse(pageNumberInput, out int pageNumber) && int.TryParse(pageSizeInput, out int pageSize))
            {
                using (var session = _store.OpenSession())
                {
                    var query = session.Query<Product>().OrderBy(c => c.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    var products = query.ToList();
                    foreach (var product in products)
                        Console.WriteLine($"Product: {product.Name}, Price: {product.Price}");
                }
            }
            else
                Console.WriteLine("Invalid page number or page size.");
        }
        #endregion
        #region Menu_Order
        public void Menu_Order()
        {
            Console.Clear();
            Console.WriteLine("Dawid Kułakowski || RavenDB Project CRUD");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("1 -- List orders");
            Console.WriteLine("2 -- List orders sorted by overall price");
            Console.WriteLine("3 -- Create order");
            Console.WriteLine("4 -- Delete order");
            Console.WriteLine("5 -- Update order");
            Console.WriteLine("6 -- Query orders by date range");
            Console.WriteLine("7 -- Perform paging on orders");
            Console.WriteLine("8 -- Close");
        }
        public void CreMenu_Order()
        {
            
            Console.Write("Enter order number: ");
            var orderNumber = Console.ReadLine();
            ListAllProducts();
            Console.Write("Enter item Id: ");
            var productId = "products/" + Console.ReadLine();
            Console.Write("How many: ");
            int quantity = Int32.Parse(Console.ReadLine());
            var order = new Order
            {
                OrderNumber = orderNumber,
                OrderDate = DateTime.Now,
                OrderPrice = AddProductToOrder(productId, quantity)
            };
            CreateOrder(order);
            Console.WriteLine("Order created successfully.");
        }
        public void DelMenu_Order()
        {
            ListAllOrders();
            Console.Write("Enter order ID: ");
            var deleteOrderId = Console.ReadLine();
            DeleteOrder(deleteOrderId);
            Console.WriteLine("Order deleted successfully.");
        }
        public void UpdMenu_Order()
        {
            ListAllOrders();
            Console.Write("Enter order ID: ");
            var updateOrderId = Console.ReadLine();
            Console.Write("Enter new order number: ");
            var updatedOrderNumber = Console.ReadLine();
            Console.Write("Enter new order date (YYYY-MM-DD): ");
            var updatedOrderDateInput = Console.ReadLine();
            if (DateTime.TryParse(updatedOrderDateInput, out DateTime updatedOrderDate))
            {
                var updatedOrder = new Order
                {
                    OrderNumber = updatedOrderNumber,
                    OrderDate = updatedOrderDate
                };
                UpdateOrder(updateOrderId, updatedOrder);
            }
            else
                Console.WriteLine("Invalid date format.");
        }
        public void QueryOrdersByDate()
        {
            Console.Write("Enter from date (YYYY-MM-DD): ");
            var fromDateInput = Console.ReadLine();
            Console.Write("Enter to date (YYYY-MM-DD): ");
            var toDateInput = Console.ReadLine();
            if (DateTime.TryParse(fromDateInput, out DateTime fromDate) && DateTime.TryParse(toDateInput, out DateTime toDate))
            {
                using (var session = _store.OpenSession())
                {
                    var query = session.Query<Order>().Where(o => o.OrderDate >= fromDate && o.OrderDate <= toDate);
                    var orders = query.ToList();
                    foreach (var order in orders)
                        Console.WriteLine($"Order: {order.OrderNumber}, Date: {order.OrderDate}");
                }
            }
            else
                Console.WriteLine("Invalid date format.");
        }
        public void PerformPagingOnOrders()
        {
            Console.Write("Enter page number: ");
            var pageNumberInput = Console.ReadLine();
            Console.Write("Enter page size: ");
            var pageSizeInput = Console.ReadLine();
            if (int.TryParse(pageNumberInput, out int pageNumber) && int.TryParse(pageSizeInput, out int pageSize))
            {
                using (var session = _store.OpenSession())
                {
                    var query = session.Query<Order>().OrderBy(c => c.OrderNumber).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    var orders = query.ToList();
                    foreach (var order in orders)
                        Console.WriteLine($"Order Number: {order.OrderNumber}, Order Date: {order.OrderDate}, Order Price: {order.OrderPrice}");
                }
            }
            else
                Console.WriteLine("Invalid page number or page size.");
        }
        #endregion
        #region Menu_Customer
        public void Menu_Customer()
        {
            Console.Clear();
            Console.WriteLine("Dawid Kułakowski || RavenDB Project CRUD");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("1. List customers");
            Console.WriteLine("2. Create customer");
            Console.WriteLine("3. Delete customer");
            Console.WriteLine("4. Update customer");
            Console.WriteLine("5. Add orders for customer");
            Console.WriteLine("6. Perform paging on customers");
            Console.WriteLine("7. Close");
        }
        public void CreMenu_Customer()
        {
            ListAllCustomers();
            Console.Write("Enter customer name: ");
            var customerName = Console.ReadLine();
            Console.Write("Enter customer email: ");
            var customerEmail = Console.ReadLine();
            var customer = new Customer
            {
                Name = customerName,
                Email = customerEmail,
                orders = new List<string>()
            };
            CreateCustomer(customer);
            Console.WriteLine("Customer created successfully.");
        }
        public void DelMenu_Customer()
        {
            ListAllCustomers();
            Console.Write("Enter customer ID: ");
            var deleteCustomerId = Console.ReadLine();
            DeleteCustomer(deleteCustomerId);
            Console.WriteLine("Customer deleted successfully.");
        }
        public void UpdMenu_Customer()
        {
            ListAllCustomers();
            Console.Write("Enter customer ID: ");
            var updateCustomerId = Console.ReadLine();
            Console.Write("Enter new customer name: ");
            var updatedCustomerName = Console.ReadLine();
            Console.Write("Enter new customer email: ");
            var updatedCustomerEmail = Console.ReadLine();
            var updatedCustomer = new Customer
            {
                Name = updatedCustomerName,
                Email = updatedCustomerEmail
            };
            UpdateCustomer(updateCustomerId, updatedCustomer);
        }
        public void AddOrderMenu_Customer()
        {
            ListAllCustomers();
            Console.Write("Enter Customer Id: ");
            var customerId = "customers/" + Console.ReadLine();
            ListAllOrders();
            Console.Write("Enter Order Ids (comma-separated): ");
            var orderIdsInput = Console.ReadLine();
            var orderIds = orderIdsInput.Split(',').Select(id => id.Trim()).ToList();
            AddOrdersToCustomer(customerId, orderIds);
            Console.WriteLine("Orders added to Customer successfully.");
        }
        //paging
        public void PerformPagingOnCustomers()
        {
            Console.Write("Enter page number: ");
            var pageNumberInput = Console.ReadLine();
            Console.Write("Enter page size: ");
            var pageSizeInput = Console.ReadLine();
            if (int.TryParse(pageNumberInput, out int pageNumber) && int.TryParse(pageSizeInput, out int pageSize))
            {
                using (var session = _store.OpenSession())
                {
                    var query = session.Query<Customer>().OrderBy(c => c.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                    var customers = query.ToList();
                    foreach (var customer in customers)
                        Console.WriteLine($"Customer: {customer.Name}, Email: {customer.Email}");
                }
            }
            else
                Console.WriteLine("Invalid page number or page size.");
        }
        #endregion
    }
}
