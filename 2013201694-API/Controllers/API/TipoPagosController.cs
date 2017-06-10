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
    public class TipoPagosController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoPagosController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var TipoPagos = _UnityOfWork.TipoPagos.GetAll();

            if (TipoPagos == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var TipoPagosDTO = new List<TipoPagoDTO>();

            foreach (var tipopago in TipoPagos)
                TipoPagosDTO.Add(Mapper.Map<TipoPago, TipoPagoDTO>(tipopago));

            return Ok(TipoPagosDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var TipoPago = _UnityOfWork.TipoPagos.Get(id);

            if (TipoPago == null)
                return NotFound();

            return Ok(Mapper.Map<TipoPago, TipoPagoDTO>(TipoPago));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TipoPagoDTO TipoPagoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipopagoInPersistence = _UnityOfWork.TipoPagos.Get(id);
            if (tipopagoInPersistence == null)
                return NotFound();

            Mapper.Map<TipoPagoDTO, TipoPago>(TipoPagoDTO, tipopagoInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(TipoPagoDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(TipoPagoDTO tipopagoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipopago = Mapper.Map<TipoPagoDTO, TipoPago>(tipopagoDTO);

            _UnityOfWork.TipoPagos.Add(tipopago);
            _UnityOfWork.SaveChanges();

            tipopagoDTO.TipoPagoId = tipopago.TipoPagoId;

            return Created(new Uri(Request.RequestUri + "/" + tipopago.TipoPagoId), tipopagoDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipopagoInDataBase = _UnityOfWork.TipoPagos.Get(id);
            if (tipopagoInDataBase == null)
                return NotFound();

            _UnityOfWork.TipoPagos.Remove(tipopagoInDataBase);
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