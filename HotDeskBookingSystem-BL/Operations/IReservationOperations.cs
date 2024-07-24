using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    public interface IReservationOperations
    {
        Task AddNewReservation(Reservation newReservation);
        Task<IList<Reservation>> GetAllReservation();
        Task<Reservation?> GetAllReservationById(int reservationId);
        Task<IList<Reservation>?> GetAllReservationByUserId(int employeeId);
        Task RemoveReservation(Reservation reservationToDelete);
        Task UpdateReservation(Reservation reservation);
    }
}