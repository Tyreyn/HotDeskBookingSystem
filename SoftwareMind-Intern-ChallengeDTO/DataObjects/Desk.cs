using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /// Location ID of Desk.
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Location of Desk.
        /// </summary>
        public virtual Location? Location { get; set; }

        /// <summary>
        /// Collection of reservations for desk.
        /// </summary>
        public virtual IList<Reservation>? Reservations { get; set; } = null!;
    }
}
