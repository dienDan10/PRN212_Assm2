using BussinessObjects;
using DataAccessLayer;
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
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Clicked(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            // admin login
            Customer admin = CustomerRepository.GetAdmin();
            if (admin.EmailAddress == email && admin.Password == password)
            {
                Admin adminForm = new Admin();
                adminForm.Show();
                this.Close();
                return;
            }

            // get customer
            Customer customer = CustomerRepository.GetCustomerByEmail(email);
            if (customer == null || customer.Password != password) {
                MessageBox.Show("Email or Password incorrect");
                return;
            }

            // display customer form
            CustomerForm customerForm = new CustomerForm();
            customerForm.SetCustomer(customer);
            customerForm.Show();
            this.Close(); 
            return;
        }
    }
}
