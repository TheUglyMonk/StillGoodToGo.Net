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

    }
}
