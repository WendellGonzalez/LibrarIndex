using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSistemaLibreria.Models.Response
{
    public class ListResponse<T>
    {
        public bool success { get; set; }
        public string Message { get; set; } = string.Empty;

        public static List<T> GetList()
        {
            return new List<T>();
        }
    }
}