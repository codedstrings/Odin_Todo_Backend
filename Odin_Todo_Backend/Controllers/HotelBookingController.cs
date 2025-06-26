using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Odin_Todo_Backend.Data;
using Odin_Todo_Backend.Models;

namespace Odin_Todo_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly TodoDbContext _context;
        public HotelBookingController(TodoDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult Create(HotelBooking booking)
        {
            var bookingInDb = _context.Bookings.Find(booking.Id);

            if (bookingInDb != null)
                return new JsonResult(Forbid("Booking already exists"));


            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return new JsonResult(Ok(booking));
        }

        //put
        [HttpPut]
        public JsonResult Update(HotelBooking booking)
        {
            var bookingInDb = _context.Bookings.Find(booking.Id);
            if (bookingInDb == null)
            {
                return new JsonResult(NotFound());
            }

            bookingInDb.RoomNumber = booking.RoomNumber;
            bookingInDb.Name = booking.Name;

            _context.SaveChanges();

            return new JsonResult(Ok("booking updated"));
        }

        //Get 
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var result = _context.Bookings.Find(id);
            if(result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        
        //list api
        [HttpGet]
        public JsonResult GetBookingsList()
        {
            var result = _context.Bookings.ToList();
            return new JsonResult(result);
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Bookings.Find(id);

            if(result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok("deleted booking for :"+ result.Name));
        }

    }
}
