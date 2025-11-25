using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ContactService : IContactService
    {
        private readonly ILogger<ContactService> _logger;
        private readonly IContactRepository _repo;

        public ContactService(ILogger<ContactService> logger, IContactRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }
        public async Task ProcessContactInfo(ContactDTO contact)
        {
            _logger.LogInformation("Iniciando limpieza y validacion de informacion de contacto: {Correo}", contact.correo);
            try
            {
                if (contact.comentario.Length > 256)
                {
                    _logger.LogError($"Cantidad de letras excedida. Maximo Permitido: 256 Caracteres. Cantidad de letras Ingresada: {contact.comentario.Length}");
                    return;
                }

                var Contact = new Contact
                {
                    nombre = contact.nombre,
                    correo = contact.correo,
                    fecha = DateTime.UtcNow,
                    asunto = contact.asunto,
                    comentario = contact.comentario
                };

                if (Contact == null)
                {
                    _logger.LogError("El contacto está siendo null: {Correo}", contact.correo);
                    return;
                }

                await _repo.SaveContact(Contact);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al procesar informacion de contaacto: {Correo}", contact.correo);
                throw;
            }
        }
    }
}