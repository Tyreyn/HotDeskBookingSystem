using SoftwareMind_Intern_ChallengeBL.Operations;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_Challenge.Services
{
    /// <summary>
    /// Location service.
    /// </summary>
    /// <param name="locationOperations">
    /// Available operations for location model.
    /// </param>
    public class LocationService(LocationOperations locationOperations, DeskOperations deskOperations)
    {
        private readonly LocationOperations locationOperations = locationOperations;

        private readonly DeskOperations deskOperations = deskOperations;

        /// <summary>
        /// Get all available locations.
        /// </summary>
        /// <returns>
        /// List of available locations.
        /// </returns>
        public IList<Location> GetLocations()
        {
            return this.locationOperations.GetLocations().ToList();
        }

        /// <summary>
        /// Add new location
        /// </summary>
        /// <param name="newLocation"></param>
        /// <returns></returns>
        public bool AddNewLocation(Location newLocation)
        {
            this.locationOperations.AddLocation(newLocation);
            return true;
        }

        /// <summary>
        /// Change location name.
        /// </summary>
        /// <param name="locationId">
        /// Location id to be changed.
        /// </param>
        /// <param name="newName">
        /// New location name.
        /// </param>
        /// <returns>
        /// T1 - True, if location name changed correctly, otherwise false.
        /// T2 - Message.
        /// </returns>
        public (bool, string) ChangeLocationName(int locationId, string newName)
        {
            Location? location = this.locationOperations.GetLocationById(locationId);
            if (location == null)
            {
                return (false, "There is no location with this ID");
            }
            location.Name = newName;
            this.locationOperations.UpdateLocation(location);
            return (true, "Location updated correctly");
        }

        /// <summary>
        /// Delete location.
        /// </summary>
        /// <param name="locationId">
        /// Location to be deleted.
        /// </param>
        /// <returns>
        /// T1 - True, if location deleted correctly, otherwise false.
        /// T2 - Message.
        /// </returns>
        public (bool, string) DeleteLocation(int locationId)
        {
            if (locationId == 1) { return (false, "You can't remove 'unused desks' location!"); }

            Location? location = this.locationOperations.GetLocationById(locationId);

            if (location == null)
            {
                return (false, "There is no location with this ID");
            }
            else if (location.Desks != null)
            {
                return (false, "There are still some desk in this location");
            }

            this.locationOperations.DeleteLocation(location);
            return (true, $"Location {locationId} ");
        }
    }
}
