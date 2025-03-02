using StillGoodToGo.Enums;
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
        /// Gets or sets the role of the user.
        /// </summary>
        [Required]
        public Role Role { get; set; }

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


        /// <summary>
        /// Initializes a new instance of the establishment class.
        /// </summary>
        /// <param name="id">The unique identifier for the establishment.</param>
        /// <param name="username">The username of the establishment owner.</param>
        /// <param name="email">The email address of the establishment.</param>
        /// <param name="role">The role of the user, typically <see cref="Role.Establishment"/>.</param>
        /// <param name="description">A brief description of the establishment.</param>
        /// <param name="categories">A list of categories associated with the establishment.</param>
        /// <param name="latitude">The latitude coordinate of the establishment's location.</param>
        /// <param name="longitude">The longitude coordinate of the establishment's location.</param>
        /// <param name="classification">The classification rating of the establishment (0 to 5).</param>
        /// <param name="publication">A list of publications related to the establishment.</param>
        public Establishment(int id, string username, string email, Role role, string description, List<Category> categories, double latitude, double longitude, double classification, List<Publication> publication)
        {
            Id = id;
            Username = username;
            Email = email;
            Role = Role.Establishment;
            Description = description;
            Categories = categories;
            Latitude = latitude;
            Longitude = longitude;
            Classification = classification;
            Publication = publication;
        }

        /// <summary>
        /// Initializes a new instance of the establishment class.
        /// </summary>
        /// <param name="username">The username of the establishment owner.</param>
        /// <param name="email">The email address of the establishment.</param>
        /// <param name="description">A brief description of the establishment.</param>
        /// <param name="categories">A list of categories associated with the establishment.</param>
        /// <param name="latitude">The latitude coordinate of the establishment's location.</param>
        /// <param name="longitude">The longitude coordinate of the establishment's location.</param>
        /// <param name="classification">The classification rating of the establishment (0 to 5).</param>
        /// <param name="publication">A list of publications related to the establishment.</param>
        public Establishment( string username, string email,string password, string description, List<Category> categories, double latitude, double longitude, double classification, List<Publication> publication)
        {
            Username = username;
            Email = email;
            Password = password;
            Role = Role.Establishment;
            Description = description;
            Categories = categories;
            Latitude = latitude;
            Longitude = longitude;
            Classification = classification;
            Publication = publication;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Establishment"/> class.
        /// </summary>
        public Establishment()
        {
        }
    }
}
