using Library.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Library.Repositories
{
    public class CustomersRepository
    {
        public IEnumerable<Customer> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.Customer.ToList();
            }
        }

        public Customer Get(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Customer.SingleOrDefault(x => x.Id == id);
            }
        }

        public int Add(Customer customer)
        {
            using (var context = new LibraryContext())
            {
                context.Customer.Add(customer);
                context.SaveChanges();
                return customer.Id;
            }
        }

        public void Update(Customer customer)
        {
            using (var context = new LibraryContext())
            {
                var originalCustomer = context.Customer.Find(customer.Id);
                context.Entry(originalCustomer).CurrentValues.SetValues(customer);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new LibraryContext())
            {
                var customerToBeDeleted = context.Customer.Find(id);
                context.Customer.Remove(customerToBeDeleted);
                context.SaveChanges();
            }
        }
    }
}
