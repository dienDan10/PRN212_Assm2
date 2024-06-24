using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoomRepository
    {

        private static RoomInformationDAO roomInformationDAO = new RoomInformationDAO();

        public static List<RoomInformation> GetAllRooms()
        {
            return roomInformationDAO.GetAllRooms();
        }

        public static RoomInformation GetRoomById(int id)
        {
            return roomInformationDAO.getRoomById(id);
        }

        public static void AddRoom(RoomInformation room)
        {
            roomInformationDAO.AddRoom(room);
        }

        public static void RemoveRoom(int id)
        {
            roomInformationDAO.DeleteRoom(id);
        }

        public static void UpdateRoom(RoomInformation room)
        {
            roomInformationDAO.updateRoom(room);
        }

        public static List<RoomInformation> GetRoomsByRoomName(string name)
        {
            return roomInformationDAO.GetRoomsByRoomNumber(name);
        }


    }
}
