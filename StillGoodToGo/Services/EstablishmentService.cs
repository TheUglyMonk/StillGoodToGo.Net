using StillGoodToGo.DataContext;
using StillGoodToGo.Enums;
using StillGoodToGo.Exceptions;
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
        /// Initializes a new instance of the <see cref="EstablishmentService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public EstablishmentService(StillGoodToGoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new establishment to the database.
        /// </summary>
        /// <param name="establishment">The establishment to add.</param>
        /// <returns>The added establishment.</returns>
        /// <exception cref="DbSetNotInitialize">Thrown when the database context is not initialized.</exception>
        /// <exception cref="ParamIsNull">Thrown when the establishment parameter is null.</exception>
        /// <exception cref="EstablishmentNotUnique">Thrown when the email or location is not unique.</exception>
        /// <exception cref="NoCategoryFound">Thrown when no categories are found.</exception>
        /// <exception cref="InvalidCategoryFound">Thrown when an invalid category is found.</exception>
        public async Task<Establishment> AddEstablishment(Establishment establishment)
        {
            // Validate that the database context is not null
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            // Validate that the establishment is not null
            if (establishment == null)
            {
                throw new ParamIsNull();
            }

            // Validate that the email and location are unique
            var emailExists = _context.Establishments.Any(e => e.Email == establishment.Email);
            var locationExists = _context.Establishments.Any(e => e.Latitude == establishment.Latitude && e.Longitude == establishment.Longitude);

            if (emailExists || locationExists)
            {
                throw new EstablishmentNotUnique();
            }

            // Validate that the establishment has at least one category
            if (establishment.Categories == null || !establishment.Categories.Any())
            {
                throw new NoCategoryFound();
            }

            // Validate that all categories are valid enum values
            foreach (var category in establishment.Categories)
            {
                if (!Enum.IsDefined(typeof(Category), category))
                {
                    throw new InvalidCategoryFound();
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

            if (updatedEstablishment == null)
            {
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
            establishment.Active = updatedEstablishment.Active;

            await _context.SaveChangesAsync();

            return establishment;
        }
    }
}