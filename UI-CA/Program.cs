using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DAL;

namespace UI_CA
{
    internal class Program
    {
        private static bool quit = false;
        private static readonly IRepository repo = new Repository();

        static void Main(string[] args)
        {
            while (!quit)
                ShowMenu();
        }

        private static void ShowMenu()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("=== CUSTOMER AND ORDER ===");
            Console.WriteLine("=================================");
            Console.WriteLine("1) Toon alle klanten");
            Console.WriteLine("2) Toon de orders van een klant");
            Console.WriteLine("3) Maak een nieuwe klant");
            Console.WriteLine("4) Geef een order in voor een klant");
            Console.WriteLine("0) Afsluiten");
            try
            {
                DetectMenuAction();
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Er heeft zich een onverwachte fout voorgedaan!");
                Console.WriteLine();
            }
        }

        private static void DetectMenuAction()
        {
            bool inValidAction = false;
            do
            {
                Console.Write("Keuze: ");
                string input = Console.ReadLine();
                int action;
                if (Int32.TryParse(input, out action))
                {
                    switch (action)
                    {
                        case 1:
                            ShowAllCustomers(); break;
                        case 2:
                            ActionShowOrders(); break;
                        case 3:
                            ActionCreateCustomer(); break;
                        case 4:
                            ActionAddOrderToCustomer(); break;
                        case 0:
                            quit = true;
                            return;
                        default:
                            Console.WriteLine("Geen geldige keuze!");
                            inValidAction = true;
                            break;
                    }
                }
            } while (inValidAction);
        }

        private static void ActionAddOrderToCustomer()
        {
            ShowAllCustomers();

            Console.Write("Klantennummer: ");
            Guid customerId = Guid.Parse(Console.ReadLine());

            Console.Write("Huisnummer: ");
            int nbr = Int32.Parse(Console.ReadLine());
            Console.Write("Stad: ");
            string city = Console.ReadLine();
            Console.Write("Land: ");
            string country = Console.ReadLine();

            Order order = new Order()
            {
                Ordered = DateTime.Now,
                ShipTo = new Location()
                {
                    Street = "SomewhereStreet " + nbr,
                    City = city,
                    Country = country
                }
            };

            repo.CreateOrder(customerId, order);


        }

        private static void ActionCreateCustomer()
        {
            Console.Write("Voornaam: ");
            string firstName = Console.ReadLine();
            Console.Write("Achternaam: ");
            string lastName = Console.ReadLine();
            Console.Write("Huisnummer: ");
            int nbr = Int32.Parse(Console.ReadLine());
            Console.Write("Stad: ");
            string city = Console.ReadLine();
            Console.Write("Land: ");
            string country = Console.ReadLine();

            Customer customer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                AverageRating = 14.26,
                Address = new Location()
                {
                    Street = "SomewhereStreet " + nbr,
                    City = city,
                    Country = country
                },
                Points = 155,
                HasGoldStatus = false
            };

            repo.CreateCustomer(customer);
        }

        private static void ActionShowOrders()
        {
            ShowAllCustomers();

            Console.Write("Klantennummer van wie je de orders wil zien: ");
            Guid customerId = Guid.Parse(Console.ReadLine());
            IEnumerable<Order> orders = repo.ReadOrder(customerId);

            foreach (Order order in orders.ToList())
                Console.WriteLine(order);
        }

        private static void ShowAllCustomers()
        {
            IEnumerable<Customer> customers = repo.ReadCustomers();
            
            foreach (Customer cust in customers.ToList())
                Console.WriteLine(cust);
        }
    }
}
 