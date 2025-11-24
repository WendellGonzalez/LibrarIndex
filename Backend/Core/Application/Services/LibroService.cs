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
    public class LibroService : IExtractorService<LibroDTO>
    {
        private readonly ILogger<LibroService> _logger;
        private readonly ILibroRepository _repository;

        public LibroService(ILogger<LibroService> logger, ILibroRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<IEnumerable<LibroDTO>> GetListAsync()
        {
            _logger.LogInformation("Iniciando proceso de listar Libros");
            try
            {
                var libros =  await _repository.GetLibrosAsync();

                List<LibroDTO> librosDto = libros.Select(l => new LibroDTO
                {
                    titulo = l.titulo,
                    tipo = l.tipo,
                    precio = l.precio,
                    notas = l.notas,
                    fecha_pub = l.fecha_pub

                }).ToList();

                if(librosDto == null || librosDto.Count < 0)
                {
                    _logger.LogError("La lista está vacía");
                    return null!;
                }

                return librosDto;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error al listar Libros");
                throw;
            }
        }
    }
}