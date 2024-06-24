using BussinessObjects;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoomTypeDAO : DBUtils
    {
        private FuminiHotelManagementContext context = new FuminiHotelManagementContext();
        public List<RoomType> GetAllRoomTypes()
        {
            List<RoomType> roomTypes = new List<RoomType>();
            
            try
            {
                roomTypes = context.RoomTypes.Select(r => r).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            return roomTypes;
        }

    }
}
