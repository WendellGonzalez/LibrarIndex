using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Autor
    {
        [Key]
        public string id_autor { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string pais { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
        public string ciudad { get; set; } = string.Empty;
    }
}