using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public class TipoLugar
    {
        public int TipoLugarId { get; set; }
        public string NombreTipo { get; set; }

        public virtual int LugarViajeId { get; set; }
        public virtual LugarViaje LugarViaje { get; set; }

        public TipoLugar()
        {

        }
    }
}
