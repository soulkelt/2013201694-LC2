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
    public class LugarViajesController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public LugarViajesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var LugarViajes = _UnityOfWork.LugarViajes.GetAll();

            if (LugarViajes == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var LugarViajesDTO = new List<LugarViajeDTO>();

            foreach (var lugarviaje in LugarViajes)
                LugarViajesDTO.Add(Mapper.Map<LugarViaje, LugarViajeDTO>(lugarviaje));

            return Ok(LugarViajesDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var LugarViaje = _UnityOfWork.LugarViajes.Get(id);

            if (LugarViaje == null)
                return NotFound();

            return Ok(Mapper.Map<LugarViaje, LugarViajeDTO>(LugarViaje));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, LugarViajeDTO LugarViajeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lugarviajeInPersistence = _UnityOfWork.LugarViajes.Get(id);
            if (lugarviajeInPersistence == null)
                return NotFound();

            Mapper.Map<LugarViajeDTO, LugarViaje>(LugarViajeDTO, lugarviajeInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(LugarViajeDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(LugarViajeDTO LugarViajeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lugarviaje = Mapper.Map<LugarViajeDTO, LugarViaje>(LugarViajeDTO);

            _UnityOfWork.LugarViajes.Add(lugarviaje);
            _UnityOfWork.SaveChanges();

            LugarViajeDTO.LugarViajeId = lugarviaje.LugarViajeId;

            return Created(new Uri(Request.RequestUri + "/" + lugarviaje.LugarViajeId), LugarViajeDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var lugarviajeInDataBase = _UnityOfWork.LugarViajes.Get(id);
            if (lugarviajeInDataBase == null)
                return NotFound();

            _UnityOfWork.LugarViajes.Remove(lugarviajeInDataBase);
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