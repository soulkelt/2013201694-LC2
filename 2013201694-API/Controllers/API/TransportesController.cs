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
    public class TransportesController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TransportesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var Transportes = _UnityOfWork.Transportes.GetAll();

            if (Transportes == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var TransportesDTO = new List<TransporteDTO>();

            foreach (var transporte in Transportes)
                TransportesDTO.Add(Mapper.Map<Transporte, TransporteDTO>(transporte));

            return Ok(TransportesDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var Transporte = _UnityOfWork.Transportes.Get(id);

            if (Transporte == null)
                return NotFound();

            return Ok(Mapper.Map<Transporte, TransporteDTO>(Transporte));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TransporteDTO TransporteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transporteInPersistence = _UnityOfWork.Transportes.Get(id);
            if (transporteInPersistence == null)
                return NotFound();

            Mapper.Map<TransporteDTO, Transporte>(TransporteDTO, transporteInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(TransporteDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(TransporteDTO transporteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transporte = Mapper.Map<TransporteDTO, Transporte>(transporteDTO);

            _UnityOfWork.Transportes.Add(transporte);
            _UnityOfWork.SaveChanges();

            transporteDTO.ServicioId = transporte.ServicioId;

            return Created(new Uri(Request.RequestUri + "/" + transporte.ServicioId), transporteDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transporteInDataBase = _UnityOfWork.Transportes.Get(id);
            if (transporteInDataBase == null)
                return NotFound();

            _UnityOfWork.Transportes.Remove(transporteInDataBase);
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