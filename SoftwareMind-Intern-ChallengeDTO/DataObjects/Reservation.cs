using System.ComponentModel.DataAnnotations;

namespace SoftwareMind_Intern_ChallengeDTO.DataObjects
{
    /// <summary>
    /// Reservation DTO.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Reservation ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Employee responsible for reservation.
        /// </summary>
        [Required]
        public required Employee Employee { get; set; }

        /// <summary>
        /// Reservation start data.
        /// </summary>
        [Required]
        public DateTime DataStart { get; set; }

        /// <summary>
        /// Reservation end data.
        /// </summary>
        [Required]
        public DateTime DataEnd { get; set; }

        /// <summary>
        /// Desk to be reserved.
        /// </summary>
        [Required]
        public required Desk Desk { get; set; }

    }
}
