using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.DTOs
{
    public class AutorDTO
    {
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string pais { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
        public string ciudad { get; set; } = string.Empty;
    }
}