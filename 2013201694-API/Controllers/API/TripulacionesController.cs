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
    public class TripulacionesController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TripulacionesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var Tripulaciones = _UnityOfWork.Tripulacion.GetAll();

            if (Tripulaciones == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var TripulacionesDTO = new List<TripulacionDTO>();

            foreach (var tripulacion in Tripulaciones)
                TripulacionesDTO.Add(Mapper.Map<Tripulacion, TripulacionDTO>(tripulacion));

            return Ok(TripulacionesDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var Tripulacion = _UnityOfWork.Tripulacion.Get(id);

            if (Tripulacion == null)
                return NotFound();

            return Ok(Mapper.Map<Tripulacion, TripulacionDTO>(Tripulacion));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TripulacionDTO TripulacionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tripulacionInPersistence = _UnityOfWork.Tripulacion.Get(id);
            if (tripulacionInPersistence == null)
                return NotFound();

            Mapper.Map<TripulacionDTO, Tripulacion>(TripulacionDTO, tripulacionInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(TripulacionDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(TripulacionDTO tripulacionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tripulacion = Mapper.Map<TripulacionDTO, Tripulacion>(tripulacionDTO);

            _UnityOfWork.Tripulacion.Add(tripulacion);
            _UnityOfWork.SaveChanges();

            tripulacionDTO.EmpleadoId = tripulacion.EmpleadoId;

            return Created(new Uri(Request.RequestUri + "/" + tripulacion.EmpleadoId), tripulacionDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tripulacionInDataBase = _UnityOfWork.Tripulacion.Get(id);
            if (tripulacionInDataBase == null)
                return NotFound();

            _UnityOfWork.Tripulacion.Remove(tripulacionInDataBase);
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