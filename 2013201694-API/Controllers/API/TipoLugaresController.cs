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
    public class TipoLugaresController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoLugaresController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var TipoLugares = _UnityOfWork.TipoLugares.GetAll();

            if (TipoLugares == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var TipoLugaresDTO = new List<TipoLugarDTO>();

            foreach (var tipolugar in TipoLugares)
                TipoLugaresDTO.Add(Mapper.Map<TipoLugar, TipoLugarDTO>(tipolugar));

            return Ok(TipoLugaresDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var TipoLugar = _UnityOfWork.TipoLugares.Get(id);

            if (TipoLugar == null)
                return NotFound();

            return Ok(Mapper.Map<TipoLugar, TipoLugarDTO>(TipoLugar));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, TipoLugarDTO TipoLugarDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipolugarInPersistence = _UnityOfWork.TipoLugares.Get(id);
            if (tipolugarInPersistence == null)
                return NotFound();

            Mapper.Map<TipoLugarDTO, TipoLugar>(TipoLugarDTO, tipolugarInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(TipoLugarDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(TipoLugarDTO tipolugarDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipolugar = Mapper.Map<TipoLugarDTO, TipoLugar>(tipolugarDTO);

            _UnityOfWork.TipoLugares.Add(tipolugar);
            _UnityOfWork.SaveChanges();

            tipolugarDTO.TipoLugarId = tipolugar.TipoLugarId;

            return Created(new Uri(Request.RequestUri + "/" + tipolugar.TipoLugarId), tipolugarDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var tipolugarInDataBase = _UnityOfWork.TipoLugares.Get(id);
            if (tipolugarInDataBase == null)
                return NotFound();

            _UnityOfWork.TipoLugares.Remove(tipolugarInDataBase);
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