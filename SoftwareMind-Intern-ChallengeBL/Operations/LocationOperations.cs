using SoftwareMind_Intern_ChallengeDTO.Data;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeBL.Operations
{
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
        private readonly HotDeskBookingSystemContext DbContext = hotDeskBookingSystemContext;

        /// <summary>
        /// Add new location
        /// </summary>
        /// <param name="newLocation">
        /// New location.
        /// </param>
        /// <returns>
        /// True, if location added correctly, otherwise false.
        /// </returns>
        public bool AddLocation(Location newLocation)
        {
            if (newLocation == null) return false;

            this.DbContext.Locations.Add(newLocation);
            this.DbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Get available locations.
        /// </summary>
        /// <returns>
        /// List of locations.
        /// </returns>
        public List<Location> GetLocation()
        {
            List<Location> locations = this.DbContext.Locations.ToList();
            return locations;
        }

        /// <summary>
        /// Delete location.
        /// </summary>
        /// <param name="locationId">
        /// Location ID to be deleted.
        /// </param>
        /// <returns>
        /// T1 - true, if deleted correctly, otherwise false.
        /// T2 - message.
        /// </returns>
        public (bool, string) DeleteLocation(int locationId)
        {
            Location locationToBeDeleted = this.DbContext.Locations.Single(location => location.Id == locationId);
            if (locationToBeDeleted == null)
            {
                return (false, "There is no location with this ID");
            }
            else if (locationToBeDeleted.Desks.Any())
            {
                return (false, "There is desks in this location");
            }

            this.DbContext.Locations.Remove(locationToBeDeleted);
            this.DbContext.SaveChanges();
            return (true, "Location deleted correctly");
        }
    }
}
