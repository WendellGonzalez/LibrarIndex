using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contact
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public DateTime fecha { get; set; } = DateTime.UtcNow;
        public string asunto { get; set; } = string.Empty;
        public string comentario { get; set; } = string.Empty;
    }
}