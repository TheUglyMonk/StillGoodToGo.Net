using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Dtos;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Mappers;
using StillGoodToGo.Services.ServicesInterfaces;


[ApiController]
[Route("api/[controller]")]
public class PublicationController : ControllerBase
{
    private readonly IPublicationService _publicationService;
    private readonly PublicationMapper _publicationMapper;
    private readonly ILogger<PublicationController> _logger;

    public PublicationController(IPublicationService publicationService, PublicationMapper publicationMapper, ILogger<PublicationController> logger)
    {
        _publicationService = publicationService;
        _publicationMapper = publicationMapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> addPublication([FromBody] PublicationRequestDto publicationRequestDto)
    {
        try
        {
            _logger.LogInformation(@"Received PublicationRequestDto: {@PublicationRequestDto}", publicationRequestDto);
            // Map the request DTO to a publication model
            var publication = _publicationMapper.PublicationRequestToPublication(publicationRequestDto);

            // Add the publication to the database
            var addedPublication = await _publicationService.AddPublication(publication);

            // Map the added publication to a response DTO and return it
            var publicationResponseDto = _publicationMapper.PublicationToPublicationResponse(addedPublication);

            // Return the response DTO
            return Ok(publicationResponseDto);
        }
        catch(DbSetNotInitialize ex)
        {
            _logger.LogError(ex, "DbSetNotInitialize error");
            // Return a 500 error with a message
            return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
        }
        catch (ParamIsNull ex)
        {
            _logger.LogError(ex, "ParamIsNull error");
            // Return a 400 error with a message
            return BadRequest(new { message = "The publication data is invalid." });
        }
        catch (EstablishmentNotFound ex)
        {
            _logger.LogError(ex, "EstablishmentNotFound error");
            // Return a 404 error with a message
            return NotFound(new { message = "The specified establishment does not exist." });
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex, "Internal server error");
            // Return a 500 error with a message
            return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
        }
    }
}
