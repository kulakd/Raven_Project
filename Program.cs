using Raven.Client.Documents;
using Raven.Client.ServerWide.Operations;
using Raven_Project;
using Raven_Project.Helpers;
using Raven_Project.Models;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        using (var store = new DocumentStore
        {
            Urls = new[] { "http://localhost:8080" },
            Database = "Project"
        }.Initialize())
        {
            var ravenDbManager = new ProductManager(store);
            Console.WriteLine("Dawid Kułakowski || RavenDB Project CRUD");
            Console.WriteLine("------------------------------------------");
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 -- Products Menu");
                Console.WriteLine("2 -- Orders Menu");
                Console.WriteLine("3 -- Customers Menu");
                Console.WriteLine("4 -- Close Application");
                Console.WriteLine("Enter option number:");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ravenDbManager.Menu_Product();
                        var choice_p = Console.ReadLine();
                        switch (choice_p)
                        {
                            case "1":
                                ravenDbManager.ListAllProducts();
                                Console.WriteLine();
                                break;
                            case "2":
                                ravenDbManager.ListAllProductsSortedByName();
                                Console.WriteLine();
                                break;
                            case "3":
                                ravenDbManager.ListAllProductsSortedByPrice();
                                Console.WriteLine();
                                break;
                            case "4":
                                ravenDbManager.CreMenu_Product();
                                break;
                            case "5":
                                ravenDbManager.DelMenu_Product();
                                break;
                            case "6":
                                ravenDbManager.UpdMenu_Product();
                                break;
                            case "7":
                                ravenDbManager.QueryProductsByPrice();
                                break;
                            case "8":
                                ravenDbManager.PerformPagingOnProducts();
                                break;
                            case "9":
                                Console.WriteLine("");
                                return;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                        break;
                    case "2":
                        ravenDbManager.Menu_Order();
                        var choice_o = Console.ReadLine();
                        switch (choice_o)
                        {
                            case "1":
                                ravenDbManager.ListAllOrders();
                                Console.WriteLine();
                                break;
                            case "2":
                                ravenDbManager.ListAllOrdersSorted();
                                Console.WriteLine();
                                break;
                            case "3":
                                ravenDbManager.CreMenu_Order();
                                break;
                            case "4":
                                ravenDbManager.DelMenu_Order();
                                break;
                            case "5":
                                ravenDbManager.UpdMenu_Order();
                                break;
                            case "6":
                                ravenDbManager.QueryOrdersByDate();
                                break;
                            case "7":
                                ravenDbManager.PerformPagingOnOrders();
                                break;
                            case "8":
                                Console.WriteLine("");
                                return;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                        break;
                    case "3":
                        ravenDbManager.Menu_Customer();
                        var choice_c = Console.ReadLine();
                        switch (choice_c)
                        {
                            case "1":
                                ravenDbManager.ListAllCustomers();
                                Console.WriteLine();
                                break;
                            case "2":
                                ravenDbManager.CreMenu_Customer();
                                break;
                            case "3":
                                ravenDbManager.DelMenu_Customer();
                                break;
                            case "4":
                                ravenDbManager.UpdMenu_Customer();
                                break;
                            case "5":
                                ravenDbManager.AddOrderMenu_Customer();
                                break;
                            case "6":
                                ravenDbManager.PerformPagingOnCustomers();
                                break;
                            case "7":
                                Console.WriteLine("");
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                        break;
                    case "4":
                        Console.WriteLine("Exiting the application...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}