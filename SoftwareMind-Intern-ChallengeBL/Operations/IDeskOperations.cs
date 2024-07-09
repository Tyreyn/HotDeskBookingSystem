using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    public interface IDeskOperations
    {
        bool AddDesk(Desk newDesk);
        (bool, string) ChangeAvailability(int deskId);
        (bool, string) DeleteDesk(int deskId);
    }
}