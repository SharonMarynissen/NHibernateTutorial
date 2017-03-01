using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace NHibernateDemoCustomer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            var sessionFactory = cfg.BuildSessionFactory();

            //Guid id;

            using (var session = sessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    //        Customer newCustomer = CreateCustomer();
                    //        Console.WriteLine("New Customer:");
                    //        Console.WriteLine(newCustomer);
                    //        session.Save(newCustomer);

                    //        //will be done by adding cascade option, so no need to do this manualy
                    //        //foreach (var order in newCustomer.Orders)
                    //        //{
                    //        //    //Every order also has to be saved to the db
                    //        //    session.Save(order);
                    //        //}

                    //        id = newCustomer.Id;
                    //        tx.Commit();
                    //    }
                    //}

                    //using (var session = sessionFactory.OpenSession())
                    //{
                    //    using (var tx = session.BeginTransaction())
                    //    {
                    //        //var reloaded = session.Load<Customer>(id); //load by custumer

                    //        //Using a query to use an eager join fetch >> remove the fetching strategy from customer.hbm.xml
                    //        var query = from customer in session.Query<Customer>()
                    //            where customer.Id == id
                    //            select customer;
                    //        var reloaded = query.Fetch(x => x.Orders).ToList().First();

                    //        Console.WriteLine("Reloaded:");
                    //        Console.WriteLine(reloaded);
                    //        Console.WriteLine("The orders were ordered by: ");

                    //        foreach (var order in reloaded.Orders)
                    //        {
                    //            Console.WriteLine(order.Customer);
                    //        }


                    //Demonstrating difference between get and load
                    //id1 is an existing id in db, id2 isn't
                    //First customer will be printed on the console
                    //Second customer will be empty (null)
                    //Both customers are from the type Customer
                    //Get will go to db immediatly and the value of the object will be null if it can't be found in db
                    //var id1 = Guid.Parse("9906edd0-e14e-401c-ba1e-a728014cde2a");
                    //var id2 = Guid.Parse("AAAAAAAA-BBBB-CCCC-DDDD-EEEEEEEEEEEE");

                    //Console.WriteLine("Loading customers from db using get:");                  
                    //var customer1 = session.Get<Customer>(id1);
                    //Console.WriteLine("Customer1 data:");
                    //Console.WriteLine(customer1);

                    //var customer2 = session.Get<Customer>(id2);
                    //Console.WriteLine("Customer2 data:");
                    //Console.WriteLine(customer2);


                    //Customer3 will be loaded and printed out on the console
                    //Customer4 will throw an ObjectNotFoundException
                    //Load uses proxy objects and only goes to db when u want to print customer >>
                    //Customer4 is a non excisting id so it will not be found in db.. Proxy object can 
                    //not be replaced by a null value so it will throw an ObjectNotFoundException
                    //Load can optimize db round trips more efficiently
                    //Console.WriteLine("\nLoading customers from db using load:");
                    //var customer3 = session.Load<Customer>(id1);
                    //Console.WriteLine("Customer1 data:");
                    //Console.WriteLine(customer3);

                    //var customer4 = session.Load<Customer>(id2);
                    //Console.WriteLine("Customer2 data:");
                    //Console.WriteLine(customer4);


                    //Linq: chainig syntax
                    //var custmer = session.Query<Customer>().Where(c => c.FirstName == "John").First();
                    //Console.WriteLine(custmer);

                    //var customers = session.Query<Customer>().Where(c => c.FirstName.StartsWith("J"));
                    //foreach (var cust in customers.ToList())
                    //{
                    //    Console.WriteLine(cust);
                    //}

                    //Linq: comprehension syntax: looks more loke sql using the from, where and select keywords
                    //Console.WriteLine("=============================================");
                    //Console.WriteLine("Comprehension syntax:");
                    //Console.WriteLine("=============================================\n");
                    //var customer = (from c in session.Query<Customer>() where c.FirstName == "John" select c).First();
                    //Console.WriteLine(customer);

                    //var customerss = from c in session.Query<Customer>() where c.FirstName.StartsWith("J") select c;
                    //foreach (var cust in customerss.ToList())
                    //    Console.WriteLine(cust);

                    //HQL: Hibernate Query Language: shared accros Java's Hibernate and NHibernate
                    //Acces to HQL by calling the session.createQuery and pass as a parameter using an HQL string
                    //You only know wether it is a valid HQL until runtime
                    //FirstName is the name of the property, not the column name! Remember when names don't match!
                    //Customer is not the table name, but the class you are collecting from
                    //var cus = session.CreateQuery("select c from Customer c where c.FirstName = 'John'");
                    //foreach (var cust in cus.List<Customer>())
                    //    Console.WriteLine(cust);

                    //var customer = session.CreateQuery("select c from Customer c where c.FirstName like 'J%'");
                    //foreach (var cust in customer.List<Customer>())
                    //    Console.WriteLine(cust);

                    //var bigOrderCust = session.CreateQuery("select c from Customer c where size(c.Orders) > 1");
                    //var bigOrderCust = session.CreateQuery("select c from Customer c where c.Orders.size > 1");
                    //foreach (var cust in bigOrderCust.List<Customer>())
                    //    Console.WriteLine(cust);



                    //Criteria query: dynamicly adding a restriction object to that query
                    //Customers who's first name start with 'J'
                    //var customers = session.CreateCriteria<Customer>().Add(Restrictions.Like("FirstName", "J%"));
                    //Customers who's first name equals to 'John'
                    //var customers = session.CreateCriteria<Customer>().Add(Restrictions.Eq("FirstName", "John"));

                    //foreach (var cust in customers.List<Customer>())
                    //    Console.WriteLine(cust);



                    //LINQ: still uses the criteria under the covers
                    //Queries are strongly typed
                    //If for ex FirstName gets refactored it changes to
                    //Query comprehension syntax can not be used, but method chain syntax
                    //You can't mix and match the link and the criteria
                    //var customers = session.QueryOver<Customer>().Where(x => x.FirstName == "John");

                    //foreach (var cust in customers.List())
                    //    Console.WriteLine(cust);

                    //To use the LIKE operator in SQL you need to create a restriction into the WHERE clause
                    //var customers = session.QueryOver<Customer>()
                    //    .Where(Restrictions.On<Customer>(c => c.FirstName).IsLike("J%"));



                    //Using native SQL statements instead of queries
                    //CreateSQLQuery() will give back a list of objects and
                    //the root entity type you want the query to return is specified as Customer
                    //IQuery sqlQuery = session.CreateSQLQuery(
                    //        "SELECT * FROM CUSTOMER").AddEntity(typeof(Customer));
                    //var customers = sqlQuery.List<Customer>();

                    //Specifing the columns and types to return >> will return a IList of Object arrays with
                    //scalar values for each column in the Customer table
                    //Will return ony 3 listed columns dispite the * 
                    //(Doens't work: can't save Object[] to Customer)
                    //IList<Customer> customers = session.CreateSQLQuery("SELECT * FROM CUSTOMER")
                    //       .AddScalar("Id", NHibernateUtil.Guid)
                    //       .AddScalar("FirstName", NHibernateUtil.String)
                    //       .AddScalar("LastName", NHibernateUtil.String).List<Customer>();

                    IList<Customer> customers =
                        session.CreateSQLQuery("SELECT * FROM CUSTOMER WHERE FirstName = 'John'")
                            .AddEntity(typeof(Customer)).List<Customer>();

                    foreach (var cust in customers)
                        Console.WriteLine(cust);

                    tx.Commit();
                }

                Console.WriteLine("Press <ENTER> to exit...");
                Console.ReadLine();
            }

        }

        private static Customer CreateCustomer()
        {
            Customer customer = new Customer()
            {
                FirstName = "John",
                LastName = "Doe",
                Points = 100,
                HasGoldStatus = true,
                MemberSince = new DateTime(2012, 1, 1),
                CreditRating = CustomerCreditRating.Good,
                AverageRating = 42.42424242,
                Address = CreateLocation()
            };

            Order order1 = new Order()
            {
                Ordered = DateTime.Now
            };

            Order order2 = new Order()
            {
                Ordered = DateTime.Now.AddDays(-1),
                Shipped = DateTime.Now,
                ShipTo = CreateLocation()
            };

            customer.AddOrder(order1);
            customer.AddOrder(order2);

            return customer;
        }

        private static Location CreateLocation()
        {
            return new Location()
            {
                Street = "123 Somewhere Avenue",
                City = "Nowhere",
                Province = "Alberta",
                Country = "Canada"
            };
        }
    }
}
 