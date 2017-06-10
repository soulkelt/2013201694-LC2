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

namespace _2013201694_API.Controllers.API
{
    public class AdministrativosController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public AdministrativosController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var Administrativos = _UnityOfWork.Administrativos.GetAll();

            if (Administrativos == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var AdministrativosDTO = new List<AdministrativoDTO>();

            foreach (var administrativo in Administrativos)
                AdministrativosDTO.Add(Mapper.Map<Administrativo, AdministrativoDTO>(administrativo));

            return Ok(AdministrativosDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var Administrativo = _UnityOfWork.Administrativos.Get(id);

            if (Administrativo == null)
                return NotFound();

            return Ok(Mapper.Map<Administrativo, AdministrativoDTO>(Administrativo));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, AdministrativoDTO AdministrativoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var administrativoInPersistence = _UnityOfWork.Administrativos.Get(id);
            if (administrativoInPersistence == null)
                return NotFound();

            Mapper.Map<AdministrativoDTO, Administrativo>(AdministrativoDTO, administrativoInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(AdministrativoDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(AdministrativoDTO administrativoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var administrativo = Mapper.Map<AdministrativoDTO, Administrativo>(administrativoDTO);

            _UnityOfWork.Administrativos.Add(administrativo);
            _UnityOfWork.SaveChanges();

            administrativoDTO.EmpleadoId = administrativo.EmpleadoId;

            return Created(new Uri(Request.RequestUri + "/" + administrativo.EmpleadoId), administrativoDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var administrativoInDataBase = _UnityOfWork.Administrativos.Get(id);
            if (administrativoInDataBase == null)
                return NotFound();

            _UnityOfWork.Administrativos.Remove(administrativoInDataBase);
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