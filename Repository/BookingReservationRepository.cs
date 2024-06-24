using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingReservationRepository
    {
        private static BookingReservationDAO bookingReservationDAO = new BookingReservationDAO();

        public static List<BookingReservation> GetBookingReservationsByCustomerID(int id)
        {
            return bookingReservationDAO.GetBookingReservationByCustomerID(id);
        }

        public static BookingReservation SaveBookingReservation(BookingReservation bookingReservation)
        {
            return bookingReservationDAO.SaveBookingReservation(bookingReservation);
        }

        public static void DeleteReservationById(int id)
        {
            bookingReservationDAO.DeleteReservationById(id);
        }
    }
}
