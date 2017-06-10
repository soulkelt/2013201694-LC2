using _2013201694_ENT.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_PER.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly TransporteDbContext _Context;

        public IAdministrativoRepository Administrativos { get; private set; }
        public IBusRepository Buses { get; private set; }
        public IClienteRepository Clientes { get; private set; }
        public IEmpleadoRepository Empleados { get; private set; }
        public IEncomiendaRepository Encomiendas { get; private set; }
        public ILugarViajeRepository LugarViajes { get; private set; }
        public IServicioRepository Servicios { get; private set; }
        public ITipoComprobanteRepository TipoComprobantes { get; private set; }
        public ITipoLugarRepository TipoLugares { get; private set; }
        public ITipoPagoRepository TipoPagos { get; private set; }
        public ITipoTripulacionRepository TipoTripulacion { get; private set; }
        public ITipoViajeRepository TipoViajes { get; private set; }
        public ITransporteRepository Transportes { get; private set; }
        public ITripulacionRepository Tripulacion { get; private set; }
        public IVentaRepository Ventas { get; private set; }

        public UnityOfWork()
        {
            _Context = new TransporteDbContext();

            Administrativos = new AdministrativoRepository(_Context);
            Buses = new BusRepository(_Context);
            Clientes = new ClienteRepository(_Context);
            Empleados = new EmpleadoRepository(_Context);
            Encomiendas = new EncomiendaRepository(_Context);
            LugarViajes = new LugarViajeRepository(_Context);
            Servicios = new ServicioRepository(_Context);
            TipoComprobantes = new TipoComprobanteRepository(_Context);
            TipoLugares = new TipoLugarRepository(_Context);
            TipoPagos = new TipoPagoRepository(_Context);
            TipoTripulacion = new TipoTripulacionRepository(_Context);
            TipoViajes = new TipoViajeRepository(_Context);
            Transportes = new TransporteRepository(_Context);
            Tripulacion = new TripulacionRepository(_Context);
            Ventas = new VentaRepository(_Context);

        }

        public void Dispose()
        {
            _Context.Dispose();
        }

        public int SaveChanges()
        {
            return _Context.SaveChanges();
        }

        public void StateModified(object Entity)
        {
            _Context.Entry(Entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
