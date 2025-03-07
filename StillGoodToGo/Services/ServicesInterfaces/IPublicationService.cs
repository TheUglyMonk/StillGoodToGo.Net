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
        Task<PublicationResponseDto> AddPublication(PublicationRequestDto publicationDto);

        Task<List<Publication>> GetFilteredPublications(Category? category, double? latitude, double? longitude, double? maxDistance, string? foodType, double? minDiscount);

        /// <summary>
        /// Gets a publication by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// Gets all publications from a specific establishment.
        /// </summary>
        /// <param name="establishmentId"></param>
        /// <returns></returns>
        Task<List<Publication>> GetPublicationsFromEstablishment(int establishmentId);

        /// <summary>
        /// Gets all publications From Establishment with a specific status.
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
        /// Updates the status of a list of publications using enddate.
        /// </summary>
        /// <param name="publications"></param>
        /// <returns></returns>
        Task<List<Publication>> UpdatePublicationsStatus(List<Publication> publications);
    }
}