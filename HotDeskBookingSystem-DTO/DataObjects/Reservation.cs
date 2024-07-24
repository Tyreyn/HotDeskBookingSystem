namespace SoftwareMind_Intern_ChallengeDTO.DataObjects
{
    /// <summary>
    /// Reservation DTO.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Gets or sets reservation ID.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets employee ID responsible for reservation.
        /// </summary>
        required public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets employee responsible for reservation.
        /// </summary>
        public virtual Employee? Employee { get; set; }

        /// <summary>
        /// Gets or sets reservation start date.
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Gets or sets reservation end date.
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Gets or sets desk id to be reserved.
        /// </summary>
        required public int DeskId { get; set; }

        /// <summary>
        /// Gets or sets desk to be reserved.
        /// </summary>
        public virtual Desk? Desk { get; set; }
    }
}
