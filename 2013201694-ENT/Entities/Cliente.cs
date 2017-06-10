using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string DNI { get; set; }

        public virtual int VentaId { get; set; }
        public virtual Venta Venta { get; set; }

        public virtual int ServicioId { get; set; }
        public virtual Servicio Servicio { get; set; }

        public Cliente()
        {

        }
    }
}
