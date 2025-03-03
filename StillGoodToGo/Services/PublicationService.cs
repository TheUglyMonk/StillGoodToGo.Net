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
        private readonly PublicationMapper _publicationMapper;

        public PublicationService(StillGoodToGoContext context, PublicationMapper publicationMapper)
        {
            _context = context;
            _publicationMapper = publicationMapper;
        }

        public async Task<PublicationResponseDto> AddPublication(PublicationRequestDto publicationDto)
        {
            if (publicationDto == null)
                throw new ParamIsNull();

            // Check if the establishment exists
            var establishmentExists = await _context.Establishments.AnyAsync(e => e.Id == publicationDto.EstablishmentId);
            if (!establishmentExists)
                throw new EstablishmentNotFound();

            // Convert DTO to entity
            var publication = _publicationMapper.PublicationRequestToPublication(publicationDto);

            _context.Publications.Add(publication);
            await _context.SaveChangesAsync();

            // Return a DTO for security
            return _publicationMapper.PublicationToPublicationResponse(publication);
        }

        //APAGAR
        public Task<PublicationResponseDto> AddPublication(PublicationRequestDto publicationDto)
        {
            throw new NotImplementedException();
        }
    }
}
