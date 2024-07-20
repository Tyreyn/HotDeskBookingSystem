using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    public interface ILocationOperations
    {
        Task AddLocation(Location newLocation);
        Task DeleteLocation(Location locationToBeDeleted);
        Task<Location?> GetLocationById(int locationId);
        Task<IList<Location>?> GetLocations();
        Task UpdateLocation(Location updatedLocation);
    }
}