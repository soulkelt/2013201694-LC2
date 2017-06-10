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
    public class EncomiendasController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public EncomiendasController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var Encomiendas = _UnityOfWork.Encomiendas.GetAll();

            if (Encomiendas == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var EncomiendasDTO = new List<EncomiendaDTO>();

            foreach (var encomienda in Encomiendas)
                EncomiendasDTO.Add(Mapper.Map<Encomienda, EncomiendaDTO>(encomienda));

            return Ok(EncomiendasDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var Encomienda = _UnityOfWork.Encomiendas.Get(id);

            if (Encomienda == null)
                return NotFound();

            return Ok(Mapper.Map<Encomienda, EncomiendaDTO>(Encomienda));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, EncomiendaDTO EncomiendaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var encomiendaInPersistence = _UnityOfWork.Encomiendas.Get(id);
            if (encomiendaInPersistence == null)
                return NotFound();

            Mapper.Map<EncomiendaDTO, Encomienda>(EncomiendaDTO, encomiendaInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(EncomiendaDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(EncomiendaDTO encomiendaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var encomienda = Mapper.Map<EncomiendaDTO, Encomienda>(encomiendaDTO);

            _UnityOfWork.Encomiendas.Add(encomienda);
            _UnityOfWork.SaveChanges();

            encomiendaDTO.ServicioId = encomienda.ServicioId;

            return Created(new Uri(Request.RequestUri + "/" + encomienda.ServicioId), encomiendaDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var encomiendaInDataBase = _UnityOfWork.Encomiendas.Get(id);
            if (encomiendaInDataBase == null)
                return NotFound();

            _UnityOfWork.Encomiendas.Remove(encomiendaInDataBase);
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