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
    public class AutorRepository : IAutorRepository
    {
        private readonly ContactDbContext _context;
        private readonly ILogger<AutorRepository> _logger;
        public AutorRepository(ContactDbContext context, ILogger<AutorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Autor>> GetAutorsAsync()
        {
            _logger.LogInformation("Iniciando proceso de extraccion de autores de la DB");
            try
            {
                var autores =  await _context.ViewAutores.ToListAsync();
                if(autores == null || autores.Count < 0)
                {
                    _logger.LogError("La lista de autores está viniendo vacia de la DB");
                    return null!;
                }

                return autores;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al listar Autores de la DB");
                throw;
            }
        }
    }
}