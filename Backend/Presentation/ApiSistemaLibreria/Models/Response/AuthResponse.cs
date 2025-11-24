using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;

namespace ApiSistemaLibreria.Models.Response
{
    public class AuthResponse
    {
        public bool success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}