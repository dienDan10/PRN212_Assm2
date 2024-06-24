using BussinessObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {

        private int selectedCustomerId = -1;
        private int selectedRoomId = -1;
        private int selectedCustomerName = -1;
        private int selectedRoomName = -1;
        private int selectedBookingDetail = -1;

        public Admin()
        {
            InitializeComponent();
            LoadCustomer();
            LoadRoom();
            LoadCustomerName();
            LoadRoomName();
        }

        public void LoadCustomer()
        {
            try
            {
                List<Customer> customers = CustomerRepository.GetAllCustomers();
                dgCustomer.ItemsSource = null;
                dgCustomer.ItemsSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail to load customer");
            }

        }

        public void LoadRoom()
        {
            try
            {

                List<RoomInformation> rooms = RoomRepository.GetAllRooms();
                List<RoomType> types = RoomTypeRepository.GetAllRoomTypes();
                var list = from room in rooms
                           join type in types
                           on room.RoomTypeId equals type.RoomTypeId
                           select new
                           {
                               RoomID = room.RoomId,
                               RoomNumber = room.RoomNumber,
                               RoomType = type.RoomTypeName,
                               RoomDetail = room.RoomDetailDescription,
                               RoomPricePerDay = room.RoomPricePerDay,
                               RoomMaxCapacity = room.RoomMaxCapacity,
                               Status = room.RoomStatus
                           };
                dgRoom.ItemsSource = null;
                dgRoom.ItemsSource = list;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fail to load Room");
            }
        }

        public void LoadCustomerName()
        {
            try
            {
                var customers = CustomerRepository.GetAllCustomers()
                .Select(c => new
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerFullName
                });
                dgCustomerName.ItemsSource = null;
                dgCustomerName.ItemsSource = customers;
            }
            catch (Exception e)
            {
                MessageBox.Show("Fail to load customer name");
            }
        }

        public void LoadRoomName()
        {
            try
            {

                var rooms = RoomRepository.GetAllRooms()
                    .Select(r => new
                    {
                        RoomId = r.RoomId,
                        RoomNumber = r.RoomNumber
                    });
                dgRoomsName.ItemsSource = null;
                dgRoomsName.ItemsSource = rooms;

            }
            catch (Exception e)
            {
                MessageBox.Show("Fail to load room name");
            }
        }

        private void btnDeleteCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedCustomerId == -1)   // show warning
            {
                MessageBox.Show("Please choose a customer!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do You want to delete?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)  // don't delete
            {
                return;
            }

            CustomerRepository.DeleteCustomer(selectedCustomerId);
            selectedCustomerId = -1;
            LoadCustomer();
            LoadCustomerName();

        }

        private void dgCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomer.ItemsSource == null)
            {
                selectedCustomerId = -1;
                return;
            }
            if (dgCustomer.SelectedCells.Count == 0)
            {
                selectedCustomerId = -1;
                return;
            }

            // get dataGrid
            DataGrid dataGrid = sender as DataGrid;
            // get selected row
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgCustomer.SelectedIndex);
            // get the cell contain the index
            DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            string id = ((TextBlock)cell.Content).Text.ToString();
            selectedCustomerId = int.Parse(id);
        }

        private void btnAddCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            AddCustomerForm form = new AddCustomerForm();
            form.SetAdminForm(this);
            form.Show();
        }

        private void btnUpdateCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedCustomerId == -1)   // show warning
            {
                MessageBox.Show("Please choose a customer!");
                return;
            }
            // get the customer
            Customer customer = CustomerRepository.GetCustomerById(selectedCustomerId);
            // display update form
            UpdateCustomer updateCustomer = new UpdateCustomer();
            updateCustomer.SetAdmin(this);
            updateCustomer.SetCustomer(customer);
            updateCustomer.Show();

        }

        private void btnSearchCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            // validate the text field
            string input = txtSearchCustomer.Text;
            if (string.IsNullOrEmpty(input.Trim()))
            {
                MessageBox.Show("Please input something");
                return;
            }

            // search for a list of customer
            List<Customer> customers = CustomerRepository.GetCustomersByName(input);
            if (customers.Count == 0)
            {
                MessageBox.Show("No Customer with name = " + input);
                return;
            }

            // update customer data grid
            dgCustomer.ItemsSource = null;
            dgCustomer.ItemsSource = customers;

        }

        private void btnLoadCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            LoadCustomer();
        }

        private void btnLoadRoom_Clicked(object sender, RoutedEventArgs e)
        {
            LoadRoom();
        }

        private void btnAddRoom_Clicked(object sender, RoutedEventArgs e)
        {
            // create add form
            AddRoom form = new AddRoom();
            form.SetAdmin(this);
            form.Show();
        }

        private void btnUpdateRoom_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Please choose a room");
                return;
            }

            // get update Room
            RoomInformation room = RoomRepository.GetRoomById(selectedRoomId);

            // display update field
            UpdateRoom form = new UpdateRoom();
            form.SetAdmin(this);
            form.SetRoom(room);
            form.Show();

        }

        private void btnDeleteRoom_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Please select a room");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            // check if room has been register
            List<BookingDetail> bookingDetails = BookingDetailRepository.GetAllBookingDetailsByRoomId(selectedRoomId);
            if (bookingDetails.Count > 0)   // change status 
            {
                RoomInformation room = RoomRepository.GetRoomById(selectedRoomId);
                room.RoomStatus = 0;
                RoomRepository.UpdateRoom(room);
            }
            else
            {
                // delete room
                RoomRepository.RemoveRoom(selectedRoomId);
            }

            selectedRoomId = -1;
            LoadRoom();
            LoadRoomName();

        }

        private void btnSearchRoom_Clicked(object sender, RoutedEventArgs e)
        {
            // validate the text field
            string input = txtSearchRoom.Text;
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please input something");
                return;
            }
            // get rooms
            List<RoomInformation> roomInformation = RoomRepository.GetRoomsByRoomName(input);

            if (roomInformation.Count == 0)
            {
                MessageBox.Show("Found no room with number = " + input);
                return;
            }

            // add rooms to data grid room
            dgRoom.ItemsSource = null;
            dgRoom.ItemsSource = roomInformation;

        }

        private void dgRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRoom.ItemsSource == null) // do nothing
            {
                selectedRoomId = -1;
                return;
            }

            if (dgRoom.SelectedItems.Count == 0) // do nothing
            {
                selectedRoomId = -1;
                return;
            }

            DataGrid dataGrid = (DataGrid)sender;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell cell = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string id = ((TextBlock)cell.Content).Text.ToString();
            selectedRoomId = int.Parse(id);
        }

        private void btnLogout_Clicked(object sender, RoutedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void dgCustomerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomerName.ItemsSource == null)
            {
                selectedCustomerName = -1; return;
            }

            if (dgCustomerName.SelectedItems.Count == 0)
            {
                selectedCustomerName = -1; return;
            }

            DataGrid dataGrid = (DataGrid)sender;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell cell = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string id = ((TextBlock)cell.Content).Text.ToString();
            selectedCustomerName = int.Parse(id);

            // get customer booking detail
            LoadCustomerBookingDetail();
        }

        private void LoadCustomerBookingDetail()
        {
            try
            {
                // get customer booking detail
                var bookingDetails = BookingDetailRepository.GetBookingDetailsByCustomerId(selectedCustomerName)
                    .Select(b => new
                    {
                        ReservationID = b.BookingReservationId,
                        Room = b.Room.RoomNumber,
                        BookingDate = b.BookingReservation.BookingDate,
                        StartDate = b.StartDate,
                        EndDate = b.EndDate,
                        TotalPrice = b.BookingReservation.TotalPrice,
                        Status = b.BookingReservation.BookingStatus
                    });
                // binding to dg customer Booking details
                dgCustomerBookingDetail.ItemsSource = null;
                dgCustomerBookingDetail.ItemsSource = bookingDetails;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Load Customer booking data");
            }
        }

        private void dgRoomName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRoomsName.ItemsSource == null)
            {
                selectedRoomName = -1; return;
            }

            if (dgRoomsName.SelectedItems.Count == 0)
            {
                selectedRoomName = -1; return;
            }

            DataGrid dataGrid = (DataGrid)sender;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell cell = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string id = ((TextBlock)cell.Content).Text.ToString();
            selectedRoomName = int.Parse(id);
        }

        private void btnAddBooking_Clicked(object sender, RoutedEventArgs e)
        {
            // check for empty field
            if (selectedCustomerName == -1)
            {
                MessageBox.Show("Please choose a customer");
                return;
            }
            if (selectedRoomName == -1)
            {
                MessageBox.Show("Please choose a room");
                return;
            }

            string bookingDate = book_date.Text;
            string startDate = start_date.Text;
            string endDate = end_date.Text;

            if (string.IsNullOrEmpty(bookingDate) || string.IsNullOrEmpty(startDate)
                || string.IsNullOrEmpty(endDate))
            {
                MessageBox.Show("You must choose Booking date, start date and end date");
                return;
            }

            // validate date
            DateOnly bDate = DateOnly.Parse(bookingDate);
            DateOnly sDate = DateOnly.Parse(startDate);
            DateOnly eDate = DateOnly.Parse(endDate);

            if (sDate.CompareTo(bDate) < 0)
            {
                MessageBox.Show("Start date cannot be earlier than booking date");
                return;
            }

            if (sDate.CompareTo(eDate) > 0)
            {
                MessageBox.Show("Start date cannot be later than end date");
                return;
            }

            // CREATE BOOKING
            // get room
            RoomInformation room = RoomRepository.GetRoomById(selectedRoomName);
            // calculate total price
            int days = eDate.DayNumber - sDate.DayNumber;
            days = days == 0 ? 1 : days;
            decimal? totalPrice = days * room.RoomPricePerDay;
            // create book
            BookingReservation bookingReservation = new BookingReservation
            {
                BookingDate = bDate,
                TotalPrice = totalPrice,
                CustomerId = selectedCustomerName,
                BookingStatus = 1
            };
            bookingReservation = BookingReservationRepository.SaveBookingReservation(bookingReservation);
            // create booking detail
            BookingDetail bookingDetail = new BookingDetail
            {
                BookingReservationId = bookingReservation.BookingReservationId,
                RoomId = room.RoomId,
                StartDate = sDate,
                EndDate = eDate,
                ActualPrice = null
            };

            BookingDetailRepository.SaveBookingDetail(bookingDetail);
            // reload customer booking details
            LoadCustomerBookingDetail();
            // reset date field
            book_date.Text = string.Empty;
            start_date.Text = string.Empty;
            end_date.Text = string.Empty;
        }

        private void dgCustomerBookingDetail_Clicked(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomerBookingDetail.ItemsSource == null)
            {
                selectedBookingDetail = -1;
                return;
            }

            if (dgCustomerBookingDetail.SelectedItems.Count == 0)
            {
                selectedBookingDetail = -1;
                return;
            }

            DataGrid dataGrid = (DataGrid)sender;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell cell = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string id = ((TextBlock)cell.Content).Text.ToString();
            selectedBookingDetail = int.Parse(id);

        }

        private void btnDeleteBoking_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedBookingDetail == -1)
            {
                MessageBox.Show("Please choose a Booking Reservation");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }

            // delete booking detail
            BookingDetailRepository.DeleteBookingDetailByReservationId(selectedBookingDetail);
            // delete booking reservation
            BookingReservationRepository.DeleteReservationById(selectedBookingDetail);

            // reload booking detail data grid
            LoadCustomerBookingDetail();

        }

        private void btnShowReport_clicked(object sender, RoutedEventArgs e)
        {
            // check valid date
            string sDateStr = d_start.Text;
            string eDateStr = d_end.Text;
            if (string.IsNullOrEmpty(sDateStr) || string.IsNullOrEmpty(eDateStr))
            {
                MessageBox.Show("You must choose start and end date");
                return;
            }

            DateOnly sDate = DateOnly.Parse(sDateStr);
            DateOnly eDate = DateOnly.Parse(eDateStr);

            if (sDate.CompareTo(eDate) > 0)
            {
                MessageBox.Show("Start Date cannot be later than End Date");
                return;
            }
                
            var bookingDetails = BookingDetailRepository.GetBookingDetailBetweenDate(sDate, eDate)
                .Select(b => new
                {
                    ReservationId = b.BookingReservationId,
                    RoomNumber = b.Room.RoomNumber,
                    CustomerId = b.BookingReservation.CustomerId,
                    BookingDate = b.BookingReservation.BookingDate,
                    Total = b.BookingReservation.TotalPrice

                })
                .OrderByDescending(b => b.BookingDate);

            dgReport.ItemsSource = null;
            dgReport.ItemsSource = bookingDetails;

        }
    }
}
