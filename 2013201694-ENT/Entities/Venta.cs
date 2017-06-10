using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public class Venta
    {
        public int VentaId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public virtual ICollection<Administrativo> Administrativo { get; set; }
        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<Servicio> Servicio { get; set; }
        public virtual ICollection<TipoPago> TipoPago { get; set; }
        public virtual ICollection<TipoComprobante> TipoComprobante { get; set; }

        public Venta()
        {
            Administrativo = new List<Administrativo>();
            Cliente = new List<Cliente>();
            Servicio = new List<Servicio>();
            TipoPago = new List<TipoPago>();
            TipoComprobante = new List<TipoComprobante>();
        }

    }
}
