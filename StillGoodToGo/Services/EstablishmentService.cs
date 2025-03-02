using StillGoodToGo.DataContext;
using StillGoodToGo.Enums;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;
using StillGoodToGo.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace StillGoodToGo.Services
{
    /// <summary>
    /// Service class for managing establishments.
    /// </summary>
    public class EstablishmentService : IEstablishmentService
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

        public async Task<Establishment> UpdatesEstablishment(int id, Establishment updatedEstablishment)
        {

            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            if (updatedEstablishment == null) {
                throw new ParamIsNull();
            }

            if (updatedEstablishment.Email.IsNullOrEmpty())
            {
                throw new InvalidParam("Email can not be empty");
            }

            if (updatedEstablishment.Password.IsNullOrEmpty())
            {
                throw new InvalidParam("Password can not be empty");
            }

            if (updatedEstablishment.Username.IsNullOrEmpty())
            {
                throw new InvalidParam("Username can not be empty");
            }

            Establishment establishment = _context.Establishments.FirstOrDefault(e => e.Id == id);

            if (establishment == null)
            {
                throw new NotFoundInDbSet();
            }

            if (updatedEstablishment.Email != establishment.Email)
            {
                Establishment establishmenexists = _context.Establishments.FirstOrDefault(e => e.Email == updatedEstablishment.Email);
                if (establishmenexists != null)
                {
                    throw new InvalidParam("Email already exists.");
                }
            }

            if (updatedEstablishment.Latitude != establishment.Latitude || updatedEstablishment.Longitude != establishment.Longitude)
            {
                Establishment establishmenexists = _context.Establishments.FirstOrDefault(e => e.Latitude == updatedEstablishment.Latitude && e.Longitude == updatedEstablishment.Longitude);
                if (establishmenexists != null)
                {
                    throw new InvalidParam("Location already exists.");
                }
            }

            establishment.Username = updatedEstablishment.Username;
            establishment.Email = updatedEstablishment.Email;
            establishment.Password = updatedEstablishment.Password;
            establishment.Latitude = updatedEstablishment.Latitude;
            establishment.Longitude = updatedEstablishment.Longitude;
            establishment.Description = updatedEstablishment.Description;

            await _context.SaveChangesAsync();

            return establishment;
        }
    }
}