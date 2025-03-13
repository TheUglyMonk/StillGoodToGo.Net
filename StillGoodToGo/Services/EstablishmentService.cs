using StillGoodToGo.DataContext;
using StillGoodToGo.Enums;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;


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
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            if (establishment == null)
            {
                throw new ParamIsNull();
            }

            var emailExists = _context.Establishments.Any(e => e.Email == establishment.Email);
            var locationExists = _context.Establishments.Any(e => e.Latitude == establishment.Latitude && e.Longitude == establishment.Longitude);

            if (emailExists || locationExists)
            {
                throw new EstablishmentNotUnique();
            }

            if (establishment.Categories == null || !establishment.Categories.Any())
            {
                throw new NoCategoryFound();
            }

            foreach (var category in establishment.Categories)
            {
                if (!Enum.IsDefined(typeof(Category), category))
                {
                    throw new InvalidCategoryFound();
                }
            }

            establishment.Classification = 0;

            await _context.Establishments.AddAsync(establishment);
            await _context.SaveChangesAsync();

            return establishment;
        }

        /// <summary>
        /// Updates an existing establishment in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the establishment to update.</param>
        /// <param name="updatedEstablishment">The updated establishment details.</param>
        /// <returns>Returns the updated establishment entity.</returns>
        /// <exception cref="DbSetNotInitialize">Thrown when the database context is not initialized.</exception>
        /// <exception cref="ParamIsNull">Thrown when the provided establishment data is null.</exception>
        /// <exception cref="InvalidParam">Thrown when required fields are empty or invalid.</exception>
        /// <exception cref="NotFoundInDbSet">Thrown when the establishment with the given ID is not found.</exception>
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
            establishment.Classification = updatedEstablishment.Classification;

            await _context.SaveChangesAsync();

            return establishment;
        }

        /// <summary>
        /// Updates the classification of an establishment in the database.
        /// </summary>
        /// <param name="id">The unique identifier of the establishment.</param>
        /// <param name="classification">The new classification value (must be between 0 and 5).</param>
        /// <returns>The updated <see cref="Establishment"/> object.</returns>
        /// <exception cref="DbSetNotInitialize">Thrown when the database context is not initialized.</exception>
        /// <exception cref="NotFoundInDbSet">Thrown when the establishment with the given ID is not found.</exception>
        /// <exception cref="InvalidParam">Thrown when the classification value is out of the valid range (0-5).</exception>
        public async Task<Establishment> UpdateClassification (int id, double classification)
        {
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            Establishment establishment = _context.Establishments.FirstOrDefault(e => e.Id == id);

            if (establishment == null)
            {
                throw new NotFoundInDbSet();
            }

            if (classification < 0 || classification > 5)
            {
                throw new InvalidParam("Invalid classification");
            }

            establishment.Classification = classification;

            await _context.SaveChangesAsync();

            return establishment;
        }

        /// <summary>
        /// Deactivates an establishment in the database turning is active value to false.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        /// <exception cref="EstablishmentAlreadyDesactivated"></exception>
        public async Task<Establishment> DeactivateEstablishment(int id)
        {
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            Establishment establishment = _context.Establishments.FirstOrDefault(e => e.Id == id);
            if (establishment == null)
            {
                throw new NotFoundInDbSet();
            }

            if (!establishment.Active)
            {
                throw new EstablishmentAlreadyDesactivated();
            }

            establishment.Active = false;

            await _context.SaveChangesAsync();

            return establishment;
        }

        /// <summary>
        /// Gets an establishment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <returns>The establishment if found.</returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        public async Task<Establishment> GetEstablishmentById(int id)
        {
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            if (id <= 0)
            {
                throw new InvalidParam();
            }

            var establishment = await _context.Establishments.FindAsync(id);
            if (establishment == null)
            {
                throw new NotFoundInDbSet();
            }

            return establishment;
        }

        /// <summary>
        /// Gets an establishment by its description.
        /// </summary>
        /// <param name="description">The description to search for.</param>
        /// <returns>The establishment if found.</returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="InvalidParam"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        public async Task<List<Establishment>> GetEstablishmentsByDescription(string description)
        {
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidParam();
            }

            var establishments = await _context.Establishments
             .Where(e => e.Description == description)
             .ToListAsync();

            if (establishments == null || !establishments.Any())
            {
                throw new NotFoundInDbSet();
            }

            return establishments;

        }

        /// <summary>
        /// Gets all establishments.
        /// </summary>
        /// <returns>The all establishments if found.</returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        public async Task<List<Establishment>> GetEstablishments()
        {
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            var establishments = await _context.Establishments.ToListAsync();

            if (establishments == null || !establishments.Any())
            {
                throw new NotFoundInDbSet("No establishments found.");
            }

            return establishments;
        }

        /// <summary>
        /// Gets all active establishments.
        /// </summary>
        /// <returns>The all active establishments if found.</returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        public async Task<List<Establishment>> GetActiveEstablishments()
        {
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            var establishments = await _context.Establishments.Where(e => e.Active == true).ToListAsync();

            if (establishments == null || !establishments.Any())
            {
                throw new NotFoundInDbSet("No establishments found.");
            }

            return establishments;
        }

        /// <summary>
        /// Adds the specified amount to the total amount received for the establishment with the given id.
        /// </summary>
        /// <param name="id">The id of the establishment.</param>
        /// <param name="amount">The amount to add.</param>
        /// <returns>The updated establishment.</returns>
        /// <exception cref="DbSetNotInitialize">Thrown when the establishment DbSet is not initialized.</exception>
        public async Task<Establishment> AddsAmountReceived(int id, double amount)
        {
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            Establishment establishment = await GetEstablishmentById(id);

            establishment.TotalAmountReceived = establishment.TotalAmountReceived + amount;

            await _context.SaveChangesAsync();

            return establishment;
        }

        /// <summary>
        /// Retrieves an establishment based on its email address.
        /// </summary>
        /// <param name="email">The email address of the establishment.</param>
        /// <returns>The establishment if found, or null if no establishment matches the given email.</returns>
        /// <exception cref="DbSetNotInitialize">Thrown when the establishment DbSet is not initialized.</exception>
        public async Task<Establishment> GetEstablishmentByEmail(string email)
        {
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            var establishment = await _context.Establishments.FirstOrDefaultAsync(e => e.Email == email);

            if (establishment == null)
            {
                throw new EstablishmentNotFound("Establishment not found.");
            }

            return establishment;
        }
    }
}