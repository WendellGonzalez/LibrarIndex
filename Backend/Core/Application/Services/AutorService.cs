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
    public class AutorService : IExtractorService<AutorDTO>
    {
        private readonly IAutorRepository _repository;
        private readonly ILogger<AutorService> _logger;
        public AutorService(IAutorRepository repository, ILogger<AutorService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<IEnumerable<AutorDTO>> GetListAsync()
        {
            _logger.LogInformation("Iniciando proceso de listar Autores");
            try
            {
                var autores = await _repository.GetAutorsAsync();

                    List<AutorDTO> autoresDTOs = autores.Select(autor => new AutorDTO
                    {
                        nombre = autor.nombre,
                        apellido = autor.apellido,
                        telefono = autor.telefono,
                        pais = autor.pais,
                        estado = autor.estado,
                        ciudad = autor.ciudad
                        
                    }).ToList();
                

                return autoresDTOs;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al listar Autores");
                throw;
            }
        }
    }
}