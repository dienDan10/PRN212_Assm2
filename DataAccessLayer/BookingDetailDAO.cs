using BussinessObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingDetailDAO : DBUtils
    {
        private FuminiHotelManagementContext context = new FuminiHotelManagementContext();

        public List<BookingDetail> GetAllBookingDetail()
        {
           List<BookingDetail> bookingDetails = new List<BookingDetail>();
            try
            {

                bookingDetails = context.BookingDetails
                    .Select(b => b)
                    .ToList();

            } catch (Exception ex)
            {
                Console.WriteLine("Cannot find booking details");
            }
            return bookingDetails;
        }

        public List<BookingDetail> GetBookingDetailsByRoomId(int id) 
        {
            List<BookingDetail> bookingDetails = new List<BookingDetail>();
            try
            {

                bookingDetails = context.BookingDetails.Where(b => b.RoomId == id).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot find booking details");
            }
            return bookingDetails;
        }

        public List<BookingDetail> getBookingDetailsByCustomerId(int id)
        {
            List<BookingDetail> bookingDetails = new List<BookingDetail>();
            try
            {

                bookingDetails = context.BookingDetails.Where(b => b.BookingReservation.CustomerId == id)
                    .Include(b => b.BookingReservation)
                    .Include(b => b.Room)
                    .ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot find booking details");
            }
            return bookingDetails;
        }

        public void SaveBookingDetail(BookingDetail bookingDetail)
        {
            try
            {
                context.BookingDetails.Add(bookingDetail);
                context.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine("cannot save Booking detail");
            }
        }

        public void DeleteBookingDetailByReservationId(int id)
        {
            try
            {
                BookingDetail bookingDetail = context.BookingDetails.FirstOrDefault(b => b.BookingReservationId == id);
                context.BookingDetails.Remove(bookingDetail);
                context.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine("Cannot delete booking detail");
            }
        }

        public List<BookingDetail> GetBookingDetailsBetweenDate(DateOnly startDate, DateOnly endDate)
        {
            List<BookingDetail> list = new List<BookingDetail>();
            try
            {
                list = context.BookingDetails
                    .Select(b => b)
                    .Include(b =>b.BookingReservation)
                    .Include (b => b.Room)
                    .Where(b => b.BookingReservation.BookingDate >  startDate && b.BookingReservation.BookingDate < endDate)
                    .ToList();
            } catch (Exception ex)
            {
                Console.WriteLine("Cannot get booking detail");
            }
            return list;
        }


    }
}
