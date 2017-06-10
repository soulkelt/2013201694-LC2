using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_ENT.IRepositories
{
    public interface IUnityOfWork : IDisposable
    {
        IAdministrativoRepository Administrativos { get; }
        IBusRepository Buses { get; }
        IClienteRepository Clientes { get; }
        IEmpleadoRepository Empleados { get; }
        IEncomiendaRepository Encomiendas { get; }
        ILugarViajeRepository LugarViajes { get; }
        IServicioRepository Servicios { get; }
        ITipoComprobanteRepository TipoComprobantes { get; }
        ITipoLugarRepository TipoLugares { get; }
        ITipoPagoRepository TipoPagos { get; }
        ITipoTripulacionRepository TipoTripulacion { get; }
        ITipoViajeRepository TipoViajes { get; }
        ITransporteRepository Transportes { get; }
        ITripulacionRepository Tripulacion { get; }
        IVentaRepository Ventas { get; }

        int SaveChanges();

        void StateModified(object entity);
    }
}
