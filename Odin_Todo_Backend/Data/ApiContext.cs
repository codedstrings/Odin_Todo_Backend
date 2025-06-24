using Microsoft.EntityFrameworkCore;
using Odin_Todo_Backend.Models;

namespace Odin_Todo_Backend.Data
{
    public class ApiContext: DbContext
    {
        //in memory db 
        public DbSet<HotelBooking> Bookings { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

    }
}
