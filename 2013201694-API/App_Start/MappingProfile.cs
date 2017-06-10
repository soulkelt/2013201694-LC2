using _2013201694_API.DTO;
using _2013201694_ENT;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2013201694_API.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Administrativo, AdministrativoDTO>();
            CreateMap<AdministrativoDTO, Administrativo>();

            CreateMap<Bus, BusDTO>();
            CreateMap<BusDTO, Bus>();

            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteDTO, Cliente>();

            CreateMap<Empleado, EmpleadoDTO>();
            CreateMap<EmpleadoDTO, Empleado>();

            CreateMap<Encomienda, EncomiendaDTO>();
            CreateMap<EncomiendaDTO, Encomienda>();

            CreateMap<LugarViaje, LugarViajeDTO>();
            CreateMap<LugarViajeDTO, LugarViaje>();

            CreateMap<Servicio, ServicioDTO>();
            CreateMap<ServicioDTO, Servicio>();

            CreateMap<TipoComprobante, TipoComprobanteDTO>();
            CreateMap<TipoComprobanteDTO, TipoComprobante>();

            CreateMap<TipoLugar, TipoLugarDTO>();
            CreateMap<TipoLugarDTO, TipoLugar>();

            CreateMap<TipoPago, TipoPagoDTO>();
            CreateMap<TipoPagoDTO, TipoPago>();

            CreateMap<TipoTripulacion, TipoTripulacionDTO>();
            CreateMap<TipoTripulacionDTO, TipoTripulacion>();

            CreateMap<TipoViaje, TipoViajeDTO>();
            CreateMap<TipoViajeDTO, TipoViaje>();

            CreateMap<Transporte, TransporteDTO>();
            CreateMap<TransporteDTO, Transporte>();

            CreateMap<Tripulacion, TripulacionDTO>();
            CreateMap<TripulacionDTO, Tripulacion>();

            CreateMap<Venta, VentaDTO>();
            CreateMap<VentaDTO, Venta>();
        }
    }
}