using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NHibernate.Cfg;
using System.Reflection;
using NHibernate;
using NHibernate.Linq;

namespace DAL
{
    public class Repository : IRepository
    {
        private static ISession Session;

        public Repository()
        {
            Configuration cfg = new Configuration();
            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            Session = cfg.BuildSessionFactory().OpenSession();
        }

        public Customer CreateCustomer(Customer customer)
        {
            using (var tx = Session.BeginTransaction())
            {
                Session.Save(customer);
                tx.Commit();
                return customer;
            }
        }

        public IEnumerable<Customer> ReadCustomers()
        {
            using (var tx = Session.BeginTransaction())
            {
                IEnumerable<Customer> customers = Session.Query<Customer>();
                tx.Commit();
                return customers;
            }            
        }

        public Customer ReadCustomer(Guid id)
        {
            using (var tx = Session.BeginTransaction())
            {
                Customer customer = Session.Get<Customer>(id);
                tx.Commit();
                return customer;
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var tx = Session.BeginTransaction())
            {
                Customer customerToUpdate = ReadCustomer(customer.Id);
                customerToUpdate.FirstName = customer.FirstName;
                customerToUpdate.LastName = customer.LastName;
                customerToUpdate.Address = customer.Address;
                customerToUpdate.AverageRating = customer.AverageRating;
                Session.Update(customerToUpdate);
                tx.Commit();
            }
        }

        public void DeleteCustomer(Guid id)
        {
            using (var tx = Session.BeginTransaction())
            {
                Customer customer = ReadCustomer(id);
                Session.Delete(customer);
            }
        }

        public IEnumerable<Order> ReadOrder(Guid customerId)
        {
            using (var tx = Session.BeginTransaction())
            {
                IEnumerable<Order> orders = Session.Query<Order>().Where(c => c.Id == customerId);
                tx.Commit();
                return orders;
            }
        }

        public Order CreateOrder(Guid id, Order order)
        {
            using (var tx = Session.BeginTransaction())
            {
                Customer customer = ReadCustomer(id);
                customer.AddOrder(order);
                tx.Commit();
                return order;
            }
        }
    }
}
