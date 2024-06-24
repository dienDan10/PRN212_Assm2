using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoomTypeRepository
    {
        private static RoomTypeDAO RoomTypeDAO = new RoomTypeDAO();

        public static List<RoomType> GetAllRoomTypes()
        {
            return RoomTypeDAO.GetAllRoomTypes();
        }

    }
}
