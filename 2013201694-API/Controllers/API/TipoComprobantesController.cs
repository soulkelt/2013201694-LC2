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
    public class TipoComprobantesController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoComprobantesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var TipoComprobantes = _UnityOfWork.TipoComprobantes.GetAll();

            if (TipoComprobantes == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var TipoComprobantesDTO = new List<TipoComprobanteDTO>();

            foreach (var tipocomprobante in TipoComprobantes)
                TipoComprobantesDTO.Add(Mapper.Map<TipoComprobante, TipoComprobanteDTO>(tipocomprobante));

            return Ok(TipoComprobantesDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var TipoComprobante = _UnityOfWork.TipoComprobantes.Get(id);

            if (TipoComprobante == null)
                return NotFound();

            return Ok(Mapper.Map<TipoComprobante, TipoComprobanteDTO>(TipoComprobante));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TipoComprobanteDTO TipoComprobanteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipocomprobanteInPersistence = _UnityOfWork.TipoComprobantes.Get(id);
            if (tipocomprobanteInPersistence == null)
                return NotFound();

            Mapper.Map<TipoComprobanteDTO, TipoComprobante>(TipoComprobanteDTO, tipocomprobanteInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(TipoComprobanteDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(TipoComprobanteDTO tipocomprobanteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipocomprobante = Mapper.Map<TipoComprobanteDTO, TipoComprobante>(tipocomprobanteDTO);

            _UnityOfWork.TipoComprobantes.Add(tipocomprobante);
            _UnityOfWork.SaveChanges();

            tipocomprobanteDTO.TipoComprobanteId = tipocomprobante.TipoComprobanteId;

            return Created(new Uri(Request.RequestUri + "/" + tipocomprobante.TipoComprobanteId), tipocomprobanteDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipocomprobanteInDataBase = _UnityOfWork.TipoComprobantes.Get(id);
            if (tipocomprobanteInDataBase == null)
                return NotFound();

            _UnityOfWork.TipoComprobantes.Remove(tipocomprobanteInDataBase);
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