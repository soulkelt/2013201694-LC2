using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2013201694_API.DTO
{
    public abstract class EmpleadoDTO
    {
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string DNI { get; set; }
        public int Edad { get; set; }
        public decimal Sueldo { get; set; }

    }
}