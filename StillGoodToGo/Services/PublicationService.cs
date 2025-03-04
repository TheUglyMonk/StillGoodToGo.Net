using Microsoft.EntityFrameworkCore;
using StillGoodToGo.DataContext;
using StillGoodToGo.Dtos;
using StillGoodToGo.Enums;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Mappers;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;

namespace StillGoodToGo.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly StillGoodToGoContext _context;

        public PublicationService(StillGoodToGoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

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

            var establishmentExists = _context.Establishments.Any(e => e.Id == publication.EstablishmentId);
            if (establishmentExists)
            {
                _context.Publications.Add(publication);
                await _context.SaveChangesAsync();
                return publication;
            }
            else
            {
                throw new EstablishmentNotFound();
            }
        }

        //APAGAR
        public Task<PublicationResponseDto> AddPublication(PublicationRequestDto publicationDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Publication>> GetFilteredPublications(
            Category? category, double? latitude, double? longitude, double? maxDistance, string? foodType, double? minDiscount)
        {
            IQueryable<Publication> query = _context.Publications.Include(p => p.Establishment);

            // Filtrar por categoria do estabelecimento
            if (category.HasValue)
            {
                query = query.Where(p => p.Establishment.Categories.Contains(category.Value));
            }

            // Filtrar por localização (latitude, longitude, raio)
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

            // Filtrar pelo tipo de alimento na descrição
            if (!string.IsNullOrEmpty(foodType))
            {
                query = query.Where(p => p.Description.Contains(foodType));
            }

            // Filtrar por nível de desconto
            if (minDiscount.HasValue)
            {
                query = query.Where(p => p.Price < minDiscount.Value);
            }

            return await query.ToListAsync();
        }
    }
}
