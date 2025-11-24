using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infraestructure.Persistence
{
    public class LibroRepository : ILibroRepository
    {
        private readonly ContactDbContext _context;
        private readonly ILogger<LibroRepository> _logger;
        public LibroRepository(ContactDbContext context, ILogger<LibroRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Libro>> GetLibrosAsync()
        {
            _logger.LogInformation("Iniciando proceso de extraccion de libros de la DB");
            try
            {
                return await _context.ViewLibros.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al listar libros de la DB");
                throw;
            }
        }
    }
}