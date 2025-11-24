using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IExtractorService<T>
    {
        Task<IEnumerable<T>> GetListAsync();
    }
}