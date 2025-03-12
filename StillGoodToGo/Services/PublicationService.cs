using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Server;
using StillGoodToGo.DataContext;
using StillGoodToGo.Dtos;
using StillGoodToGo.Enums;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Mappers;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;

namespace StillGoodToGo.Services
{
    /// <summary>
    /// Publication Service.
    /// </summary>
    public class PublicationService : IPublicationService
    {
        private readonly StillGoodToGoContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicationService"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PublicationService(StillGoodToGoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        /// <summary>
        /// Adds a new publication to the database.
        /// </summary>
        /// <param name="publication">The publication entity to be added.</param>
        /// <returns>Returns the created publication.</returns>
        /// <exception cref="DbSetNotInitialize">Thrown when the database context is not initialized.</exception>
        /// <exception cref="ParamIsNull">Thrown when the provided publication data is null.</exception>
        /// <exception cref="InvalidPrice">Thrown when the publication price is less than or equal to zero.</exception>
        /// <exception cref="InvalidEndDate">Thrown when the publication's end date is in the past.</exception>
        /// <exception cref="InvalidParam">Thrown when the description is too short.</exception>
        /// <exception cref="EstablishmentNotFound">Thrown when the establishment associated with the publication is not found.</exception>
        public async Task<Publication> AddPublication(Publication publication)
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            if (publication == null)
            {
                throw new ParamIsNull();
            }

            if (publication.Price <= 0)
            {
                throw new InvalidPrice();
            }

            if (publication.EndDate <= DateTime.Now)
            {
                throw new InvalidEndDate();
            }

            if (publication.Description.IsNullOrEmpty())
            {
                throw new InvalidParam("Description is too short.");
            }

            var establishment = await _context.Establishments.FindAsync(publication.EstablishmentId);

            if (establishment == null)
            {
                throw new EstablishmentNotFound();
            }

            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();

            return new Publication
            {
                Id = publication.Id,
                EstablishmentId = publication.EstablishmentId,
                Description = publication.Description,
                Price = publication.Price,
                EndDate = publication.EndDate,
                Status = publication.Status = PublicationStatus.Available
            };
        }

        /// <summary>
        /// Retrieves a filtered list of publications based on specified criteria.
        /// </summary>
        /// <param name="category">The category of the publication.</param>
        /// <param name="latitude">The latitude for location-based filtering.</param>
        /// <param name="longitude">The longitude for location-based filtering.</param>
        /// <param name="maxDistance">The maximum distance for filtering.</param>
        /// <param name="foodType">The type of food associated with the publication.</param>
        /// <param name="minDiscount">The minimum discount value to filter publications.</param>
        /// <returns>Returns a list of publications that match the filtering criteria.</returns>
        public async Task<List<Publication>> GetFilteredPublications(Category? category, double? latitude, double? longitude, double? maxDistance, string? foodType, double? minDiscount)
        {
            IQueryable<Publication> query = _context.Publications.Include(p => p.Establishment);

            Console.WriteLine($"Total publications in DB: {_context.Publications.Count()}");

            if (category.HasValue)
            {
                query = query.Where(p => p.Establishment.Categories.Contains(category.Value));
                Console.WriteLine($"Filtering by category: {category.Value}");
            }

            if (longitude.HasValue)
            {
                query = query.Where(p => p.Establishment.Longitude == longitude.Value);
            }

            if (latitude.HasValue && longitude.HasValue && maxDistance.HasValue)
            {
                query = query.Where(p =>
                    (6371 * Math.Acos(
                        Math.Cos(Math.PI * latitude.Value / 180) * Math.Cos(Math.PI * p.Establishment.Latitude / 180) *
                        Math.Cos(Math.PI * (p.Establishment.Longitude - longitude.Value) / 180) +
                        Math.Sin(Math.PI * latitude.Value / 180) * Math.Sin(Math.PI * p.Establishment.Latitude / 180)
                    )) <= maxDistance.Value
                );
                Console.WriteLine($"Filtering by location: {latitude.Value}, {longitude.Value}, {maxDistance.Value}");
            }

            if (!string.IsNullOrEmpty(foodType))
            {
                query = query.Where(p => p.Description.Contains(foodType));
                Console.WriteLine($"Filtering by food type: {foodType}");
            }

            if (minDiscount.HasValue)
            {
                query = query.Where(p => p.Price < minDiscount.Value);
                Console.WriteLine($"Filtering by minimum discount: {minDiscount.Value}");
            }

            var results = await query.ToListAsync();

            Console.WriteLine($"Total results found: {results.Count}");

            return results;
        }


        /// <summary>
        /// Get all Publications
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        public async Task<List<Publication>> GetAllPublications()
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            var publications = await _context.Publications.ToListAsync();
            if (publications == null || publications.Count == 0)
            {
                throw new NoPublicationsFound();
            }

            return publications;
        }

        /// <summary>
        /// Gets Publication by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        public async Task<Publication> GetPublicationById(int id)
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            if (id <= 0)
            {
                throw new ParamIsNull();
            }

            var publication = await _context.Publications.FindAsync(id);

            if (publication == null)
            {
                throw new NotFoundInDbSet();
            }

            return publication;
        }

        /// <summary>
        /// Updates a publication
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPublication"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="InvalidParam"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        public async Task<Publication> UpdatesPublication(int id, Publication updatedPublication)
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            if (updatedPublication == null)
            {
                throw new ParamIsNull();
            }

            if (updatedPublication.Description.IsNullOrEmpty())
            {
                throw new InvalidParam("Description can not be empty");
            }

            if (updatedPublication.Price <= 0)
            {
                throw new InvalidParam("Price can not be empty");
            }

            if (updatedPublication.EndDate < DateTime.Now)
            {
                throw new InvalidParam("End date is invalid");
            }

            if (updatedPublication.Status == PublicationStatus.Sold)
            {
                throw new InvalidParam("Can't update an publication to sold, only unavailable");
            }

            Publication publication = _context.Publications.FirstOrDefault(e => e.Id == id);

            if (publication == null)
            {
                throw new NotFoundInDbSet();
            }

            publication.Status = updatedPublication.Status;
            publication.Description = updatedPublication.Description;
            publication.EndDate = updatedPublication.EndDate;
            publication.Price = updatedPublication.Price;
            publication.EstablishmentId = updatedPublication.EstablishmentId;

            await _context.SaveChangesAsync();

            return publication;
        }

        /// <summary>
        /// Updates a publication's status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPublication"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="InvalidParam"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        public async Task<Publication> UpdatesPublicationStatus(int id, PublicationStatus status)
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            if (!Enum.IsDefined(typeof(PublicationStatus), status))
            {
                throw new InvalidEnumValue("Invalid status.");
            }

            Publication publication = _context.Publications.FirstOrDefault(e => e.Id == id);

            if (publication == null)
            {
                throw new NotFoundInDbSet();
            }

            publication.Status = status;

            await _context.SaveChangesAsync();

            return publication;
        }

        /// <summary>
        ///     Gets all publications from a specific establishment.
        /// </summary>
        /// <param name="establishmentId"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="NoPublicationsFound"></exception>
        public async Task<List<Publication>> GetPublicationsFromEstablishment(int establishmentId)
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            if (establishmentId <= 0)
            {
                throw new ParamIsNull();
            }

            var publications = await _context.Publications.Where(p => p.EstablishmentId == establishmentId).ToListAsync();
            if (publications == null || publications.Count == 0)
            {
                throw new NoPublicationsFound();
            }

            return publications;
        }

        /// <summary>
        /// Gets all publications with a specific status.
        /// </summary>
        /// <param name="establishmentId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="InvalidEnumValue"></exception>
        /// <exception cref="NoPublicationsFound"></exception>
        public async Task<List<Publication>> GetPublicationsWithStatus(int establishmentId, PublicationStatus status)
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            if (establishmentId <= 0)
            {
                throw new ParamIsNull();
            }

            if (!Enum.IsDefined(typeof(PublicationStatus), status))
            {
                throw new InvalidEnumValue();
            }

            var publications = await _context.Publications
                                              .Where(p => p.EstablishmentId == establishmentId && p.Status == status)
                                              .ToListAsync();

            if (publications == null || publications.Count == 0)
            {
                throw new NoPublicationsFound();
            }

            return publications;
        }

        /// <summary>
        ///     Gets all publications with a specific status.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="InvalidEnumValue"></exception>
        /// <exception cref="NoPublicationsFound"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<List<Publication>> GetPublicationsByStatus(PublicationStatus status)
        {
            try
            {
                if (_context.Publications == null)
                {
                    throw new DbSetNotInitialize();
                }

                if (!Enum.IsDefined(typeof(PublicationStatus), status))
                {
                    throw new InvalidEnumValue("Invalid status.");
                }

                var publications = await _context.Publications
                                                 .Where(p => p.Status == status)
                                                 .ToListAsync();

                if (publications == null || publications.Count == 0)
                {
                    throw new NoPublicationsFound();
                }

                return publications;
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching publications by status", ex);
            }
        }

        /// <summary>
        /// Gets all publications with a specific price range.
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="InvalidParam"></exception>
        /// <exception cref="NoPublicationsFound"></exception>
        public async Task<List<Publication>> GetPublicationsByPriceRange(double minPrice, double maxPrice)
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            if (minPrice < 0 || maxPrice < 0)
            {
                throw new InvalidParam();
            }

            if (minPrice > maxPrice)
            {
                throw new InvalidParam("Min price can not be greater than max price");
            }

            var publications = await _context.Publications
                                              .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                                              .ToListAsync();

            if (publications == null || publications.Count == 0)
            {
                throw new NoPublicationsFound();
            }
            return publications;
        }

        /// <summary>
        /// Retrieves a list of available publications.
        /// </summary>
        /// <returns>Returns a list of publications with status 'Available'.</returns>
        /// <exception cref="DbSetNotInitialize">Thrown when the database context is not initialized.</exception>
        /// <exception cref="NoPublicationsFound">Thrown when no available publications are found.</exception>
        public async Task<List<Publication>> GetAvailablePublications()
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            await UpdatePublicationsStatus();

            List<Publication> publications = await _context.Publications
                                                           .Where(p => p.Status == PublicationStatus.Available)
                                                           .ToListAsync();
            if (publications == null || publications.Count == 0)
            {
                throw new NoPublicationsFound();
            }

            return publications;
        }

        /// <summary>
        /// Updates the status of publications based on their end date.
        /// </summary>
        /// <returns>Returns a list of updated publications that have changed status.</returns>
        /// <exception cref="DbSetNotInitialize">Thrown when the database context is not initialized.</exception>
        /// <exception cref="NoPublicationsFound">Thrown when no available publications are found.</exception>
        public async Task<List<Publication>> UpdatePublicationsStatus()
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            List<Publication> publications = await _context.Publications
                                                           .Where(p => p.Status == PublicationStatus.Available)
                                                           .ToListAsync();

            if (publications == null || publications.Count == 0)
            {
                throw new NoPublicationsFound();
            }

            List<Publication> publicationsUpdated = new List<Publication>();

            foreach (Publication p in publications)
            {
                if (p.EndDate < DateTime.Now)
                {
                    p.Status = PublicationStatus.Unavailable;
                    publicationsUpdated.Add(p);
                }
            }

            await _context.SaveChangesAsync();

            return publicationsUpdated;
        }

    }
}