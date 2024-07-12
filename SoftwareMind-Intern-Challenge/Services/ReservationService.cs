using SoftwareMind_Intern_ChallengeBL.Operations;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_Challenge.Services
{
    public class ReservationService(ReservationOperations reservationOperations, DeskOperations deskOperations)
    {
        private readonly ReservationOperations reservationOperations = reservationOperations;

        private readonly DeskOperations deskOperations = deskOperations;

        public (bool,string) MakeReservation(Reservation newReservation)
        {
            if (newReservation.DataEnd - newReservation.DataStart > TimeSpan.FromDays(7))
            {
                return (false, "Cannot book a desk for more than a week.");
            }

            Desk desk = this.deskOperations.GetDeskById(newReservation.DeskId);

            if (desk == null)
            {
                return (false, "There is no such a desk");
            }
            else if (!desk.IsAvailable)
            {
                return (false, "This desk is not available for now!");
            }

            newReservation.DeskId = newReservation.DeskId;
            this.reservationOperations.AddNewReservation(newReservation);

            return (true, "Reservation has been made correctly");
        }
    }
}
