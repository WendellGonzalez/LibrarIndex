using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface ILibroRepository
    {
        Task<IEnumerable<Libro>> GetLibrosAsync();
    }
}