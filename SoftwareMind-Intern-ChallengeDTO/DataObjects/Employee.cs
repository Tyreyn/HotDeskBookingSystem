using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SoftwareMind_Intern_ChallengeDTO.DataObjects
{
    /// <summary>
    /// Employee DTO.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Employee ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Employee name.
        /// </summary>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// Indicates whenever Employee is Admin.
        /// </summary>
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }
    }
}
