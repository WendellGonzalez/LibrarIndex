using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using Infraestructure.Context;
using Microsoft.Extensions.Logging;

namespace Infraestructure.Persistence
{
    public class ContactRepository : IContactRepository
    {
        private readonly ILogger<ContactRepository> _logger;
        private readonly ContactDbContext _context;

        public ContactRepository( ILogger<ContactRepository> logger, ContactDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task SaveContact(Contact contact)
        {
            _logger.LogInformation("Iniciando proceso de Registro de informacion de contacto: {Correo}", contact.correo);
            try
            {
                await _context.contacto.AddAsync(contact);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al Guardar informacion de contacto: {Correo}", contact.correo);
                throw;
            }
        }
    }
}