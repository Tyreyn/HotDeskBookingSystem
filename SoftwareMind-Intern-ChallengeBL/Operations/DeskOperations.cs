namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    using Microsoft.EntityFrameworkCore;
    using SoftwareMind_Intern_ChallengeDTO.Data;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Available operations for desk.
    /// </summary>
    /// <param name="hotDeskBookingSystemContext">
    /// Hot desk booking system context.
    /// </param>
    public class DeskOperations(HotDeskBookingSystemContext hotDeskBookingSystemContext)
    {
        /// <summary>
        /// Hot desk booking system context.
        /// </summary>
        private readonly HotDeskBookingSystemContext hotDeskBookingSystemContexts = hotDeskBookingSystemContext;

        /// <summary>
        /// Get all desks.
        /// </summary>
        /// <returns>
        /// List of desk.
        /// </returns>
        public IList<Desk>? GetAllDesks()
        {
            return this.hotDeskBookingSystemContexts.Desks
                .Include(d => d.Location!)
                .Include(d => d.Reservations!)
                .ThenInclude(r => r.Employee)
                .ToList(); // Null forgiving null.
        }

        /// <summary>
        /// Get desk by id.
        /// </summary>
        /// <param name="deskId">
        /// Desk ID to get.
        /// </param>
        /// <returns>
        /// Found desk.
        /// </returns>
        public Desk? GetDeskById(int deskId)
        {
            return this.hotDeskBookingSystemContexts.Desks
                .Include(d => d.Location!)
                .Include(d => d.Reservations!)
                .ThenInclude(d => d.Employee)
                .SingleOrDefault(desk => desk.Id == deskId); // Null forgiving null.
        }

        /// <summary>
        /// Add new desk.
        /// </summary>
        /// <param name="newDesk">
        /// Desk to add.
        /// </param>
        /// <returns>
        /// True, if desk is added correctly, otherwise false.
        /// </returns>
        public bool AddDesk(Desk newDesk)
        {
            if (newDesk == null)
            {
                return false;
            }

            this.hotDeskBookingSystemContexts.Add(newDesk);
            this.hotDeskBookingSystemContexts.SaveChanges();
            return true;
        }

        /// <summary>
        /// Update desk.
        /// </summary>
        /// <param name="updatedDesk">
        /// Desk to be updated.
        /// </param>
        /// <returns>
        /// True, if desk updated correctly, otherwise false.
        /// </returns>
        public bool UpdateDesk(Desk updatedDesk)
        {
            if (updatedDesk == null)
            {
                return false;
            }

            this.hotDeskBookingSystemContexts.Desks.Update(updatedDesk);
            this.hotDeskBookingSystemContexts.SaveChanges();
            return true;
        }

        /// <summary>
        /// Delete desk.
        /// </summary>
        /// <param name="deskToBeDeleted">
        /// Desk to be deleted.
        /// </param>
        /// <returns>
        /// True, if deleted correctly, otherwise false.
        /// </returns>
        public bool DeleteDesk(Desk deskToBeDeleted)
        {
            try
            {
                this.hotDeskBookingSystemContexts.Desks.Remove(deskToBeDeleted);
                this.hotDeskBookingSystemContexts.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
