using StillGoodToGo.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
        [NotNull]
        public double Price { get; set; }

        /// <summary>
        /// Posting date of the publication.
        /// </summary>
        [Required]
        public DateTime PostDate { get; set; }

        /// <summary>
        /// Expiration date of the publication.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the list of status associated with the publication (Available, Sold, Unavailable).
        /// </summary>
        [Required]
        public List<PublicationStatus> Status { get; set; }

        /// <summary>
        /// Constructor to create a publication with a specific ID.
        /// </summary>
        /// <param name="id">Unique identifier of the publication.</param>
        /// <param name="establishmentId">Establishment responsible for the publication.</param>
        /// <param name="description">Description of the publication.</param>
        /// <param name="price">Price of the item.</param>
        /// <param name="postDate">Posting date.</param>
        /// <param name="endDate">Expiration date.</param>
        /// <param name="status">Status of the publication.</param>
        public Publication(int id, int establishmentId, string description, double price, DateTime postDate, DateTime endDate, List<PublicationStatus> status)
        {
            Id = id;
            EstablishmentId = establishmentId;
            Description = description;
            Price = price;
            PostDate = DateTime.Now;
            EndDate = endDate;
            Status = status;
        }

        /// <summary>
        /// Constructor to create a new publication without an ID.
        /// </summary>
        /// <param name="establishmentId">Establishment responsible for the publication.</param>
        /// <param name="description">Description of the publication.</param>
        /// <param name="price">Price of the item.</param>
        /// <param name="postDate">Posting date.</param>
        /// <param name="endDate">Expiration date.</param>
        /// <param name="status">Status of the publication.</param>
        public Publication(int establishmentId, string description, double price, DateTime postDate, DateTime endDate, List<PublicationStatus> status)
        {
            EstablishmentId = establishmentId;
            Description = description;
            Price = price;
            PostDate = DateTime.Now;
            EndDate = endDate;
            Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Publication"/> class.
        /// </summary>
        public Publication()
        {
        }
    }
}
