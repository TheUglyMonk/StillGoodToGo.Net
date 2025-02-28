using StillGoodToGo.Enum;
using System.ComponentModel.DataAnnotations;

namespace StillGoodToGo.Models
{
    /// <summary>
    /// Represents an establishment entity with details such as location, classification, categories, and publications.
    /// </summary>
    public class Establishment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the establishment.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the establishment owner.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address of the establishment.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password for the establishment account.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role of the user. Defaults to <see cref="Role.Establishment"/>.
        /// </summary>
        [Required]
        public Role Role { get; set; } = Role.Establishment;

        /// <summary>
        /// Gets or sets a brief description of the establishment.
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the list of categories associated with the establishment.
        /// </summary>
        [Required]
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate of the establishment's location.
        /// </summary>
        [Required]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate of the establishment's location.
        /// </summary>
        [Required]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the classification rating of the establishment, ranging from 0 to 5.
        /// </summary>
        [Required]
        [Range(0, 5)]
        public double Classification { get; set; }

        /// <summary>
        /// Gets or sets the publications related to the establishment.
        /// </summary>
        [Required]
        public List<Publication> Publication { get; set; }
    }
}
