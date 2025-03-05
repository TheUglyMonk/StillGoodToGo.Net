using StillGoodToGo.Dtos;
using StillGoodToGo.Models;

namespace StillGoodToGo.Mappers
{
    /// <summary>
    /// Provides mapping functions to convert between different Publication-related DTOs and models.
    /// </summary>
    public class PublicationMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationMapper"/> class.
        /// </summary>
        public PublicationMapper() { }

        /// <summary>
        /// Converts a <see cref="PublicationRequestDto"/> to a <see cref="Publication"/> model.
        /// </summary>
        /// <param name="publicationDto">The request DTO containing publication data.</param>
        /// <returns>A <see cref="Publication"/> model.</returns>
        public Publication PublicationRequestToPublication(PublicationRequestDto publicationDto)
        {
            return new Publication
            (
                publicationDto.EstablishmentId,
                publicationDto.Description,
                publicationDto.Price,
                publicationDto.EndDate,
                publicationDto.Status
            );
        }

        /// <summary>
        /// Converts a <see cref="PublicationResponseDto"/> to a <see cref="Publication"/> model.
        /// </summary>
        /// <param name="publicationDto">The response DTO containing publication data.</param>
        /// <returns>A <see cref="Publication"/> model.</returns>
        public Publication PublicationResponseToPublication(PublicationResponseDto publicationDto)
        {
            return new Publication
            (
                publicationDto.Id,
                publicationDto.EstablishmentId,
                publicationDto.Description,
                publicationDto.Price,
                publicationDto.EndDate,
                publicationDto.Status
            );
        }

        /// <summary>
        /// Converts a <see cref="Publication"/> model to a <see cref="PublicationRequestDto"/>.
        /// </summary>
        /// <param name="publication">The publication model.</param>
        /// <returns>A <see cref="PublicationRequestDto"/> containing publication data.</returns>
        public PublicationRequestDto PublicationToPublicationRequest(Publication publication)
        {
            return new PublicationRequestDto
            (
                publication.EstablishmentId,
                publication.Description,
                publication.Price,
                publication.EndDate,
                publication.Status
            );
        }

        /// <summary>
        /// Converts a <see cref="Publication"/> model to a <see cref="PublicationResponseDto"/>.
        /// </summary>
        /// <param name="publication">The publication model.</param>
        /// <returns>A <see cref="PublicationResponseDto"/> containing publication data.</returns>
        public PublicationResponseDto PublicationToPublicationResponse(Publication publication)
        {
            return new PublicationResponseDto
            (
                publication.Id,
                publication.EstablishmentId,
                publication.Description,
                publication.Price,
                publication.EndDate,
                publication.Status
            );
        }
    }
}
