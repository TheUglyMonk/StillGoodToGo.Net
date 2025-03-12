using StillGoodToGo.Enums;
using StillGoodToGo.Models;

namespace StillGoodToGo.Dtos
{
    /// <summary>
    /// Data Transfer Object for Establishment Responses
    /// </summary>
    public class EstablishmentResponseDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the establishment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the establishment owner.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address of the establishment.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the establishment.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the list of categories associated with the establishment.
        /// </summary>
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate of the establishment's location.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate of the establishment's location.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the classification rating of the establishment, ranging from 0 to 5.
        /// </summary>
        public double Classification { get; set; }

        /// <summary>
        /// Gets or sets the publications related to the establishment.
        /// </summary>
        public List<Publication> Publication { get; set; }

        public double TotalAmountReceived { get; set; }


        public bool Active { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EstablishmentRequestDto"/> class.
        /// </summary>
        public EstablishmentResponseDto(int id, string username, string email, Role role, string description, List<Category> categories, double latitude, double longitude, double classification, List<Publication> publication, bool active, double totalAmountReceived)
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
            Active = active;
            TotalAmountReceived = totalAmountReceived;
        }
    }
}