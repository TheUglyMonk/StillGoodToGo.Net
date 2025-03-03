using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Enums;
using StillGoodToGo.Services.ServicesInterfaces;

namespace StillGoodToGo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService ?? throw new ArgumentNullException(nameof(publicationService));
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetPublications(
            [FromQuery] Category? category,
            [FromQuery] double? latitude,
            [FromQuery] double? longitude,
            [FromQuery] double? maxDistance,
            [FromQuery] string? foodType,
            [FromQuery] double? minDiscount)
        {
            try
            {
                var publications = await _publicationService.GetFilteredPublications(
                    category, latitude, longitude, maxDistance, foodType, minDiscount);
                return Ok(publications);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
