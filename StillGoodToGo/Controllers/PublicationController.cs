using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Dtos;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Services.ServicesInterfaces;


[ApiController]
[Route("api/publications")]
public class PublicationController : ControllerBase
{
    private readonly IPublicationService _publicationService;

    public PublicationController(IPublicationService publicationService)
    {
        _publicationService = publicationService;
    }

    [HttpPost]
    public async Task<ActionResult<PublicationResponseDto>> CreatePublication([FromBody] PublicationRequestDto publicationDto)
    {
        try
        {
            var createdPublication = await _publicationService.AddPublication(publicationDto);
            return CreatedAtAction(nameof(CreatePublication), new { id = createdPublication.Id }, createdPublication);
        }
        catch (EstablishmentNotFound)
        {
            return NotFound(new { message = "The specified establishment does not exist." });
        }
        catch (ParamIsNull)
        {
            return BadRequest(new { message = "The publication data is invalid." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
        }
    }
}
