namespace SoftwareMind_Intern_Challenge.Services
{
    using SoftwareMind_Intern_ChallengeBL.Operations;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Location service.
    /// </summary>
    /// <param name="locationOperations">
    /// Available operations for location model.
    /// </param>
    public class LocationService(LocationOperations locationOperations)
    {
        private readonly LocationOperations locationOperations = locationOperations;

        /// <summary>
        /// Get all available locations.
        /// </summary>
        /// <returns>
        /// List of available locations.
        /// </returns>
        public async Task<IList<Location>?> GetLocations()
        {
            return await this.locationOperations.GetLocations();
        }

        /// <summary>
        /// Add new location.
        /// </summary>
        /// <param name="newLocation">
        /// New location to add.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public async Task AddNewLocation(Location newLocation)
        {
            await this.locationOperations.AddLocation(newLocation);
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
        public async Task<(bool, string)> ChangeLocationName(int locationId, string newName)
        {
            Location? location = await this.locationOperations.GetLocationById(locationId);
            if (location == null)
            {
                return (false, "There is no location with this ID");
            }

            location.Name = newName;
            await this.locationOperations.UpdateLocation(location);
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
        public async Task<(bool, string)> DeleteLocation(int locationId)
        {
            if (locationId == 1)
            {
                return (false, "You can't remove 'unused desks' location!");
            }

            Location? location = await this.locationOperations.GetLocationById(locationId);

            if (location == null)
            {
                return (false, "There is no location with this ID");
            }
            else if (location.Desks.Count > 0)
            {
                return (false, "There are still some desk in this location!");
            }

            await this.locationOperations.DeleteLocation(location);
            return (true, $"Location {locationId} deleted successfully");
        }
    }
}
