using StillGoodToGo.Dtos;


namespace StillGoodToGo.Services.ServicesInterfaces
{
    public interface IPublicationService
    {
        Task<PublicationResponseDto> AddPublication(PublicationRequestDto publicationDto);
    }
}
