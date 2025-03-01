using StillGoodToGo.Enum;
using System.ComponentModel.DataAnnotations;

namespace StillGoodToGo.Dtos
{
    /// <summary>
    /// Data Transfer Object for the Publication class.
    /// </summary>
    public class PublicationRequestDto
    {
        /// <summary>
        /// Gets or sets the description of the publication.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the published item.
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the posting date of the publication.
        /// </summary>
        [Required]
        public DateTime PostDate { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the publication.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the publication (active or inactive).
        /// </summary>
        [Required]
        public PublicationStatus Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the PublicationResponseDto.
        /// </summary>
        /// <param name="id">The unique identifier for the publication.</param>
        /// <param name="description">The description of the publication.</param>
        /// <param name="price">The price of the published item.</param>
        /// <param name="postDate">The posting date of the publication.</param>
        /// <param name="endDate">The expiration date of the publication.</param>
        /// <param name="status">The status of the publication.</param>
        public PublicationRequestDto(string description, double price, DateTime postDate, DateTime endDate, PublicationStatus status)
        {
            Description = description;
            Price = price;
            PostDate = postDate;
            EndDate = endDate;
            Status = status;
        }
    }
}