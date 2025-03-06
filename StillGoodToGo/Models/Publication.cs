using StillGoodToGo.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StillGoodToGo.Models
{
    /// <summary>
    /// Represents a publication made by an establishment.
    /// </summary>
    public class Publication
    {
        /// <summary>
        /// Unique identifier for the publication.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Establishment associated with the publication.
        /// </summary>
        [Required]
        public Establishment Establishment { get; set; }

        /// <summary>
        /// Establishment's id associated with the publication.
        /// </summary>
        [Required]
        public int EstablishmentId { get; set; }

        /// <summary>
        /// Description of the publication.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// Price of the published item.
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Posting date of the publication (automatically assigned).
        /// </summary>
        [Required]
        public DateTime PostDate { get; } = DateTime.Now;

        /// <summary>
        /// Expiration date of the publication.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the list of status associated with the publication (Available, Sold, Unavailable).
        /// </summary>
        [Required]
        public PublicationStatus Status { get; set; } = PublicationStatus.Available;

        /// <summary>
        /// Constructor to create a publication.
        /// </summary>
        /// <param name="establishmentId">Establishment responsible for the publication.</param>
        /// <param name="description">Description of the publication.</param>
        /// <param name="price">Price of the item.</param>
        /// <param name="endDate">Expiration date.</param>
        /// <param name="status">Status of the publication.</param>
        /// <exception cref="ArgumentException">Thrown when EndDate is not after PostDate.</exception>
        public Publication(int establishmentId, string description, double price, DateTime endDate, PublicationStatus status)
        {
            EstablishmentId = establishmentId;
            Description = description;
            Price = price;
            EndDate = endDate;
            Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Publication"/> class.
        /// </summary>
        public Publication() { }

        public Publication(int id, int establishmentId, string description, double price, DateTime endDate, PublicationStatus status)
        {
            Id = id;
            EstablishmentId = establishmentId;
            Description = description;
            Price = price;
            EndDate = endDate;
            Status = status;
        }
    }
}
