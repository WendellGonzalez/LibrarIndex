using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSistemaLibreria.Models.Request;
using ApiSistemaLibreria.Models.Response;
using Application.DTOs;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiSistemaLibreria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _service;

        public ContactController(IContactService service, ILogger<ContactController> logger)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("Post-Contact")]
        public async Task<ActionResult<AuthResponse>> PostContact(ContactDTO model)
        {
            _logger.LogInformation($"Iniciando proceso de POST: Contacto {model.nombre}");
            try
            {
                if (string.IsNullOrWhiteSpace(model.nombre) || string.IsNullOrWhiteSpace(model.correo)
                    || string.IsNullOrWhiteSpace(model.asunto) || string.IsNullOrWhiteSpace(model.comentario))
                {
                    return BadRequest(new AuthResponse
                    {
                        success = false,
                        Message = "Todos los campos son obligatorios"
                    });
                }

                await _service.ProcessContactInfo(model);

                return new AuthResponse
                {
                    success = true,
                    Message = "Registro exitoso"
                };

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al realizar Post: {Correo}", model.correo);
                return StatusCode(500, new AuthResponse
                {
                    success = false,
                    Message = "Error interno del servidor"
                });
            }
        }

    }
}