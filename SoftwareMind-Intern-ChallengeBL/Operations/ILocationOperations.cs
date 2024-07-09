using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    /// <summary>
    /// Available operations for location.
    /// </summary>
    public interface ILocationOperations
    {
        /// <summary>
        /// Add new location
        /// </summary>
        /// <param name="newLocation">
        /// New location.
        /// </param>
        /// <returns>
        /// True, if location added correctly, otherwise false.
        /// </returns>
        bool AddLocation(Location newLocation);

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
        (bool, string) DeleteLocation(int locationId);

        /// <summary>
        /// Get available locations.
        /// </summary>
        /// <returns>
        /// List of locations.
        /// </returns>
        List<Location> GetLocation();
    }
}