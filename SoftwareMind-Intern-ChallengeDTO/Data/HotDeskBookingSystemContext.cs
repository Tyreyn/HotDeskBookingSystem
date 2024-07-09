using Microsoft.EntityFrameworkCore;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeDTO.Data
{
    /// <summary>
    /// Hot desk booking system context.
    /// </summary>
    public class HotDeskBookingSystemContext : DbContext, IHotDeskBookingSystemContext
    {
        public HotDeskBookingSystemContext(DbContextOptions<HotDeskBookingSystemContext> options) : base(options)
        {

        }

        /// <summary>
        /// DbSet of Desks.
        /// </summary>
        public DbSet<Desk> Desks { get; set; }

        /// <summary>
        /// DbSet of Employees.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// DbSet of Locations.
        /// </summary>
        public DbSet<Location> Locations { get; set; }

        /// <summary>
        /// DbSet of Reservations.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; }
    }
}
