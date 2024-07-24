using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SoftwareMind_Intern_ChallengeDTO.DataObjects
{
    /// <summary>
    /// Location DTO.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Location ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Location name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// List of desks in this location.
        /// </summary>
        public virtual ICollection<Desk>? Desks { get; set; } = null!;
    }
}
