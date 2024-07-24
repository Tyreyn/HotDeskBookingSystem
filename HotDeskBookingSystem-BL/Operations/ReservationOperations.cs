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
    public class ReservationOperations(HotDeskBookingSystemContext hotDeskBookingSystemContext) : IReservationOperations
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
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task AddNewReservation(Reservation newReservation)
        {
            await this.hotDeskBookingSystemContexts.Reservations.AddAsync(newReservation);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }

        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <returns>
        /// All reservations.
        /// </returns>
        public async Task<IList<Reservation>> GetAllReservation()
        {
            return await this.hotDeskBookingSystemContexts.Reservations
                .Include(r => r.Employee!)
                .Include(r => r.Desk!)
                .ToListAsync(); // Null forgiving null.
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
        public async Task<IList<Reservation>?> GetAllReservationByUserId(int employeeId)
        {
            return await this.hotDeskBookingSystemContexts.Reservations
                .Include(r => r.Employee)
                .Include(r => r.Desk)
                .Where(r => r.EmployeeId == employeeId)
                .ToListAsync();
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
        public async Task<Reservation?> GetAllReservationById(int reservationId)
        {
            return await this.hotDeskBookingSystemContexts.Reservations
                .Include(r => r.Employee)
                .Include(r => r.Desk)
                .SingleOrDefaultAsync(r => r.Id == reservationId);
        }

        /// <summary>
        /// Update reservation object.
        /// </summary>
        /// <param name="reservation">
        /// Reservation object to be updated.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task UpdateReservation(Reservation reservation)
        {
            this.hotDeskBookingSystemContexts.Reservations.Update(reservation);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }

        /// <summary>
        /// Remove reservation.
        /// </summary>
        /// <param name="reservationToDelete">
        /// Reservation to be deleted.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task RemoveReservation(Reservation reservationToDelete)
        {
            this.hotDeskBookingSystemContexts.Remove(reservationToDelete);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }
    }
}
