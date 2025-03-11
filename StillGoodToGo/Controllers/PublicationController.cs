﻿using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Dtos;
using StillGoodToGo.Enums;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Mappers;
using StillGoodToGo.Models;
using StillGoodToGo.Services;
using StillGoodToGo.Services.ServicesInterfaces;


namespace StillGoodToGo.Controllers
{
    [ApiController]
    [Route("api/publications")]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;
        private readonly PublicationMapper _publicationMapper;

        public PublicationController(IPublicationService publicationService, PublicationMapper publicationMapper)
        {
            _publicationService = publicationService ?? throw new ArgumentNullException(nameof(publicationService));
            _publicationMapper = publicationMapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePublication([FromBody] PublicationRequestDto publicationRequestDto)
        {
            try
            {
                var publication = _publicationMapper.PublicationRequestToPublication(publicationRequestDto);

                var createdPublication = await _publicationService.AddPublication(publication);

                var publicationResponseDto = _publicationMapper.PublicationToPublicationResponse(publication);
                return Ok(publicationResponseDto);
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

        /// <summary>
        /// Get all publications.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPublications()
        {
            try
            {
                // Get all publications
                var publications = await _publicationService.GetAllPublications();

                if (publications == null)
                {
                    return NotFound();
                }

                // Map publications to response dtos
                var responseDtos = publications.Select(e => _publicationMapper.PublicationToPublicationResponse(e)).ToList();

                return Ok(responseDtos);
            }
            catch (DbSetNotInitialize ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { details = ex.Message });
            }

        }

        /// <summary>
        /// Get publication by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicationById(int id)
        {
            try
            {
                // Get publication by id
                var publication = await _publicationService.GetPublicationById(id);

                // Check if publication was found
                if (publication == null)
                {
                    return NotFound();
                }

                // Map publication to response dto
                var responseDto = _publicationMapper.PublicationToPublicationResponse(publication);

                return Ok(responseDto);
            }
            catch (ParamIsNull ex)
            {
                return BadRequest();
            }
            catch (DbSetNotInitialize ex)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }


        /// <summary>
        /// Update publication.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publicationDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublication(int id, [FromBody] PublicationRequestDto publicationDto)
        {
            try
            {
                // Map publication request dto to publication
                Publication publication = _publicationMapper.PublicationRequestToPublication(publicationDto);

                // Update publication
                publication = await _publicationService.UpdatesPublication(id, publication);

                // Map publication to response dto
                return Ok(_publicationMapper.PublicationToPublicationResponse(publication));
            }
            catch (ParamIsNull ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundInDbSet ex)
            {
                return NotFound(ex.Message);
            }
            catch (DbSetNotInitialize ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }

        /// <summary>
        /// Update publication' status.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publicationDto"></param>
        /// <returns></returns>
        [HttpPut("updatestatus/{id}")]
        public async Task<IActionResult> UpdatesPublicationStatus(int id, [FromBody] PublicationStatus status)
        {
            try
            {
                // Get publication by id
                Publication publication = await _publicationService.GetPublicationById(id); ;

                // Update publication
                publication = await _publicationService.UpdatesPublicationStatus(id, status);

                // Map publication to response dto
                return Ok(_publicationMapper.PublicationToPublicationResponse(publication));
            }
            catch(ParamIsNull ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundInDbSet ex)
            {
                return NotFound(ex.Message);
            }
            catch (DbSetNotInitialize ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get publications from establishment.
        /// </summary>
        /// <param name="establishmentId"></param>
        /// <returns></returns>
        [HttpGet("establishment/{establishmentId}")]
        public async Task<IActionResult> GetPublicationsFromEstablishment(int establishmentId)
        {
            try
            {
                // Get all publications from establishment
                var publications = await _publicationService.GetPublicationsFromEstablishment(establishmentId);
                // Check if publications were found
                if (publications == null)
                {
                    return NotFound();
                }
                // Map publications to response dtos
                var responseDtos = publications.Select(e => _publicationMapper.PublicationToPublicationResponse(e)).ToList();
                return Ok(responseDtos);
            }
            catch (ParamIsNull ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbSetNotInitialize ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get publications with status. from a establishment
        /// </summary>
        /// <param name="establishmentId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("establishment/{establishmentId}/status/{status}")]
        public async Task<IActionResult> GetPublicationsWithStatus(int establishmentId, PublicationStatus status)
        {
            try
            {
                // Fetch publications with the specified status
                var publications = await _publicationService.GetPublicationsWithStatus(establishmentId, status);

                // Check if any publications are found
                if (publications == null || !publications.Any())
                {
                    return NotFound(new { message = "No publications found for the given establishment and status." });
                }

                // Map publications to response DTOs
                var responseDtos = publications.Select(e => _publicationMapper.PublicationToPublicationResponse(e)).ToList();
                return Ok(responseDtos);
            }
            catch (InvalidEnumValue ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoPublicationsFound)
            {
                return NotFound(new { message = "No publications found" });
            }
            catch (ParamIsNull ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbSetNotInitialize ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get publications by status.
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("search/status/{status}")]
        public async Task<IActionResult> GetPublicationsByStatus(PublicationStatus status)
        {
            try
            {
                // Fetch publications with the specified status
                var publications = await _publicationService.GetPublicationsByStatus(status);

                // Check if any publications are found
                if (publications == null || !publications.Any())
                {
                    return NotFound(new { message = "No publications found with the specified status." });
                }

                // Map publications to response DTOs
                var responseDtos = publications.Select(e => _publicationMapper.PublicationToPublicationResponse(e)).ToList();
                return Ok(responseDtos);
            }
            catch (InvalidEnumValue ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NoPublicationsFound)
            {
                return NotFound(new { message = "No publications found with the specified status." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get publications by price range.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [HttpGet("search/price/{min}/{max}")]
        public async Task<IActionResult> GetPublicationsByPriceRange(double min, double max)
        {
            try
            {
                // Validate the price range, if necessary
                if (min < 0 || max < 0 || min > max)
                {
                    return BadRequest(new { message = "Invalid price range." });
                }

                // Fetch publications within the price range
                var publications = await _publicationService.GetPublicationsByPriceRange(min, max);

                // Check if any publications are found
                if (publications == null || !publications.Any())
                {
                    return NotFound();
                }

                // Map publications to response DTOs
                var responseDtos = publications.Select(e => _publicationMapper.PublicationToPublicationResponse(e)).ToList();

                return Ok(responseDtos);
            }
            catch (ParamIsNull ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NoPublicationsFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get publications by price range.
        /// </summary>
        /// <returns></returns>
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailablePublications()
        {
            try
            {
                var publications = await _publicationService.GetAvailablePublications();

                // Check if any publications are found
                if (publications == null || !publications.Any())
                {
                    return NotFound();
                }

                // Map publications to response DTOs
                var responseDtos = publications.Select(e => _publicationMapper.PublicationToPublicationResponse(e)).ToList();

                return Ok(responseDtos);
            }
            catch (DbSetNotInitialize ex)
            {
                return NotFound(ex.Message);
            }
            catch (NoPublicationsFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}