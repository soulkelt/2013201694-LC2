using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public class Tripulacion : Empleado
    {
        public string NombreTripulacion { get; set; }

        public virtual int BusId { get; set; }
        public virtual Bus Bus { get; set; }

        public virtual ICollection<TipoTripulacion> TipoTripulacion { get; set; }

        public Tripulacion()
        {
            TipoTripulacion = new List<TipoTripulacion>();
        }
    }
}
