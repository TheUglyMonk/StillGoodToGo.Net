using StillGoodToGo.Dtos;
using StillGoodToGo.Models;


namespace StillGoodToGo.Services.ServicesInterfaces
{
    public interface IPublicationService
    {
        Task<Publication> AddPublication(Publication publication);
    }
}
