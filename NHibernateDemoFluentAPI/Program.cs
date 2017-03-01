using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateDemoFluentAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var customer = new FluentCustomer()
                    {
                        FirstName = "Allan",
                        LastName = "Boomer"
                    };

                    session.Save(customer);
                    transaction.Commit();

                    Console.Write("Customer Created : ");
                    Console.Write(customer.FirstName + "\t" + customer.LastName);
                }

                Console.ReadLine();
            }
        }
    }
}
