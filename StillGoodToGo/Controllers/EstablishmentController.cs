
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StillGoodToGo.Dtos;
using StillGoodToGo.Exceptions;
using StillGoodToGo.Mappers;
using StillGoodToGo.Models;
using StillGoodToGo.Services.ServicesInterfaces;

namespace StillGoodToGo.Controllers
{
    /// <summary>
    /// controller for the establishment entity.
    /// </summary>
    
    [ApiController]
    [Route("api/[controller]")]
    public class EstablishmentController : ControllerBase
    {

        private readonly IEstablishmentService _establishmentService;
        private readonly EstablishmentMapper _establishmentMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EstablishmentController"/> class.
        /// </summary>
        /// <param name="establishmentService"></param>
        public EstablishmentController(IEstablishmentService establishmentService, EstablishmentMapper establishmentMapper)
        {
            _establishmentService = establishmentService;
            _establishmentMapper = establishmentMapper;
        }

        /// <summary>
        /// Adds a new establishment to the database.
        /// </summary>
        /// <param name="establishmentRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEstablishment(EstablishmentRequestDto establishmentRequestDto)
        {
            try
            {
                // Map the request DTO to an establishment model
                var establishment = _establishmentMapper.EstablishmentRequestToEstablishment(establishmentRequestDto);

                // Add the establishment to the database
                var addedEstablishment = await _establishmentService.AddEstablishment(establishment);

                // Map the added establishment to a response DTO and return it
                var establishmentResponseDto = _establishmentMapper.EstablishmentToEstablishmentResponse(addedEstablishment);

                return Ok(establishmentResponseDto);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstablishment(int id, [FromBody] EstablishmentRequestDto establishmentDto)
        {
            try
            {
                
                Establishment establishment = _establishmentMapper.EstablishmentRequestToEstablishment(establishmentDto);

                establishment = await _establishmentService.UpdatesEstablishment(id, establishment);

                return Ok(_establishmentMapper.EstablishmentToEstablishmentResponse(establishment));
            }
            catch (DbSetNotInitialize e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidParam e)
            {
                return BadRequest(e.Message);
            }
            catch (ParamIsNull e)
            {
                return BadRequest(e.Message);
            }
            catch (NotFoundInDbSet e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

