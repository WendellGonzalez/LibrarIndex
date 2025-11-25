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
            _logger.LogInformation($"Iniciando proceso de POST: Contacto {model.Nombre}");
            try
            {
                if (string.IsNullOrWhiteSpace(model.Nombre) || string.IsNullOrWhiteSpace(model.Correo) || string.IsNullOrWhiteSpace(model.Asunto) || string.IsNullOrWhiteSpace(model.Comentario))
                {
                    return BadRequest(new AuthResponse
                    {
                        success = false,
                        Message = "El usuario enviado está null"
                    });
                }

                var proceso = _service.ProcessContactInfo(model);

                if (proceso == null)
                {
                    return BadRequest(new AuthResponse
                    {
                        success = false,
                        Message = "Error en el procesamiento de la informacion"
                    });
                }
                else
                {
                    await proceso;
                }

                return new AuthResponse
                {
                    success = true,
                    Message = "Registro exitoso"
                };

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al realizar Post: {Correo}", model.Correo);
                return StatusCode(500, new AuthResponse
                {
                    success = false,
                    Message = "Error interno del servidor"
                });
            }
        }
    }
}