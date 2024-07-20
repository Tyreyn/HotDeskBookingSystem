namespace SoftwareMind_Intern_Challenge.Services
{
    using SoftwareMind_Intern_ChallengeBL.Operations;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Reservation service.
    /// </summary>
    /// <param name="reservationOperations">
    /// Available operations for reservation model.
    /// </param>
    /// <param name="deskOperations">
    /// Available operations for desk model.
    /// </param>
    public class ReservationService(ReservationOperations reservationOperations, DeskOperations deskOperations)
    {
        /// <summary>
        /// Available operations for reservation model.
        /// </summary>
        private readonly ReservationOperations reservationOperations = reservationOperations;

        /// <summary>
        /// Available operations for desk model.
        /// </summary>
        private readonly DeskOperations deskOperations = deskOperations;

        /// <summary>
        /// Make reservation.
        /// </summary>
        /// <param name="newReservation">
        /// New reservation to be added.
        /// </param>
        /// <param name="employeeEmail">
        /// Employee email who is making reservation.
        /// </param>
        /// <returns>
        /// T1 - true, if reservation made correctly, otherwise false.
        /// T2 - message.
        /// </returns>
        public async Task<(bool, string)> MakeReservation(Reservation newReservation, string employeeEmail)
        {
            DateTime currentDate = DateTime.Now;

            if (newReservation.DateEnd < currentDate || newReservation.DateStart < currentDate)
            {
                return (false, "You can't start or end reservation before current date!");
            }
            else if (newReservation.DateEnd < newReservation.DateStart)
            {
                return (false, "End date can't be before start date!");
            }
            else if (newReservation.DateEnd - newReservation.DateStart > TimeSpan.FromDays(7))
            {
                return (false, "Cannot book a desk for more than a week.");
            }

            Desk? desk = await this.deskOperations.GetDeskById(newReservation.DeskId);

            if (desk == null)
            {
                return (false, "There is no such a desk");
            }
            else if (!desk.IsAvailable)
            {
                return (false, "This desk is not available for now!");
            }

            desk.IsAvailable = false;
            await this.deskOperations.UpdateDesk(desk);
            await this.reservationOperations.AddNewReservation(newReservation);

            return (true, "Reservation has been made correctly");
        }

        /// <summary>
        /// Get all reservation made by user.
        /// </summary>
        /// <param name="userId">
        /// User id.
        /// </param>
        /// <returns>
        /// List of user reservations.
        /// </returns>
        public async Task<IList<Reservation>?> GetReservationByUserId(int userId)
        {
            return await this.reservationOperations.GetAllReservationByUserId(userId);
        }

        /// <summary>
        /// Change desk in reservation.
        /// </summary>
        /// <param name="reservationId">
        /// Reservation ID to be updated.
        /// </param>
        /// <param name="newDeskId">
        /// New desk ID to be updated.
        /// </param>
        /// <param name="userId">
        /// User Id who wants to update reservation.
        /// </param>
        /// <returns>
        /// T1 - true, if reservation updated correctly, otherwise false.
        /// T2 - message.
        /// </returns>
        public async Task<(bool, string)> ChangeReservationDesk(int reservationId, int newDeskId, int userId)
        {
            Desk? newDesk = await this.deskOperations.GetDeskById(newDeskId);
            Reservation? reservation = await this.reservationOperations.GetAllReservationById(reservationId);
            DateTime currentDate = DateTime.Now;

            if (newDesk == null)
            {
                return (false, "There is no Desk with this ID.");
            }

            if (!newDesk.IsAvailable)
            {
                return (false, "Selected desk is not available");
            }

            if (reservation.EmployeeId != userId)
            {
                return (false, "It is not your reservation!");
            }

            if (reservation.DateStart - currentDate < TimeSpan.FromDays(1))
            {
                return (false, "You can't change desk 1 day before start of reservation.");
            }

            int? oldDeskId = reservation.DeskId;
            reservation.Desk.IsAvailable = true;
            await this.deskOperations.UpdateDesk(reservation.Desk);
            reservation.DeskId = newDesk.Id;
            newDesk.IsAvailable = false;
            await this.deskOperations.UpdateDesk(newDesk);

            await this.reservationOperations.UpdateReservation(reservation);
            return (true, $"Desk changed from {oldDeskId} to {newDeskId} in reservation {reservationId}");
        }

        /// <summary>
        /// Check if reservation can be deleted.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task CheckIfReservationChanged()
        {
            DateTime currentDate = DateTime.Now;
            IList<Reservation> reservations = await this.reservationOperations.GetAllReservation();
            foreach (Reservation reservation in reservations.Where(r => (currentDate - r.DateEnd) >= TimeSpan.FromDays(1)))
            {
                reservation.Desk.IsAvailable = true;
                await this.deskOperations.UpdateDesk(reservation.Desk);
                await this.reservationOperations.RemoveReservation(reservation);
            }
        }
    }
}
