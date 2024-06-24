using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookingDetailRepository
    {
        private static BookingDetailDAO bookingDetailDAO = new BookingDetailDAO();

        public static List<BookingDetail> GetAllBookingDetail()
        {
            return bookingDetailDAO.GetAllBookingDetail();
        }

        public static List<BookingDetail> GetAllBookingDetailsByRoomId(int id)
        {
            return bookingDetailDAO.GetBookingDetailsByRoomId(id);
        }

        public static List<BookingDetail> GetBookingDetailsByCustomerId(int id)
        {
            return bookingDetailDAO.getBookingDetailsByCustomerId(id);
        }

        public static void SaveBookingDetail(BookingDetail bookingDetail)
        {
            bookingDetailDAO.SaveBookingDetail(bookingDetail);
        }

        public static void DeleteBookingDetailByReservationId(int id)
        {
            bookingDetailDAO.DeleteBookingDetailByReservationId((int)id);
        }

        public static List<BookingDetail> GetBookingDetailBetweenDate(DateOnly startDate,  DateOnly endDate)
        {
            return bookingDetailDAO.GetBookingDetailsBetweenDate(startDate, endDate);
        }


    }
}
