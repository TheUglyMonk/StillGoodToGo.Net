using StillGoodToGo.DataContext;
using StillGoodToGo.Enums;
using StillGoodToGo.Models;

namespace StillGoodToGo.Services
{
    public class EstablishmentService
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly StillGoodToGoContext _context;

        public EstablishmentService(StillGoodToGoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create Establishment
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

            ///<summary>
            /// Checks if the Email or location already exists
            /// </summary>
            var emailExists = _context.Establishments.Any(e => e.Email == establishment.Email);
            var locationExists = _context.Establishments.Any(e => e.Latitude == establishment.Latitude && e.Longitude == establishment.Longitude);
            if (emailExists || locationExists)
            {
                throw new Exception();
            }

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

            await _context.Establishments.AddAsync(establishment);
            await _context.SaveChangesAsync();

            return establishment;
        }
    }
}