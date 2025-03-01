using StillGoodToGo.DataContext;
using StillGoodToGo.Enums;
using StillGoodToGo.Models;

namespace StillGoodToGo.Services
{
    /// <summary>
    /// Service class for managing establishments.
    /// </summary>
    public class EstablishmentService
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly StillGoodToGoContext _context;

        /// <summary>
        /// Initializes a new instance of the EstablishmentService class.
        /// </summary>
        public EstablishmentService(StillGoodToGoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new establishment to the database.
        /// </summary>
        public async Task<Establishment> AddEstablishment(Establishment establishment)
        {
            if (_context.Establishments == null)
            {
                throw new Exception();
            }

            if (establishment == null)
            {
                throw new Exception();
            }

            // Validate that the email and location are unique
            var emailExists = _context.Establishments.Any(e => e.Email == establishment.Email);
            var locationExists = _context.Establishments.Any(e => e.Latitude == establishment.Latitude && e.Longitude == establishment.Longitude);
            if (emailExists || locationExists)
            {
                throw new Exception();
            }

            // Validate that the establishment has at least one category
            if (establishment.Categories == null || !establishment.Categories.Any())
            {
                throw new Exception();
            }

            // Validate that all categories are valid enum values
            foreach (var category in establishment.Categories)
            {
                if (!Enum.IsDefined(typeof(Category), category))
                {
                    throw new Exception($"Category '{category}' is not a valid category.");
                }
            }

            // Adds establishment to the database
            await _context.Establishments.AddAsync(establishment);
            await _context.SaveChangesAsync();

            return establishment;
        }
    }
}