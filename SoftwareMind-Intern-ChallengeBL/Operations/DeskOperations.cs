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
    public class DeskOperations(HotDeskBookingSystemContext hotDeskBookingSystemContext) : IDeskOperations
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
        public async Task<IList<Desk>?> GetAllDesks()
        {
            return await this.hotDeskBookingSystemContexts.Desks
                .Include(d => d.Location!)
                .Include(d => d.Reservations!)
                .ThenInclude(r => r.Employee)
                .ToListAsync(); // Null forgiving null.
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
        public async Task<Desk?> GetDeskById(int deskId)
        {
            return await this.hotDeskBookingSystemContexts.Desks
                .Include(d => d.Location!)
                .Include(d => d.Reservations!)
                .ThenInclude(d => d.Employee)
                .SingleOrDefaultAsync(desk => desk.Id == deskId); // Null forgiving null.
        }

        /// <summary>
        /// Add new desk.
        /// </summary>
        /// <param name="newDesk">
        /// Desk to add.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task AddDesk(Desk newDesk)
        {
            await this.hotDeskBookingSystemContexts.AddAsync(newDesk);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }

        /// <summary>
        /// Update desk.
        /// </summary>
        /// <param name="updatedDesk">
        /// Desk to be updated.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task UpdateDesk(Desk updatedDesk)
        {
            this.hotDeskBookingSystemContexts.Desks.Update(updatedDesk);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }

        /// <summary>
        /// Delete desk.
        /// </summary>
        /// <param name="deskToBeDeleted">
        /// Desk to be deleted.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task DeleteDesk(Desk deskToBeDeleted)
        {
            this.hotDeskBookingSystemContexts.Desks.Remove(deskToBeDeleted);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }
    }
}
