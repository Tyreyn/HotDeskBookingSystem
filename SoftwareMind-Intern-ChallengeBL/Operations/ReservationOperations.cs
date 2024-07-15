namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    using Microsoft.EntityFrameworkCore;
    using SoftwareMind_Intern_ChallengeDTO.Data;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

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
        private readonly HotDeskBookingSystemContext hotDeskBookingSystemContexts = hotDeskBookingSystemContext;

        /// <summary>
        /// Add new reservation.
        /// </summary>
        /// <param name="newReservation">
        /// New reservation to add.
        /// </param>
        /// <returns>
        /// True, if reservation was made correctly, otherwise false.
        /// </returns>
        public bool AddNewReservation(Reservation newReservation)
        {
            this.hotDeskBookingSystemContexts.Reservations.Add(newReservation);
            this.hotDeskBookingSystemContexts.SaveChanges();
            return true;
        }

        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <returns>
        /// All reservations.
        /// </returns>
        public IList<Reservation> GetAllReservation()
        {
            return this.hotDeskBookingSystemContexts.Reservations
                .Include(r => r.Employee!)
                .Include(r => r.Desk!)
                .ToList(); // Null forgiving null.
        }

        /// <summary>
        /// Get reservation by user ID.
        /// </summary>
        /// <param name="employeeId">
        /// Reservation to get.
        /// </param>
        /// <returns>
        /// Reservation objects.
        /// </returns>
        public IList<Reservation>? GetAllReservationByUserId(int employeeId)
        {
            return this.hotDeskBookingSystemContexts.Reservations
                .Include(r => r.Employee)
                .Include(r => r.Desk)
                .Where(r => r.EmployeeId == employeeId)
                .ToList();
        }

        /// <summary>
        /// Get reservation by ID.
        /// </summary>
        /// <param name="reservationId">
        /// Reservation to get.
        /// </param>
        /// <returns>
        /// Reservation object.
        /// </returns>
        public Reservation? GetAllReservationById(int reservationId)
        {
            return this.hotDeskBookingSystemContexts.Reservations
                .Include(r => r.Employee)
                .Include(r => r.Desk)
                .SingleOrDefault(r => r.Id == reservationId);
        }

        /// <summary>
        /// Update reservation object.
        /// </summary>
        /// <param name="reservation">
        /// Reservation object to be updated.
        /// </param>
        /// <returns>
        /// True, if update went correctly, otherwise false.
        /// </returns>
        public bool UpdateReservation(Reservation reservation)
        {
            if (reservation == null)
            {
                return false;
            }

            this.hotDeskBookingSystemContexts.Reservations.Update(reservation);
            this.hotDeskBookingSystemContexts.SaveChanges();
            return true;
        }

        /// <summary>
        /// Remove reservation.
        /// </summary>
        /// <param name="reservationToDelete">
        /// Reservation to be deleted.
        /// </param>
        public void RemoveReservation(Reservation reservationToDelete)
        {
            this.hotDeskBookingSystemContexts.Remove(reservationToDelete);
            this.hotDeskBookingSystemContexts.SaveChanges();
        }
    }
}
