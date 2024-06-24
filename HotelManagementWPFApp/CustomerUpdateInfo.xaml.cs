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
    /// Interaction logic for CustomerUpdateInfo.xaml
    /// </summary>
    public partial class CustomerUpdateInfo : Window
    {

        private CustomerForm form;
        private Customer customer;

        public CustomerUpdateInfo()
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
            form.lbCustomerName.Content = customer.CustomerFullName;
            this.Close();
        }

        private void btnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SetCustomerForm(CustomerForm form)
        {
            this.form = form;
        }

        public void SetCustomer(Customer customer)
        {
            this.customer = customer;
            LoadCustomerInfo();
        }

        private void LoadCustomerInfo()
        {
            txtName.Text = customer.CustomerFullName;
            txtDBirthDate.Text = customer.CustomerBirthday.ToString();
            txtEmail.Text = customer.EmailAddress;
            txtPhone.Text = customer.Telephone;
            txtPassword.Text = customer.Password;
        }

    }
}
