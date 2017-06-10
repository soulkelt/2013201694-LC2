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
    public class ClientesController : ApiController
    {
        private readonly IUnityOfWork _UnityOfWork;

        public ClientesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var Clientes = _UnityOfWork.Clientes.GetAll();

            if (Clientes == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var ClientesDTO = new List<ClienteDTO>();

            foreach (var cliente in Clientes)
                ClientesDTO.Add(Mapper.Map<Cliente, ClienteDTO>(cliente));

            return Ok(ClientesDTO);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var Cliente = _UnityOfWork.Clientes.Get(id);

            if (Cliente == null)
                return NotFound();

            return Ok(Mapper.Map<Cliente, ClienteDTO>(Cliente));
        }

        [HttpPut]
        public IHttpActionResult Update(int id, ClienteDTO ClienteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var clienteInPersistence = _UnityOfWork.Clientes.Get(id);
            if (clienteInPersistence == null)
                return NotFound();

            Mapper.Map<ClienteDTO, Cliente>(ClienteDTO, clienteInPersistence);

            _UnityOfWork.SaveChanges();

            return Ok(ClienteDTO);
        }

        [HttpPost]
        public IHttpActionResult Create(ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cliente = Mapper.Map<ClienteDTO, Cliente>(clienteDTO);

            _UnityOfWork.Clientes.Add(cliente);
            _UnityOfWork.SaveChanges();

            clienteDTO.ClienteId = cliente.ClienteId;

            return Created(new Uri(Request.RequestUri + "/" + cliente.ClienteId), clienteDTO);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var clienteInDataBase = _UnityOfWork.Clientes.Get(id);
            if (clienteInDataBase == null)
                return NotFound();

            _UnityOfWork.Clientes.Remove(clienteInDataBase);
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