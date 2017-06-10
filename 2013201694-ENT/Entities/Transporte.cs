using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public class Transporte : Servicio
    {
        public string Observaciones { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<TipoViaje> TipoViaje { get; set; }


        public Transporte()
        {
            Cliente = new List<Cliente>();
            TipoViaje = new List<TipoViaje>();

        }
    }
}
