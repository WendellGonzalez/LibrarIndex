using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LibroDTO
    {
        public string titulo { get; set; } = string.Empty;
        public string tipo { get; set; } = string.Empty;
        public decimal precio {get;set;}
        public string notas { get; set; } = string.Empty;
        public DateOnly fecha_pub { get; set; }
    }
}