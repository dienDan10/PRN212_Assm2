using BussinessObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoomInformationDAO : DBUtils
    {
        private FuminiHotelManagementContext context = new FuminiHotelManagementContext();

        public List<RoomInformation> GetAllRooms()
        {
            List<RoomInformation> rooms = new List<RoomInformation>();
            
            try
            {
                rooms = context.RoomInformations.Select(c => c).ToList();
            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            return rooms;
        }

        public List<RoomInformation> GetRoomsByRoomNumber(string number)
        {
            List<RoomInformation> rooms = new List<RoomInformation>();
            
            try
            {
                rooms = context.RoomInformations
                    .Where(r => r.RoomNumber.ToLower().Contains(number.ToLower()))
                    .ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            return rooms;

        }

        public RoomInformation getRoomById(int id)
        {
           
            RoomInformation room = new RoomInformation();
            try
            {
                room = context.RoomInformations.FirstOrDefault(r => r.RoomId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return room;
        }


        public void AddRoom(RoomInformation room)
        {

            try
            {
                context.RoomInformations.Add(room);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        public void updateRoom(RoomInformation room)
        {
           
            try
            {
                context.RoomInformations.Update(room);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void DeleteRoom(int id)
        {
 
            try
            {
                RoomInformation room = context.RoomInformations.FirstOrDefault(r => r.RoomId == id);
                context.RoomInformations.Remove(room);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


    }
}
