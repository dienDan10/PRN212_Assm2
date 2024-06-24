using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CustomerRepository
    {
        private static CustomerDAO customerDAO = new CustomerDAO();

        public static List<Customer> GetAllCustomers()
        {
            return customerDAO.GetAllCustomers();
        }

        public static Customer GetCustomerById(int id)
        {
            return customerDAO.getCustomerById(id);
        }

        public static Customer GetAdmin()
        {
            return customerDAO.GetAdmin();
        }

        public static void SaveCustomer(Customer customer)
        {
            customerDAO.AddCustomer(customer);
        }

        public static void DeleteCustomer(int id)
        {
            customerDAO.DeleteCustomer(id);
        }

        public static void UpdateCustomer(Customer customer)
        {
            customerDAO.UpdateCustomer(customer);
        }

        public static List<Customer> GetCustomersByName(string name)
        {
            return customerDAO.GetCustomersByName(name);
        }

        public static Customer GetCustomerByEmail(string email)
        {
            return customerDAO.GetCustomerByEmail(email);
        }

    }
}
