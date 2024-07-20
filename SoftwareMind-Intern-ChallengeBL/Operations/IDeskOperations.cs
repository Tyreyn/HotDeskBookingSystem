using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    public interface IDeskOperations
    {
        Task AddDesk(Desk newDesk);
        Task DeleteDesk(Desk deskToBeDeleted);
        Task<IList<Desk>?> GetAllDesks();
        Task<Desk?> GetDeskById(int deskId);
        Task UpdateDesk(Desk updatedDesk);
    }
}