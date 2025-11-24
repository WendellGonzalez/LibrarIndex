using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaLibreria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly IExtractorService<LibroDTO> _service;
        private readonly ILogger<LibroController> _logger;

        public LibroController(IExtractorService<LibroDTO> service, ILogger<LibroController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("Get-Libros")]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> GetAutorsAsync()
        {
            try
            {
                var libros = await _service.GetListAsync() ?? new List<LibroDTO>();
                if (libros == null)
                {
                    _logger.LogError("La lista llegó vacía al controlador");
                    return NoContent();
                }

                return Ok(libros);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al obtener libros");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}