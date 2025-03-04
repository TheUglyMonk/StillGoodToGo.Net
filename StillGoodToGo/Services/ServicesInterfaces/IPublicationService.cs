﻿using StillGoodToGo.Dtos;
using StillGoodToGo.Enums;
using StillGoodToGo.Models;


namespace StillGoodToGo.Services.ServicesInterfaces
{
    public interface IPublicationService
    {
        Task<PublicationResponseDto> AddPublication(PublicationRequestDto publicationDto);
    

        Task<List<Publication>> GetFilteredPublications(Category? category, double? latitude, double? longitude, double? maxDistance, string? foodType, double? minDiscount);
    }
}