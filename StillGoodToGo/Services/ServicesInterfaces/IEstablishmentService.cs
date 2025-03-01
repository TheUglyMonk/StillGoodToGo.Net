using StillGoodToGo.Models;

namespace StillGoodToGo.Services.ServicesInterfaces
{
    /// <summary>
    /// Interface for the establishment related services.
    /// </summary>
    public interface IEstablishmentService
    {
        /// <summary>
        /// Adds a new establishment to the database.
        /// </summary>
        Task<Establishment> AddEstablishment(Establishment establishment);
    }
}
