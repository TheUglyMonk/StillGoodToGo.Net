using StillGoodToGo.Dtos;
using StillGoodToGo.Enums;
using StillGoodToGo.Models;


namespace StillGoodToGo.Services.ServicesInterfaces
{
    /// <summary>
    /// Interface for the publication related services.
    /// </summary>
    public interface IPublicationService
    {

        /// <summary>
        /// Adds a new publication to the database.
        /// </summary>
        /// <param name="publication">The publication entity to be added.</param>
        /// <returns>Returns the created publication.</returns>
        Task<Publication> AddPublication(Publication publication);

        /// <summary>
        /// Retrieves a filtered list of publications based on specified criteria.
        /// </summary>
        /// <param name="category">The category of the publication.</param>
        /// <param name="latitude">The latitude for location-based filtering.</param>
        /// <param name="longitude">The longitude for location-based filtering.</param>
        /// <param name="maxDistance">The maximum distance for filtering.</param>
        /// <param name="foodType">The type of food associated with the publication.</param>
        /// <param name="minDiscount">The minimum discount percentage.</param>
        /// <returns>Returns a list of publications that match the filtering criteria.</returns>
        Task<List<Publication>> GetFilteredPublications(Category? category, double? latitude, double? longitude, double? maxDistance, string? foodType, double? minDiscount);

        /// <summary>
        /// Gets a publication by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the publication.</param>
        /// <returns>Returns the publication if found.</returns>
        Task<Publication> GetPublicationById(int id);


        /// <summary>
        /// Gets all publications.
        /// </summary>
        /// <returns></returns>
        Task<List<Publication>> GetAllPublications();

        /// <summary>
        /// Updates a publication.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publication"></param>
        /// <returns></returns>
        Task<Publication> UpdatesPublication(int id, Publication publication);

        /// <summary>
        /// Updates a publication's status.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<Publication> UpdatesPublicationStatus(int id, PublicationStatus status);

        /// <summary>
        /// Gets all publications from a specific establishment.
        /// </summary>
        /// <param name="establishmentId"></param>
        /// <returns></returns>
        Task<List<Publication>> GetPublicationsFromEstablishment(int establishmentId);

        /// <summary>
        /// Gets all publications with a specific status.
        Task<List<Publication>>GetPublicationsWithStatus(int establishmentId, PublicationStatus status);

        /// <summary>
        /// Gets all publications with a status.
        Task <List<Publication>> GetPublicationsByStatus(PublicationStatus status);

        /// <summary>
        /// Gets all publications with a specific price range.
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        Task<List<Publication>> GetPublicationsByPriceRange(double minPrice, double maxPrice);

        /// <summary>
        /// Gets all available publications.
        Task<List<Publication>> GetAvailablePublications();

        /// <summary>
        /// Updates the status of a list of publications using enddate.
        /// </summary>
        /// <param name="publications"></param>
        /// <returns></returns>
        Task<List<Publication>> UpdatePublicationsStatus();
    }
}