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
    /// Interaction logic for CustomerForm.xaml
    /// </summary>
    public partial class CustomerForm : Window
    {

        private Customer customer;

        public CustomerForm()
        {
            InitializeComponent();
        }

        private void btnUpdateInfo_Clicked(object sender, RoutedEventArgs e)
        {
            CustomerUpdateInfo form = new CustomerUpdateInfo();
            form.SetCustomerForm(this);
            form.SetCustomer(customer);
            form.Show();
        }

        public void SetCustomer(Customer customer)
        {
            this.customer = customer;
            lbCustomerName.Content = customer.CustomerFullName;
            LoadBookingReservation();
        }

        public void LoadBookingReservation()
        {
            List<RoomInformation> roomInformations = RoomRepository.GetAllRooms();
            List<BookingDetail> bookingDetails = BookingDetailRepository.GetAllBookingDetail();
            List<BookingReservation> bookingReservations = BookingReservationRepository.GetBookingReservationsByCustomerID(customer.CustomerId);

            var reservation = from bookingReservation in bookingReservations
                              join bookingDetail in bookingDetails
                              on bookingReservation.BookingReservationId equals bookingDetail.BookingReservationId
                              join room in roomInformations
                              on bookingDetail.RoomId equals room.RoomId
                              select new
                              {
                                  BookingReservationID = bookingReservation.BookingReservationId,
                                  BookingDate = bookingReservation.BookingDate,
                                  StartDate = bookingDetail.StartDate,
                                  EndDate = bookingDetail.EndDate,
                                  RoomNumber = room.RoomNumber,
                                  TotalPrice = bookingReservation.TotalPrice,
                                  ActualPrice = bookingDetail.ActualPrice
                              };

            dgBookingReservation.ItemsSource = reservation;

        }

        private void btnLogout_Clicked(object sender, RoutedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}
