using Microsoft.EntityFrameworkCore;
using StillGoodToGo.DataContext;
using StillGoodToGo.Dtos;
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
            _context = context;
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
    }
}
