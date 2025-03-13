using StillGoodToGo.Enums;
<<<<<<< HEAD
=======

>>>>>>> 17223f5c14c9c7cc9a4e8a2e930ac9a034fa7124

namespace StillGoodToGo.Dtos
{
    /// <summary>
    /// Data Transfer Object for creating or updating an establishment.
    /// </summary>
    public class EstablishmentRequestDto
    {
        /// <summary>
        /// Gets or sets the username of the establishment owner.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address of the establishment.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Password of the establishment.
        /// </summary>
        public string Password { get; set; }

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
        /// Gets or sets if the establishment is active/visible
        /// </summary>
        public bool Active { get; set; }

        public double TotalAmountReceived { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="EstablishmentRequestDto"/> class.
        /// </summary>
        /// <param name="username">The username of the establishment owner.</param>
        /// <param name="email">The email address of the establishment.</param>
        /// <param name="password">The password for the establishment account.</param>
        /// <param name="description">A brief description of the establishment.</param>
        /// <param name="categories">A list of categories associated with the establishment.</param>
        /// <param name="latitude">The latitude coordinate of the establishment's location.</param>
        /// <param name="longitude">The longitude coordinate of the establishment's location.</param>
        /// <param name="classification">The classification rating of the establishment (0 to 5).</param>
        /// <param name="publication">A list of publications related to the establishment.</param>
        public EstablishmentRequestDto(string username, string email, string password, string description, List<Category>? categories, double latitude, double longitude, double classification, double totalAmountReceived)
        {
            Username = username;
            Email = email;
            Password = password;
            Categories = categories;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            Classification = classification;
            Active = true;
            TotalAmountReceived = totalAmountReceived;
        }
    }
}