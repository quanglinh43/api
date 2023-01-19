using Employee.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Service
{
    public interface ICustomersList
    {
        public List<Customers> GetCustomers();

        public Customers AddCustomer(Customers customers);

        public Customers UpdateCustomer(string id, Customers customer);

        public string DeleteCustomer(string id);
    }
}
