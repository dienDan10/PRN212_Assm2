using BussinessObjects;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingReservationDAO : DBUtils
    {
        private FuminiHotelManagementContext context = new FuminiHotelManagementContext();

        public List<BookingReservation> GetBookingReservationByCustomerID(int id) { 
            List<BookingReservation> list = new List<BookingReservation>();
           
            try
            {
                list = context.BookingReservations.Where(b => b.CustomerId == id).ToList();
            } catch (Exception ex)
            {
                Console.WriteLine("Cannot get Booking Reservation");
            } 

            return list;
            
        }
        
        public BookingReservation SaveBookingReservation(BookingReservation bookingReservation)
        {
            try
            {

                context.BookingReservations.Add(bookingReservation);
                context.SaveChanges();

            } catch (Exception ex)
            {
                Console.WriteLine("Cannot save booking reservation");
            }
            return bookingReservation;
        }

        public void DeleteReservationById(int id)
        {
            try
            {
                BookingReservation bookingReservation = context.BookingReservations.FirstOrDefault(b => b.BookingReservationId == id);
                context.BookingReservations.Remove(bookingReservation);
                context.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine("Cannot delete reservation");
            }
        }

    }
}
