using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSistemaLibreria.Models.Response;
using Application.DTOs;
using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaLibreria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IExtractorService<AutorDTO> _service;
        private readonly ILogger<AutorController> _logger;

        public AutorController(IExtractorService<AutorDTO> service, ILogger<AutorController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("Get-Autores")]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> GetAutorsAsync()
        {
            try
            {
                var autores = await _service.GetListAsync() ?? throw new Exception("Lista vacia");
                if (autores == null)
                {
                    _logger.LogError("La lista llegó vacía al controlador");
                    return NoContent();
                }

                return Ok(autores);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al obtener Autores");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}