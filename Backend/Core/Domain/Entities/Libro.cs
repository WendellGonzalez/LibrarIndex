using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Libro
    {
        [Key]
        public string id_titulo { get; set; } = string.Empty;
        public string titulo { get; set; } = string.Empty;
        public string tipo { get; set; } = string.Empty;
        public double precio { get; set; }
        public string notas { get; set; } = string.Empty;
        public DateOnly fecha_pub { get; set; }
    }
}