namespace SoftwareMind_Intern_ChallengeDTO.DataObjects
{
    /// <summary>
    /// Employee DTO.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets employee id.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Gets or sets employee email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets employee password.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets employee role.
        /// </summary>
        public string? Role { get; set; }

        /// <summary>
        /// Gets or sets employees reservations.
        /// </summary>
        public virtual ICollection<Reservation>? Reservations { get; set; } = null!;
    }
}
