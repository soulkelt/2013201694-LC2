using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using _2013201694_ENT;
using _2013201694_PER;
using _2013201694_ENT.IRepositories;
using _2013201694_API.DTO;
using AutoMapper;

namespace _2013201694_API.Controllers
{
    public class VentasController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public VentasController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var Ventas = _UnityOfWork.Ventas.GetAll();

            if (Ventas == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var VentasDTO = new List<VentaDTO>();

            foreach (var venta in Ventas)
                VentasDTO.Add(Mapper.Map<Venta, VentaDTO>(venta));

            return Ok(VentasDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var Venta = _UnityOfWork.Ventas.Get(id);

            if (Venta == null)
                return NotFound();

            return Ok(Mapper.Map<Venta, VentaDTO>(Venta));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, VentaDTO VentaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var ventaInPersistence = _UnityOfWork.Ventas.Get(id);
            if (ventaInPersistence == null)
                return NotFound();

            Mapper.Map<VentaDTO, Venta>(VentaDTO, ventaInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(VentaDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(VentaDTO ventaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var venta = Mapper.Map<VentaDTO, Venta>(ventaDTO);

            _UnityOfWork.Ventas.Add(venta);
            _UnityOfWork.SaveChanges();

            ventaDTO.VentaId = venta.VentaId;

            return Created(new Uri(Request.RequestUri + "/" + venta.VentaId), ventaDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var ventaInDataBase = _UnityOfWork.Ventas.Get(id);
            if (ventaInDataBase == null)
                return NotFound();

            _UnityOfWork.Ventas.Remove(ventaInDataBase);
            _UnityOfWork.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _UnityOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}