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
    /// Interaction logic for UpdateRoom.xaml
    /// </summary>
    public partial class UpdateRoom : Window
    {
        private Admin admin;
        private RoomInformation room;


        public UpdateRoom()
        {
            InitializeComponent();
        }

        private void LoadRoomType()
        {
            // get all room type
            List<RoomType> roomTypes = RoomTypeRepository.GetAllRoomTypes();
            cboRoomType.ItemsSource = roomTypes;
            cboRoomType.DisplayMemberPath = "RoomTypeName";
            cboRoomType.SelectedValuePath = "RoomTypeId";
            cboRoomType.SelectedIndex = 0;
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
                || string.IsNullOrEmpty(capacity) || string.IsNullOrEmpty(price))
            {
                MessageBox.Show("Please fill in all the information");
                return;
            }

            // update room info
            room.RoomDetailDescription = description;
            room.RoomNumber = roomNum;
            room.RoomMaxCapacity = int.Parse(capacity);
            room.RoomTypeId = int.Parse(roomType);
            room.RoomPricePerDay = decimal.Parse(price);

            // update room
            RoomRepository.UpdateRoom(room);
            // reload room data grid
            admin.LoadRoom();
            admin.LoadRoomName();
            this.Close();
        }

        private void btnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SetAdmin(Admin admin)
        {
            this.admin = admin;
        } 

        public void SetRoom(RoomInformation room)
        {
            this.room = room;
            LoadRoomType();
            SetRoomInfo();
        }

        private void SetRoomInfo()
        {
            txtDescription.Text = room.RoomDetailDescription;
            txtMaxCapacity.Text = room.RoomMaxCapacity.ToString();
            txtPricePerDay.Text = room.RoomPricePerDay.ToString();
            txtRoomNumber.Text = room.RoomNumber;
            cboRoomType.SelectedValue = room.RoomTypeId;
        }

    }
}
