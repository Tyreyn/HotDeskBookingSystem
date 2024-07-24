using Microsoft.EntityFrameworkCore;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeDTO.Data
{

    /// <summary>
    /// Hot desk booking system context interface.
    /// </summary>
    public interface IHotDeskBookingSystemContext
    {
        /// <summary>
        /// DbSet of Desks.
        /// </summary>
        DbSet<Desk> Desks { get; set; }

        /// <summary>
        /// DbSet of Employees.
        /// </summary>
        DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// DbSet of Locations.
        /// </summary>
        DbSet<Location> Locations { get; set; }

        /// <summary>
        /// DbSet of Reservations.
        /// </summary>
        DbSet<Reservation> Reservations { get; set; }
    }
}