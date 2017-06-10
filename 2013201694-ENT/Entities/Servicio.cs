using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public abstract class Servicio
    {
        public int ServicioId { get; set; }
        public string NombreServicio { get; set; }
        public decimal Tarifa { get; set; }

        public virtual int VentaId { get; set; }
        public virtual Venta Venta { get; set; }

        public virtual ICollection<Bus> Bus { get; set; }
        public virtual ICollection<LugarViaje> LugarViaje { get; set; }

        public Servicio()
        {
            Bus = new List<Bus>();
            LugarViaje = new List<LugarViaje>();
        }

    }
}
