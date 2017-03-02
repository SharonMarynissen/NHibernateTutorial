using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL
{
    public interface IRepository
    {
        IEnumerable<Customer> ReadCustomers();
        Customer CreateCustomer(Customer customer);
        Customer ReadCustomer(Guid id);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Guid id);

        IEnumerable<Order> ReadOrder(Guid id);
        Order CreateOrder(Guid id, Order order);
    }
}
