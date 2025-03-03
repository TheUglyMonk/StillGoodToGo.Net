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
            _context = context;
            
        }

        public async Task<Publication> AddPublication(Publication publication)
        {
            if(_context.Publications == null) { throw new DbSetNotInitialize(); }

            if (publication == null) { throw new ParamIsNull(); }

            if(publication.Establishment == null) { throw new ParamIsNull(); }

            if (publication.Establishment.Id == 0) { throw new ParamIsNull(); }

            if(publication.Status == null) { throw new NoStatusFound(); }

            foreach (var status in publication.Status) 
            { 
                if(!Enum.IsDefined(typeof(PublicationStatus), status)) { throw new InvalidStatusFound(); }
            }

            await _context.Publications.AddAsync(publication);
            await _context.SaveChangesAsync();

            return publication;
        }
    }
}
