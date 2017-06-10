using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _2013201694_ENT;
using _2013201694_PER;
using _2013201694_ENT.IRepositories;

namespace _2013201694_MVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public ClientesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: Clientes
        public ActionResult Index()
        {
            var clientes = _UnityOfWork.Clientes.GetEntity().Include(c => c.Servicio).Include(c => c.Venta);
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = _UnityOfWork.Clientes.Get(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio");
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion");
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,Nombre,Apellidos,DNI,VentaId,ServicioId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Clientes.Add(cliente);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", cliente.ServicioId);
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", cliente.VentaId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = _UnityOfWork.Clientes.Get(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", cliente.ServicioId);
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", cliente.VentaId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Nombre,Apellidos,DNI,VentaId,ServicioId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(cliente);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", cliente.ServicioId);
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", cliente.VentaId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = _UnityOfWork.Clientes.Get(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = _UnityOfWork.Clientes.Get(id);
            _UnityOfWork.Clientes.Remove(cliente);
            _UnityOfWork.SaveChanges();
            return RedirectToAction("Index");
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
