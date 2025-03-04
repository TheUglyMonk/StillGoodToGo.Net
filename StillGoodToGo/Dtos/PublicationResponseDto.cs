using StillGoodToGo.Enums;
using System.ComponentModel.DataAnnotations;

namespace StillGoodToGo.Dtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for returning publication details.
    /// </summary>
    public class PublicationResponseDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the publication.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the establishment that created the publication.
        /// </summary>
        public int EstablishmentId { get; set; }

        /// <summary>
        /// Gets or sets the description of the publication.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the published item.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the date when the publication was created.
        /// </summary>
        public DateTime PostDate { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the publication.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the publication (e.g., Active or Inactive).
        /// </summary>
        public PublicationStatus Status { get; set; }

        public PublicationResponseDto() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationResponseDto"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the publication.</param>
        /// <param name="establishmentId">The ID of the establishment that owns this publication.</param>
        /// <param name="description">A short description of the publication.</param>
        /// <param name="price">The price of the published item.</param>
        /// <param name="postDate">The date when the publication was created.</param>
        /// <param name="endDate">The expiration date of the publication.</param>
        /// <param name="status">The current status of the publication.</param>
        public PublicationResponseDto(int id, int establishmentId, string description, double price, DateTime postDate, DateTime endDate, PublicationStatus status)
        {
            Id = id;
            EstablishmentId = establishmentId;
            Description = description;
            Price = price;
            PostDate = postDate;
            EndDate = endDate;
            Status = status;
        }
    }
}
