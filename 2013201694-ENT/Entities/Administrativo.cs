using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT
{
    public class Administrativo : Empleado
    {
        public string Cargo { get; set; }

        public virtual int VentaId { get; set; }
        public virtual Venta Venta { get; set; }


        public Administrativo()
        {

        }
    }
}
