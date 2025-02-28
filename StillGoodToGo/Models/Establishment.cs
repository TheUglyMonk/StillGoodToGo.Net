using StillGoodToGo.Enum;
using System.ComponentModel.DataAnnotations;

namespace StillGoodToGo.Models
{
    /// <summary>
    /// Represents an establishment entity.
    /// </summary>
    public class Establishment
    {
        /// <summary>
        /// Unique identifier for the establishment.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Username associated with the establishment.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Email address of the establishment.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Password for the establishment account.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Role assigned to the establishment.
        /// </summary>
        [Required]
        public Role Role { get; set; } = Role.Establishment;

        /// <summary>
        /// Description of the establishment.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string? Description { get; set; }

        /// <summary>
        /// List of categories associated with the establishment.
        /// </summary>
        [Required]
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Latitude coordinate of the establishment location.
        /// </summary>
        [Required]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude coordinate of the establishment location.
        /// </summary>
        [Required]
        public double Longitude { get; set; }

        /// <summary>
        /// Classification rating of the establishment (0 to 5).
        /// </summary>
        [Required]
        [Range(0, 5)]
        public double Classification { get; set; }

        /// <summary>
        /// Publications associated with the establishment.
        /// </summary>
        [Required]
        public Publication Publication { get; set; }
    }
}
