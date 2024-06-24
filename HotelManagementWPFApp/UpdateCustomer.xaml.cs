using BussinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelManagementWPFApp
{
    /// <summary>
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Window
    {
        private Admin admin;
        private Customer customer;

        public UpdateCustomer()
        {
            InitializeComponent();
        }

        private void btnSave_Clicked(object sender, RoutedEventArgs e)
        {
            // validation
            string name = txtName.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            string birthDay = txtDBirthDate.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone)
                || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(birthDay) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all information");
                return;
            }
            // update customer info
            customer.CustomerFullName = name;
            customer.Telephone = phone;
            customer.EmailAddress = email;
            customer.Password = password;
            customer.CustomerBirthday = DateOnly.Parse(birthDay);
            // update customer
            CustomerRepository.UpdateCustomer(customer);
            // reload customer data grid
            admin.LoadCustomer();
            admin.LoadCustomerName();
            // close
            this.Close();
        }

        public void SetAdmin(Admin admin)
        {
            this.admin = admin;
        }

        private void btnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SetCustomer(Customer customer)
        {
            this.customer = customer;
            SetCustomerInfo();
        }

        private void SetCustomerInfo()
        {
            txtName.Text = customer.CustomerFullName;
            txtEmail.Text = customer.EmailAddress;
            txtDBirthDate.Text = customer.CustomerBirthday.ToString();
            txtPassword.Text = customer.Password;
            txtPhone.Text = customer.Telephone;
        }

    }
}
