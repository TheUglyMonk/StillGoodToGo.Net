using StillGoodToGo.Enums;


namespace StillGoodToGo.Dtos
{
    /// <summary>
    /// Data Transfer Object for the Publication class.
    /// </summary>
    public class PublicationRequestDto
    {
        /// <summary>
        /// Gets or sets the establishmentId of the publication.
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
        /// Gets or sets the posting date of the publication.
        /// </summary>
        public DateTime PostDate { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the publication.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the status of the publication (active or inactive).
        /// </summary>
        public PublicationStatus Status { get; set; } = PublicationStatus.Available;

        /// <summary>
        /// Initializes a new instance of the PublicationResponseDto.
        /// </summary>
        /// <param name="id">The unique identifier for the publication.</param>
        /// <param name="description">The description of the publication.</param>
        /// <param name="price">The price of the published item.</param>
        /// <param name="postDate">The posting date of the publication.</param>
        /// <param name="endDate">The expiration date of the publication.</param>
        /// <param name="status">The status of the publication.</param>
        public PublicationRequestDto(int establishmentId, string description, double price, DateTime postDate, DateTime endDate, PublicationStatus status)
        {
            EstablishmentId = establishmentId;
            Description = description;
            Price = price;
            EndDate = endDate;
            Status = status;
        }

        /// <summary>
        /// Empty constructor for the PublicationResponseDto.
        /// </summary>
        public PublicationRequestDto() { }

        /// <summary>
        /// Initializes a new instance of the PublicationResponseDto.
        /// </summary>
        /// <param name="establishmentId"></param>
        /// <param name="description"></param>
        /// <param name="price"></param>
        /// <param name="endDate"></param>
        /// <param name="status"></param>
        public PublicationRequestDto(int establishmentId, string description, double price, DateTime endDate, PublicationStatus status)
        {
            EstablishmentId = establishmentId;
            Description = description;
            Price = price;
            EndDate = endDate;
            Status = status;
        }
    }
}