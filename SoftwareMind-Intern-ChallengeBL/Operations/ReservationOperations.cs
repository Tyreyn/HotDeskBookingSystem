using SoftwareMind_Intern_ChallengeDTO.Data;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    /// <summary>
    /// Available operations for reservatio.
    /// </summary
    /// <param name="hotDeskBookingSystemContext">
    /// Hot desk booking system context.
    /// </param>
    public class ReservationOperations(HotDeskBookingSystemContext hotDeskBookingSystemContext)
    {
        /// <summary>
        /// Hot desk booking system context.
        /// </summary>
        private readonly HotDeskBookingSystemContext DbContext = hotDeskBookingSystemContext;

        /// <summary>
        /// Add new reservation.
        /// </summary>
        /// <param name="employee">
        /// Employee who is making reservation.
        /// </param>
        /// <param name="desk">
        /// Desk to be reserved.
        /// </param>
        /// <param name="startDate">
        /// Start date of reservation.
        /// </param>
        /// <param name="endDate">
        /// End date of reservation.
        /// </param>
        /// <returns></returns>
        public (bool, string) AddNewReservation(Employee employee, Desk desk, DateTime startDate, DateTime endDate)
        {
            if (endDate - startDate > TimeSpan.FromDays(7))
            {
                return (false, "Cannot book a desk for more than a week.");
            }

            if (employee == null)
            {
                return (false, "There is no such a user");
            }
            else if (desk == null)
            {
                return (false, "There is no such a desk");
            }
            else if (!desk.IsAvailable)
            {
                return (false, "This desk is not available for now!");
            }

            Reservation newReservation = new Reservation
            {
                Desk = desk,
                Employee = employee,
                DataStart = startDate,
                DataEnd = endDate,
            };

            this.DbContext.Reservations.Add(newReservation);
            this.DbContext.SaveChanges();
            return (true, "Reservation was made corretly");
        }
    }
}
