namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    using Microsoft.EntityFrameworkCore;
    using SoftwareMind_Intern_ChallengeDTO.Data;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Available operations for location.
    /// </summary>
    /// <param name="hotDeskBookingSystemContext">
    /// Hot desk booking system context.
    /// </param>
    public class LocationOperations(HotDeskBookingSystemContext hotDeskBookingSystemContext) : ILocationOperations
    {
        /// <summary>
        /// Hot desk booking system context.
        /// </summary>
        private readonly HotDeskBookingSystemContext hotDeskBookingSystemContexts = hotDeskBookingSystemContext;

        /// <summary>
        /// Add new location.
        /// </summary>
        /// <param name="newLocation">
        /// New location.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task AddLocation(Location newLocation)
        {
            await this.hotDeskBookingSystemContexts.Locations.AddAsync(newLocation);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }

        /// <summary>
        /// Update location.
        /// </summary>
        /// <param name="updatedLocation">
        /// Location to be updated.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task UpdateLocation(Location updatedLocation)
        {
            this.hotDeskBookingSystemContexts.Locations.Update(updatedLocation);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }

        /// <summary>
        /// Get available locations.
        /// </summary>
        /// <returns>
        /// List of locations.
        /// </returns>
        public async Task<IList<Location>?> GetLocations()
        {
            return await this.hotDeskBookingSystemContexts.Locations
                .Include(l => l.Desks!)
                .ThenInclude(l => l.Reservations)
                .ToListAsync(); // Null forgiving null;
        }

        /// <summary>
        /// Get location by ID.
        /// </summary>
        /// <param name="locationId">
        /// Location ID to find.
        /// </param>
        /// <returns>
        /// Location received.
        /// </returns>
        public async Task<Location?> GetLocationById(int locationId)
        {
            return await this.hotDeskBookingSystemContexts.Locations
                .Include(l => l.Desks)
                .SingleOrDefaultAsync(location => location.Id == locationId);
        }

        /// <summary>
        /// Delete location.
        /// </summary>
        /// <param name="locationToBeDeleted">
        /// Location to be deleted.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task DeleteLocation(Location locationToBeDeleted)
        {
            this.hotDeskBookingSystemContexts.Locations.Remove(locationToBeDeleted);
            await this.hotDeskBookingSystemContexts.SaveChangesAsync();
        }
    }
}
