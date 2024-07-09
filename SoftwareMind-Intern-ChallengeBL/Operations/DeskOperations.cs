using SoftwareMind_Intern_ChallengeDTO.Data;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;

namespace SoftwareMind_Intern_ChallengeBL.Operations
{
    /// <summary>
    /// Available operations for desk.
    /// </summary>
    /// <param name="hotDeskBookingSystemContext">
    /// Hot desk booking system context.
    /// </param>
    public class DeskOperations(HotDeskBookingSystemContext hotDeskBookingSystemContext) : IDeskOperations
    {
        /// <summary>
        /// Hot desk booking system context.
        /// </summary>
        private readonly HotDeskBookingSystemContext DbContext = hotDeskBookingSystemContext;

        /// <summary>
        /// Add new desk.
        /// </summary>
        /// <param name="newDesk">
        /// Desk to add.
        /// </param>
        /// <returns>
        /// True, if desk is added correctly, otherwise false.
        /// </returns>
        public bool AddDesk(Desk newDesk)
        {
            if (newDesk == null) return false;
            this.DbContext.Add(newDesk);
            this.DbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Delete desk.
        /// </summary>
        /// <param name="deskId">
        /// Desk ID to be deleted.
        /// </param>
        /// <returns>
        /// T1 - true, if deleted correctly, otherwise false.
        /// T2 - message.
        /// </returns>
        public (bool, string) DeleteDesk(int deskId)
        {
            Desk deskToBeDeleted = this.DbContext.Desks.Single(location => location.Id == deskId);
            if (deskToBeDeleted == null)
            {
                return (false, "There is no desks with this ID");
            }
            else if (deskToBeDeleted.Reservations.Any())
            {
                return (false, "There is still reservations for this desk");
            }

            this.DbContext.Desks.Remove(deskToBeDeleted);
            this.DbContext.SaveChanges();
            return (true, "Desk deleted correctly");
        }

        /// <summary>
        /// Change availability of desk.
        /// </summary>
        /// <param name="deskId">
        /// Desk ID to be updated.
        /// </param>
        /// <returns>
        /// T1 - true, if updated correctly, otherwise false.
        /// T2 - message.
        /// </returns>
        public (bool, string) ChangeAvailability(int deskId)
        {
            Desk deskToBeChanged = this.DbContext.Desks.Single(desk => desk.Id == deskId);
            if (deskToBeChanged == null)
            {
                return (false, "There is no desks with this ID");
            }

            deskToBeChanged.IsAvailable = !deskToBeChanged.IsAvailable;
            this.DbContext.Desks.Update(deskToBeChanged);
            this.DbContext.SaveChanges();
            return (true, "Desk updated correctly");
        }
    }
}
