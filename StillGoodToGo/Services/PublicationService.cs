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

        public async Task<PublicationResponseDto> AddPublication(PublicationRequestDto publicationDto)
        {
            if (_context.Publications == null)
            {
                throw new DbSetNotInitialize();
            }

            if (publicationDto == null)
            {
                throw new ParamIsNull();
            }

            var establishment = await _context.Establishments.FindAsync(publicationDto.EstablishmentId);

            if (establishment == null)
            {
                throw new EstablishmentNotFound();
            }

            var publication = new Publication
            {
                EstablishmentId = publicationDto.EstablishmentId,
                Description = publicationDto.Description,
                Price = publicationDto.Price,
                PostDate = publicationDto.PostDate,
                EndDate = publicationDto.EndDate,
                Status = publicationDto.Status
            };

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
                Status = publication.Status
            };
        }

        public async Task<List<Publication>> GetFilteredPublications(Category? category, double? latitude, double? longitude, double? maxDistance, string? foodType, double? minDiscount)
        {
            IQueryable<Publication> query = _context.Publications.Include(p => p.Establishment);

            // LOG PARA VER SE EXISTEM PUBLICAÇÕES
            Console.WriteLine($"Total de publicações no BD: {_context.Publications.Count()}");

            // Filtrar por categoria do estabelecimento
            if (category.HasValue)
            {
                query = query.Where(p => p.Establishment.Categories.Contains(category.Value));
                Console.WriteLine($"Filtrando por categoria: {category.Value}");
            }

            // Filtrar apenas pela longitude se fornecida
            if (longitude.HasValue)
            {
                query = query.Where(p => p.Establishment.Longitude == longitude.Value);
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
                Console.WriteLine($"Filtrando por localização: {latitude.Value}, {longitude.Value}, {maxDistance.Value}");
            }

            // Filtrar pelo tipo de alimento na descrição
            if (!string.IsNullOrEmpty(foodType))
            {
                query = query.Where(p => p.Description.Contains(foodType));
                Console.WriteLine($"Filtrando por tipo de alimento: {foodType}");
            }

            // Filtrar por nível de desconto
            if (minDiscount.HasValue)
            {
                query = query.Where(p => p.Price < minDiscount.Value);
                Console.WriteLine($"Filtrando por desconto mínimo: {minDiscount.Value}");
            }

            var results = await query.ToListAsync();

            Console.WriteLine($"Total de resultados encontrados: {results.Count}");

            return results;
        }
    }
}
