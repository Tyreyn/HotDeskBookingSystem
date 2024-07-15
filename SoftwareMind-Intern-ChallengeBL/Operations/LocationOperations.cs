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
    public class LocationOperations(HotDeskBookingSystemContext hotDeskBookingSystemContext)
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
        /// True, if location added correctly, otherwise false.
        /// </returns>
        public bool AddLocation(Location newLocation)
        {
            if (newLocation == null)
            {
                return false;
            }

            this.hotDeskBookingSystemContexts.Locations.Add(newLocation);
            this.hotDeskBookingSystemContexts.SaveChanges();
            return true;
        }

        /// <summary>
        /// Update location.
        /// </summary>
        /// <param name="updatedLocation">
        /// Location to be updated.
        /// </param>
        /// <returns>
        /// True, if location updated correctly, otherwise false.
        /// </returns>
        public bool UpdateLocation(Location updatedLocation)
        {
            if (updatedLocation == null)
            {
                return false;
            }

            this.hotDeskBookingSystemContexts.Locations.Update(updatedLocation);
            this.hotDeskBookingSystemContexts.SaveChanges();
            return true;
        }

        /// <summary>
        /// Get available locations.
        /// </summary>
        /// <returns>
        /// List of locations.
        /// </returns>
        public IList<Location>? GetLocations()
        {
            IList<Location>? locations = this.hotDeskBookingSystemContexts.Locations
                .Include(l => l.Desks!)
                .ThenInclude(l => l.Reservations)
                .ToList(); // Null forgiving null.
            return locations;
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
        public Location? GetLocationById(int locationId)
        {
            Location? location = this.hotDeskBookingSystemContexts.Locations
                .Include(l => l.Desks)
                .SingleOrDefault(location => location.Id == locationId);
            return location;
        }

        /// <summary>
        /// Delete location.
        /// </summary>
        /// <param name="locationToBeDeleted">
        /// Location to be deleted.
        /// </param>
        /// <returns>
        /// True, if deleted correctly, otherwise false.
        /// </returns>
        public bool DeleteLocation(Location locationToBeDeleted)
        {
            try
            {
                this.hotDeskBookingSystemContexts.Locations.Remove(locationToBeDeleted);
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
