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
    }
}