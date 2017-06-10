using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public class LugarViaje
    {
        public int LugarViajeId { get; set; }
        public string NombreLugar { get; set; }

        public virtual int ServicioId { get; set; }
        public virtual Servicio Servicio { get; set; }

        public virtual ICollection<TipoLugar> TipoLugar { get; set; }

        public LugarViaje()
        {
            TipoLugar = new List<TipoLugar>();
        }

    }
}
