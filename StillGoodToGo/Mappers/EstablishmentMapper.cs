using StillGoodToGo.Dtos;
using StillGoodToGo.Enums;
using StillGoodToGo.Models;

namespace StillGoodToGo.Mappers
{
    public class EstablishmentMapper
    {
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
                establishmentResponse.Publication
            );
        }

        public Establishment EstablishmentRequestToEstablishment(EstablishmentResponseDto establishmentResponse)
        {
            return new Establishment(
                establishmentResponse.Username,
                establishmentResponse.Email,
                establishmentResponse.Role,
                establishmentResponse.Description,
                establishmentResponse.Categories,
                establishmentResponse.Latitude,
                establishmentResponse.Longitude,
                establishmentResponse.Classification,
                establishmentResponse.Publication
            );
        }

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
                establishment.Publication
            );
        }
    }
}
