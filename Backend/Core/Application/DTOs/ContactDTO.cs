using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ContactDTO
    {
        public string nombre { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string asunto { get; set; } = string.Empty;
        public string comentario { get; set; } = string.Empty;
    }
}