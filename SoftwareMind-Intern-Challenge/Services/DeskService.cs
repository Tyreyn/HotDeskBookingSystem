using SoftwareMind_Intern_ChallengeBL.Operations;
using SoftwareMind_Intern_ChallengeDTO.DataObjects;
using System.Collections.Generic;

namespace SoftwareMind_Intern_Challenge.Services
{
    public class DeskService(DeskOperations deskOperations, LocationOperations locationOperations)
    {
        private readonly DeskOperations deskOperations = deskOperations;

        private readonly LocationOperations locationOperations = locationOperations;

        public bool AddDesk(Desk newDesk)
        {
            this.deskOperations.AddDesk(newDesk);
            return true;
        }
        public IList<Desk> GetDesks()
        {
            IList<Desk> desks = this.deskOperations.GetAllDesks();
            return desks;
        }

        public IList<Desk> FilterDesks()
        {
            return this.deskOperations.GetAllDesks().OrderBy(desk => desk.LocationId).ToList();
        }

        public (bool, string) ChangeDeskLocation(int newLocationId, int deskId)
        {
            Desk? deskToChange = this.deskOperations.GetDeskById(deskId);
            int oldLocationId = deskToChange.LocationId;
            if (deskToChange == null) { return (false, "There is no desk with this ID"); }
            deskToChange.LocationId = newLocationId;
            deskToChange.Location = this.locationOperations.GetLocationById(newLocationId);
            deskToChange.IsAvailable = true;

            bool result = this.deskOperations.UpdateDesk(deskToChange);
            if (!result)
            {
                return (false, "Something went wrong with updating location");
            }

            return (true, $"Location of desk {deskId} changed from {oldLocationId} to {newLocationId}");
        }

        public bool AddNewDesk(Desk desk)
        {
            Desk deskToCheck = this.deskOperations.GetDeskById(desk.Id);
            if (deskToCheck == null)
            {
                return false;
            }
            else
            {
                this.deskOperations.AddDesk(desk);
                return true;
            }
        }

        public Desk GetDeskById(int deskId)
        {
            return this.deskOperations.GetDeskById(deskId);
        }

        public bool ChangeDeskAvailable(int deskId)
        {
            Desk deskToBeChanged = this.deskOperations.GetDeskById(deskId);

            if (deskToBeChanged == null)
            {
                return false;
            }

            deskToBeChanged.IsAvailable = !deskToBeChanged.IsAvailable;

            this.deskOperations.UpdateDesk(deskToBeChanged);
            return true;
        }

        public (bool, string) DeleteDesk(int deskId)
        {
            Desk deskToBeDeleted = this.deskOperations.GetDeskById(deskId);

            if (deskToBeDeleted == null)
            {
                return (false, "There is no desks with this ID");
            }
            else if (deskToBeDeleted.Reservations != null)
            {
                return (false, "There is still reservations for this desk");
            }

            this.deskOperations.DeleteDesk(deskToBeDeleted);
            return (true, $"Desk {deskId} deleted successfully");
        }
    }
}
