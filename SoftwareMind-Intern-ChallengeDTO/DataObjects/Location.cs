using System.Collections;

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
        /// List of desks in this location.
        /// </summary>
        public IEnumerable<Desk>? Desks { get; set; }
    }
}
