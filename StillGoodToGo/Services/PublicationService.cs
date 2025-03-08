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
        /// <param name="publicationDto"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="InvalidPrice"></exception>
        /// <exception cref="EstablishmentNotFound"></exception>
        public async Task<PublicationResponseDto> AddPublication(PublicationRequestDto publicationDto)
        {
            // Check that the database context is initialized.
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            // Check that the publication DTO is not null.
            if (publicationDto == null)
            {
                throw new ParamIsNull();
            }

            // Check that the price is valid.
            if (publicationDto.Price <= 0)
            {
                throw new InvalidPrice();
            }

            // Find the establishment by its id.
            var establishment = await _context.Establishments.FindAsync(publicationDto.EstablishmentId);

            // Check that the establishment exists.
            if (establishment == null)
            {
                throw new EstablishmentNotFound();
            }

            // Create a new publication.
            Publication publication = new Publication
            {
                EstablishmentId = publicationDto.EstablishmentId,
                Description = publicationDto.Description,
                Price = publicationDto.Price,
                PostDate = DateTime.Now,
                EndDate = publicationDto.EndDate,
                Status = PublicationStatus.Available
            };

            // Add the publication to the database.
            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();

            return new PublicationResponseDto
            {
                Id = publication.Id,
                EstablishmentId = publication.EstablishmentId,
                Description = publication.Description,
                Price = publication.Price,
                PostDate = publication.PostDate,
                EndDate = publication.EndDate,
                Status = publicationDto.Status = PublicationStatus.Available
            };
        }

        public async Task<List<Publication>> GetFilteredPublications(Category? category, double? latitude, double? longitude, double? maxDistance, string? foodType, double? minDiscount)
        {
            // Query to get all publications
            IQueryable<Publication> query = _context.Publications.Include(p => p.Establishment);

            // Filters by category
            if (category.HasValue)
            {
                query = query.Where(p => p.Establishment.Categories.Contains(category.Value));
            }

            // Filters by distance
            if (longitude.HasValue)
            {
                query = query.Where(p => p.Establishment.Longitude == longitude.Value);
            }

            // By range
            if (latitude.HasValue && longitude.HasValue && maxDistance.HasValue)
            {
                query = query.Where(p =>
                    (6371 * Math.Acos(
                        Math.Cos(Math.PI * latitude.Value / 180) * Math.Cos(Math.PI * p.Establishment.Latitude / 180) *
                        Math.Cos(Math.PI * (p.Establishment.Longitude - longitude.Value) / 180) +
                        Math.Sin(Math.PI * latitude.Value / 180) * Math.Sin(Math.PI * p.Establishment.Latitude / 180)
                    )) <= maxDistance.Value
                );
            }

            // Filters by foodtype
            if (!string.IsNullOrEmpty(foodType))
            {
                query = query.Where(p => p.Description.Contains(foodType));
            }

            // Filters by discount
            if (minDiscount.HasValue)
            {
                query = query.Where(p => p.Price < minDiscount.Value);
            }

            var results = await query.ToListAsync();

            return results;
        }

        /// <summary>
        /// Get all Publications
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        public async Task<List<Publication>> GetAllPublications()
        {
            // Check that the database context is initialized.
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            // Get all publications
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
            // Check that the database context is initialized.
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            // Check that the id is valid.
            if (id <= 0)
            {
                throw new ParamIsNull();
            }

            // Find the publication by its id.
            var publication = await _context.Publications.FindAsync(id);

            // Check that the publication exists.
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
            // Check that the database context is initialized.
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            // Check that the updated publication is not null.
            if (updatedPublication == null)
            {
                throw new ParamIsNull();
            }

            // Check that the updated publication has a description.
            if (updatedPublication.Description.IsNullOrEmpty())
            {
                throw new InvalidParam("Description can not be empty");
            }

            // Check that the updated publication has a price.
            if (updatedPublication.Price <= 0)
            {
                throw new InvalidParam("Price can not be empty");
            }

            // Check that the updated publication has a post date.
            if (updatedPublication.EndDate == null)
            {
                throw new InvalidParam("EndDate can not be empty");
            }

            // Check that the updated publication has a post date.
            if (updatedPublication.Status == null)
            {
                throw new InvalidParam("Status can not be empty");
            }

            // Find the publication by its id.
            Publication publication = _context.Publications.FirstOrDefault(e => e.Id == id);

            // Check that the publication exists.
            if (publication == null)
            {
                throw new NotFoundInDbSet();
            }

            // Update the publication.
            publication.Status = updatedPublication.Status;
            publication.Description = updatedPublication.Description;
            publication.EndDate = updatedPublication.EndDate;
            publication.Price = updatedPublication.Price;
            publication.EstablishmentId = updatedPublication.EstablishmentId;

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
            // Check that the database context is initialized.
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            // Check that the establishment id is valid.
            if (establishmentId <= 0)
            {
                throw new ParamIsNull();
            }

            // Get all publications from the specified establishment.
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
            // Check that the database context is initialized.
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            // Check that the establishment id is valid.
            if (establishmentId <= 0)
            {
                throw new ParamIsNull();
            }

            // Check that the status is valid.
            if (!Enum.IsDefined(typeof(PublicationStatus), status))
            {
                throw new InvalidEnumValue();
            }

            // Get all publications with the specified status.
            var publications = await _context.Publications
                                              .Where(p => p.EstablishmentId == establishmentId && p.Status.ToString() == status.ToString())
                                              .ToListAsync();

            // Check that publications were found.
            if (publications == null)
            {
                throw new NoPublicationsFound();
            }

            // If the requested status is "Available", update all found publications' statuses.
            if (status == PublicationStatus.Available)
            {
                publications = await UpdatePublicationsStatus(publications);
            }

            return publications;
        }

        /// <summary>
        ///    Updates the status of a list of publications.
        /// </summary>
        /// <param name="publications"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<Publication>> UpdatePublicationsStatus(List<Publication> publications)
        {
            // Check that publications were provided.
            if (publications == null || !publications.Any())
            {
                throw new ArgumentException("No publications provided for status update.");
            }

            // Update the status of each publication.
            foreach (var publication in publications)
            {
                // Check if the publication's end date has passed.
                if (publication.EndDate < DateTime.Now)
                {
                    publication.Status = PublicationStatus.Unavailable;
                }
            }
            // Save the changes to the database.
            await _context.SaveChangesAsync();

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
            // Check that the database context is initialized.
            if (!Enum.IsDefined(typeof(PublicationStatus), status))
            {
                throw new InvalidEnumValue();
            }

            // Get all publications with the specified status.
            var publications = await _context.Publications
                                             .Where(p => p.Status.ToString() == status.ToString())
                                             .ToListAsync();

            // Check that publications were found.
            if (publications == null || publications.Count == 0)
            {
                throw new NoPublicationsFound();
            }

            return publications;
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
            // Check that the database context is initialized.
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            // Check that the price range is valid.
            if (minPrice < 0 || maxPrice < 0)
            {
                throw new InvalidParam();
            }

            // Check that the min price is not greater than the max price.
            if (minPrice > maxPrice)
            {
                throw new InvalidParam("Min price can not be greater than max price");
            }

            // Get all publications with a price within the specified range.
            var publications = await _context.Publications
                                              .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                                              .ToListAsync();

            // Check that publications were found.
            if (publications == null || publications.Count == 0)
            {
                throw new NoPublicationsFound();
            }
            return publications;
        }
    }
}