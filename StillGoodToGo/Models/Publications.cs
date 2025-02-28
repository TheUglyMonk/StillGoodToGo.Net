using StillGoodToGo.Enum;
using System.ComponentModel.DataAnnotations;

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
        /// Description of the publication.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }

        /// <summary>
        /// Price of the published item.
        /// </summary>
        [Required]
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
        /// Status of the publication (active or inactive).
        /// </summary>
        [Required]
        public PublicationStatus Status { get; set; }

        /// <summary>
        /// Constructor to create a new publication without an ID.
        /// </summary>
        /// <param name="establishment">Establishment responsible for the publication.</param>
        /// <param name="description">Description of the publication.</param>
        /// <param name="price">Price of the item.</param>
        /// <param name="postDate">Posting date.</param>
        /// <param name="endDate">Expiration date.</param>
        /// <param name="status">Status of the publication.</param>
        public Publication(Establishment establishment, string description, double price, DateTime postDate, DateTime endDate, PublicationStatus status)
        {
            Establishment = establishment;
            Description = description;
            Price = price;
            PostDate = postDate;
            EndDate = endDate;
            Status = status;
        }

        /// <summary>
        /// Constructor to create a publication with a specific ID.
        /// </summary>
        /// <param name="id">Unique identifier of the publication.</param>
        /// <param name="establishment">Establishment responsible for the publication.</param>
        /// <param name="description">Description of the publication.</param>
        /// <param name="price">Price of the item.</param>
        /// <param name="postDate">Posting date.</param>
        /// <param name="endDate">Expiration date.</param>
        /// <param name="status">Status of the publication.</param>
        public Publication(int id, Establishment establishment, string description, double price, DateTime postDate, DateTime endDate, PublicationStatus status)
        {
            Id = id;
            Establishment = establishment;
            Description = description;
            Price = price;
            PostDate = postDate;
            EndDate = endDate;
            Status = status;
        }
    }
}
