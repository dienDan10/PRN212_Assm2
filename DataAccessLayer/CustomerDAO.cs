using BussinessObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerDAO : DBUtils
    {

        private FuminiHotelManagementContext context = new FuminiHotelManagementContext();

        public  List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                customers = context.Customers.Select(c => c).ToList();
            } catch(Exception e) {
                Console.WriteLine("Cannot get all customer");
            }
            return customers;
        }

        public Customer GetAdmin()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            string email = configuration["AdminAccount:Email"];
            string password = configuration["AdminAccount:Password"];
            return new Customer { EmailAddress = email, Password = password };

        }

        public  Customer getCustomerById(int id)
        {
            Customer customer = null;
            try
            {
                customer = context.Customers.Single(c => c.CustomerId == id);
            } catch(Exception e)
            {
                Console.WriteLine("Cannot get customer");
            }
            return customer;
        }

        public Customer GetCustomerByEmail(string email)
        {

            Customer customer = null;
            try
            {
                customer = context.Customers.Single(c => c.EmailAddress == email);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot get customer");
            }
            return customer;
        }

        public  void AddCustomer(Customer customer)
        { 

            try
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public  void UpdateCustomer(Customer customer)
        { 

            try
            {
               context.Customers.Update(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public  void DeleteCustomer(int id) {
           
            try
            {
                Customer customer = context.Customers.FirstOrDefault(c => c.CustomerId == id);
                context.Customers.Remove(customer);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public List<Customer> GetCustomersByName(string name)
        {

            List<Customer> customers = new List<Customer>();
            try
            {
                customers = context.Customers.Where(c => c.CustomerFullName.ToLower().Contains(name.ToLower())).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot get customer");
            }
            return customers;
        }

    }
}
