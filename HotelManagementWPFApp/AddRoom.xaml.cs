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
    /// Interaction logic for AddRoom.xaml
    /// </summary>
    public partial class AddRoom : Window
    {

        private Admin admin;

        public AddRoom()
        {
            InitializeComponent();
            LoadRoomType();
        }

        private void LoadRoomType()
        {
            // get room types
            List<RoomType> roomTypes = RoomTypeRepository.GetAllRoomTypes();
            // set up room type for list box
            cboRoomType.ItemsSource = roomTypes;
            cboRoomType.DisplayMemberPath = "RoomTypeName";
            cboRoomType.SelectedValuePath = "RoomTypeId";
            cboRoomType.SelectedIndex = 0;
        }

        private void btnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Clicked(object sender, RoutedEventArgs e)
        {
            // get form field
            string roomNum = txtRoomNumber.Text;
            string description = txtDescription.Text;
            string capacity = txtMaxCapacity.Text;
            string price = txtPricePerDay.Text;
            string roomType = cboRoomType.SelectedValue.ToString();

            // validation
            if (string.IsNullOrEmpty(price) || string.IsNullOrEmpty(roomNum) 
                || string.IsNullOrEmpty(capacity) || string.IsNullOrEmpty(price)) {
                MessageBox.Show("Please fill in all the information");
                return;
            }

            // create a room
            RoomInformation room = new RoomInformation() {
                RoomNumber = roomNum,
                RoomStatus = 1,
                RoomDetailDescription = description,
                RoomMaxCapacity = int.Parse(capacity),
                RoomPricePerDay = decimal.Parse(price),
                RoomTypeId = int.Parse(roomType)
            };
            
            // add room
            RoomRepository.AddRoom(room);
            // reload room data grid
            admin.LoadRoom();
            admin.LoadRoomName();
            // close
            this.Close();
        }

        public void SetAdmin(Admin admin)
        {
            this.admin = admin;
        }
    }
}
