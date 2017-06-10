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
using _2013201694_PER.Repositories;

namespace _2013201694_API.Areas.HelpPage.Controllers
{
    public class BusesController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public BusesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var Buses = _UnityOfWork.Buses.GetAll();

            if (Buses == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var BusesDTO = new List<BusDTO>();

            foreach (var bus in Buses)
                BusesDTO.Add(Mapper.Map<Bus, BusDTO>(bus));

            return Ok(BusesDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var Bus = _UnityOfWork.Buses.Get(id);

            if (Bus == null)
                return NotFound();

            return Ok(Mapper.Map<Bus, BusDTO>(Bus));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, BusDTO BusDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var busInPersistence = _UnityOfWork.Buses.Get(id);
            if (busInPersistence == null)
                return NotFound();

            Mapper.Map<BusDTO, Bus>(BusDTO, busInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(BusDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(BusDTO busDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var bus = Mapper.Map<BusDTO, Bus>(busDTO);

            _UnityOfWork.Buses.Add(bus);
            _UnityOfWork.SaveChanges();

            busDTO.BusId = bus.BusId;

            return Created(new Uri(Request.RequestUri + "/" + bus.BusId), busDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var busInDataBase = _UnityOfWork.Buses.Get(id);
            if (busInDataBase == null)
                return NotFound();

            _UnityOfWork.Buses.Remove(busInDataBase);
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