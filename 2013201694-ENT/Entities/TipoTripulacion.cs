 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public class TipoTripulacion
    {
        public int TipoTripulacionId { get; set; }
        public string Descripcion { get; set; }

        public virtual int TripulacionId { get; set; }
        public virtual Tripulacion Tripulacion { get; set; }

        public TipoTripulacion()
        {

        }
    }
}
