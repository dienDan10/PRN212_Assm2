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
    /// Interaction logic for AddCustomerForm.xaml
    /// </summary>
    public partial class AddCustomerForm : Window
    {
        private Admin admin;

        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private void btnSave_Clicked(object sender, RoutedEventArgs e)
        {
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

            Customer customer = new Customer() { 
                CustomerFullName = name,
                CustomerBirthday = DateOnly.Parse(birthDay),
                Password = password,
                CustomerStatus = 1,
                Telephone = phone,
                EmailAddress = email,
            };

            CustomerRepository.SaveCustomer(customer);
            admin.LoadCustomer();
            admin.LoadCustomerName();
            this.Close();
        }

        private void btnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SetAdminForm(Admin admin)
        {
            this.admin = admin;
        }

    }
}
