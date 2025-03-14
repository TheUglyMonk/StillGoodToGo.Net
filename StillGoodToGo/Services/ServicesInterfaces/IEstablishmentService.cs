﻿using StillGoodToGo.Dtos;
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

        /// <summary>
        /// Updates an establishment.
        /// </summary>
        Task<Establishment> UpdatesEstablishment(int id, Establishment establishment);

        /// <summary>
        /// Deactivates/Turns isActive to false an establishment.
        /// </summary>
        Task<Establishment> DeactivateEstablishment(int id);

        /// <summary>
        /// Gets establishments by id.
        /// </summary>
        Task<Establishment> GetEstablishmentById(int id);

        /// <summary>
        /// Gets estbalishments by Description.
        /// </summary>
        Task<List<Establishment>> GetEstablishmentsByDescription(string description);

        /// <summary>
        /// Gets allestbalishments.
        /// </summary>
        Task<List<Establishment>> GetEstablishments();

        /// <summary>
        /// Gets all active estbalishments.
        /// </summary>
        Task<List<Establishment>> GetActiveEstablishments();

        /// <summary>
        /// Adds Amount Received on sale
        /// </summary>
        Task<Establishment> AddsAmountReceived(int id, double amount);

        /// <summary>
        /// Gets establishments by email.
        /// </summary>
        Task<Establishment> GetEstablishmentByEmail(string email);

        /// <summary>
        /// Updates establishment's classification.
        /// </summary>
        Task<Establishment> UpdateClassification(int id, double classification);
    }
}
