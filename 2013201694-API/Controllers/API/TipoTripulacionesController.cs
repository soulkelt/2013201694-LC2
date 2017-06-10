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
    public class TipoTripulacionesController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoTripulacionesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var TipoTripulaciones = _UnityOfWork.TipoTripulacion.GetAll();

            if (TipoTripulaciones == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var TipoTripulacionesDTO = new List<TipoTripulacionDTO>();

            foreach (var tipotripulacion in TipoTripulaciones)
                TipoTripulacionesDTO.Add(Mapper.Map<TipoTripulacion, TipoTripulacionDTO>(tipotripulacion));

            return Ok(TipoTripulacionesDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var TipoTripulacion = _UnityOfWork.TipoTripulacion.Get(id);

            if (TipoTripulacion == null)
                return NotFound();

            return Ok(Mapper.Map<TipoTripulacion, TipoTripulacionDTO>(TipoTripulacion));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TipoTripulacionDTO TipoTripulacionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipotripulacionInPersistence = _UnityOfWork.TipoTripulacion.Get(id);
            if (tipotripulacionInPersistence == null)
                return NotFound();

            Mapper.Map<TipoTripulacionDTO, TipoTripulacion>(TipoTripulacionDTO, tipotripulacionInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(TipoTripulacionDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(TipoTripulacionDTO tipotripulacionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipotripulacion = Mapper.Map<TipoTripulacionDTO, TipoTripulacion>(tipotripulacionDTO);

            _UnityOfWork.TipoTripulacion.Add(tipotripulacion);
            _UnityOfWork.SaveChanges();

            tipotripulacionDTO.TipoTripulacionId = tipotripulacion.TipoTripulacionId;

            return Created(new Uri(Request.RequestUri + "/" + tipotripulacion.TipoTripulacionId), tipotripulacionDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipotripulacionInDataBase = _UnityOfWork.TipoTripulacion.Get(id);
            if (tipotripulacionInDataBase == null)
                return NotFound();

            _UnityOfWork.TipoTripulacion.Remove(tipotripulacionInDataBase);
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