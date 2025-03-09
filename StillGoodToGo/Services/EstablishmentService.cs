using StillGoodToGo.DataContext;
using StillGoodToGo.Enums;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;
using StillGoodToGo.Exceptions;
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

        /// <summary>
        /// Updates an establishment in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedEstablishment"></param>
        /// <returns></returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="ParamIsNull"></exception>
        /// <exception cref="InvalidParam"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        public async Task<Establishment> UpdatesEstablishment(int id, Establishment updatedEstablishment)
        {
            // Check that the database context is initialized.
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            // Check that the establishment is not null.
            if (updatedEstablishment == null)
            {
                throw new ParamIsNull();
            }

            // Check that the establishment has a valid id.
            if (updatedEstablishment.Email.IsNullOrEmpty())
            {
                throw new InvalidParam("Email can not be empty");
            }

            // Check that the establishment has a valid email.
            if (updatedEstablishment.Password.IsNullOrEmpty())
            {
                throw new InvalidParam("Password can not be empty");
            }

            // Check that the establishment has a valid email.
            if (updatedEstablishment.Username.IsNullOrEmpty())
            {
                throw new InvalidParam("Username can not be empty");
            }

            // Check that the establishment has a valid email.
            Establishment establishment = _context.Establishments.FirstOrDefault(e => e.Id == id);

            // Check that the establishment was found.
            if (establishment == null)
            {
                throw new NotFoundInDbSet();
            }

            // Check if the email is unique.
            if (updatedEstablishment.Email != establishment.Email)
            {
                // Check if the email is unique.
                Establishment establishmenexists = _context.Establishments.FirstOrDefault(e => e.Email == updatedEstablishment.Email);
                if (establishmenexists != null)
                {
                    throw new InvalidParam("Email already exists.");
                }
            }

            // Check if the location is unique.
            if (updatedEstablishment.Latitude != establishment.Latitude || updatedEstablishment.Longitude != establishment.Longitude)
            {
                Establishment establishmenexists = _context.Establishments.FirstOrDefault(e => e.Latitude == updatedEstablishment.Latitude && e.Longitude == updatedEstablishment.Longitude);
                if (establishmenexists != null)
                {
                    throw new InvalidParam("Location already exists.");
                }
            }

            // Update the establishment.
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
            // Check that the database context is initialized.
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            // Locate the establishment by its id.
            Establishment establishment = _context.Establishments.FirstOrDefault(e => e.Id == id);
            if (establishment == null)
            {
                throw new NotFoundInDbSet();
            }

            // Check if the establishment is already deactivated.
            if (!establishment.Active)
            {
                throw new EstablishmentAlreadyDesactivated();
            }

            // Deactivate the establishment.
            establishment.Active = false;

            // Save the changes to the database.
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
            // Check that the database context is initialized.
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            // Check that the id is valid.
            if (id == null || id <= 0)
            {
                throw new InvalidParam();
            }

            // Find the establishment by its id.
            var establishment = await _context.Establishments.FindAsync(id);

            // Check if the establishment was found.
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
            // Check that the database context is initialized.
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            // Check that the description is valid.
            if (string.IsNullOrEmpty(description))
            {
                throw new InvalidParam();
            }

            // Find the establishment by its description.
            var establishments = await _context.Establishments
             .Where(e => e.Description == description)
             .ToListAsync();

            // Check if the establishment was found.
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
            // Check that the database context is initialized.
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            // Find all establishments.
            var establishments = await _context.Establishments.ToListAsync();

            // Check if any establishments were found.
            if (establishments == null || !establishments.Any())
            {
                throw new NotFoundInDbSet("No establishments found.");
            }

            return establishments;
        }

        /// <summary>
        /// Gets all active establishments.
        /// </summary>
        /// <returns>The all establishments if found.</returns>
        /// <exception cref="DbSetNotInitialize"></exception>
        /// <exception cref="NotFoundInDbSet"></exception>
        public async Task<List<Establishment>> GetActiveEstablishments()
        {
            // Check that the database context is initialized.
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            var establishments = await _context.Establishments.Where(e => e.Active == true).ToListAsync();

            // Check if any establishments were found.
            if (establishments == null || !establishments.Any())
            {
                throw new NotFoundInDbSet("No establishments found.");
            }

            return establishments;
        }

        public async Task<Establishment> AddsAmountReceived(int id, double amount) 
        {
            if (_context.Establishments == null)
            {
                throw new DbSetNotInitialize();
            }

            Establishment establishment = await GetEstablishmentById(id);

            establishment.TotalAmountReceived = establishment.TotalAmountReceived  + amount;

            await _context.SaveChangesAsync();

            return establishment;
        }
    }
}