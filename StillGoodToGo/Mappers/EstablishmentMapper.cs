using StillGoodToGo.Dtos;
using StillGoodToGo.Enums;
using StillGoodToGo.Models;

namespace StillGoodToGo.Mappers
{
    /// <summary>
    /// Provides mapping functions to convert between different Establishment-related DTOs and models.
    /// </summary>
    public class EstablishmentMapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EstablishmentMapper"/> class.
        /// </summary>
        public EstablishmentMapper() { }

        /// <summary>
        /// Converts an <see cref="EstablishmentResponseDto"/> to an <see cref="Establishment"/> model.
        /// </summary>
        /// <param name="establishmentResponse">The response DTO containing establishment data.</param>
        /// <returns>An <see cref="Establishment"/> model.</returns>
        public Establishment EstablishmentResponseToEstablishment(EstablishmentResponseDto establishmentResponse)
        {
            return new Establishment(
                establishmentResponse.Id,
                establishmentResponse.Username,
                establishmentResponse.Email,
                establishmentResponse.Role,
                establishmentResponse.Description,
                establishmentResponse.Categories,
                establishmentResponse.Latitude,
                establishmentResponse.Longitude,
                establishmentResponse.Classification,
                establishmentResponse.Publication,
                establishmentResponse.Active
            );
        }

        /// <summary>
        /// Converts an <see cref="EstablishmentRequestDto"/> to an <see cref="Establishment"/> model.
        /// </summary>
        /// <param name="establishmentResponse">The request DTO containing establishment data.</param>
        /// <returns>An <see cref="Establishment"/> model.</returns>
        public Establishment EstablishmentRequestToEstablishment(EstablishmentRequestDto establishmentResponse)
        {
            return new Establishment(
                establishmentResponse.Username,
                establishmentResponse.Email,
                establishmentResponse.Password,
                establishmentResponse.Description,
                establishmentResponse.Categories,
                establishmentResponse.Latitude,
                establishmentResponse.Longitude,
                establishmentResponse.Classification
            );
        }

        /// <summary>
        /// Converts an <see cref="Establishment"/> model to an <see cref="EstablishmentResponseDto"/>.
        /// </summary>
        /// <param name="establishment">The establishment model.</param>
        /// <returns>An <see cref="EstablishmentResponseDto"/> containing establishment data.</returns>
        public EstablishmentResponseDto EstablishmentToEstablishmentResponse(Establishment establishment)
        {
            return new EstablishmentResponseDto(
                establishment.Id,
                establishment.Username,
                establishment.Email,
                establishment.Role,
                establishment.Description,
                establishment.Categories,
                establishment.Latitude,
                establishment.Longitude,
                establishment.Classification,
                establishment.Publication,
                establishment.Active,
                establishment.TotalAmountReceived
            );
        }

        /// <summary>
        /// Converts an <see cref="Establishment"/> model to an <see cref="EstablishmentRequestDto"/>.
        /// </summary>
        /// <param name="establishment">The establishment model.</param>
        /// <returns>An <see cref="EstablishmentRequestDto"/> containing establishment data.</returns>
        public EstablishmentRequestDto EstablishmentToEstablishmentRequest(Establishment establishment)
        {
            return new EstablishmentRequestDto(
                establishment.Username,
                establishment.Email,
                establishment.Password,
                establishment.Description,
                establishment.Categories,
                establishment.Latitude,
                establishment.Longitude,
                establishment.Classification,
                establishment.TotalAmountReceived
            );
        }
    }
}
