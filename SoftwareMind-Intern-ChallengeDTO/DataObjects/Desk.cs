using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareMind_Intern_ChallengeDTO.DataObjects
{
    /// <summary>
    /// Desk DTO.
    /// </summary>
    public class Desk
    {
        /// <summary>
        /// Desk Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Indicates whenever desk is available to reserve.
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Location of Desk.
        /// </summary>
        public Location? Location { get; set; }

        /// <summary>
        /// Collection of reservations for desk.
        /// </summary>
        public IEnumerable<Reservation>? Reservations { get; set; }
    }
}
