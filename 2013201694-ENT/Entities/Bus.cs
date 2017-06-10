using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public class Bus
    {
        public int BusId { get; set; }
        public string Placa { get; set; }
        public string SerieMotor { get; set; }

        public virtual int ServicioId { get; set; }
        public virtual Servicio Servicio { get; set; }

        public virtual ICollection<Tripulacion> Tripulacion { get; set; }

        public Bus()
        {
            Tripulacion = new List<Tripulacion>();
        }

    }
}
