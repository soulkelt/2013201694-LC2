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
    public class TipoViajesController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoViajesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var TipoViajes = _UnityOfWork.TipoViajes.GetAll();

            if (TipoViajes == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var TipoViajesDTO = new List<TipoViajeDTO>();

            foreach (var tipoviaje in TipoViajes)
                TipoViajesDTO.Add(Mapper.Map<TipoViaje, TipoViajeDTO>(tipoviaje));

            return Ok(TipoViajesDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var TipoViaje = _UnityOfWork.TipoViajes.Get(id);

            if (TipoViaje == null)
                return NotFound();

            return Ok(Mapper.Map<TipoViaje, TipoViajeDTO>(TipoViaje));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TipoViajeDTO TipoViajeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipoviajeInPersistence = _UnityOfWork.TipoViajes.Get(id);
            if (tipoviajeInPersistence == null)
                return NotFound();

            Mapper.Map<TipoViajeDTO, TipoViaje>(TipoViajeDTO, tipoviajeInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(TipoViajeDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(TipoViajeDTO tipoviajeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipoviaje = Mapper.Map<TipoViajeDTO, TipoViaje>(tipoviajeDTO);

            _UnityOfWork.TipoViajes.Add(tipoviaje);
            _UnityOfWork.SaveChanges();

            tipoviajeDTO.TipoViajeId = tipoviaje.TipoViajeId;

            return Created(new Uri(Request.RequestUri + "/" + tipoviaje.TipoViajeId), tipoviajeDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipoviajeInDataBase = _UnityOfWork.TipoViajes.Get(id);
            if (tipoviajeInDataBase == null)
                return NotFound();

            _UnityOfWork.TipoViajes.Remove(tipoviajeInDataBase);
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