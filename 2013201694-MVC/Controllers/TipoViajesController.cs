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
    public class TipoViajesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoViajesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: TipoViajes
        public ActionResult Index()
        {
            var tipoViajes = _UnityOfWork.TipoViajes.GetEntity().Include(t => t.Servicio);
            return View(tipoViajes.ToList());
        }

        // GET: TipoViajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoViaje tipoViaje = _UnityOfWork.TipoViajes.Get(id);
            if (tipoViaje == null)
            {
                return HttpNotFound();
            }
            return View(tipoViaje);
        }

        // GET: TipoViajes/Create
        public ActionResult Create()
        {
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio");
            return View();
        }

        // POST: TipoViajes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoViajeId,Nombre,ServicioId")] TipoViaje tipoViaje)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.TipoViajes.Add(tipoViaje);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", tipoViaje.ServicioId);
            return View(tipoViaje);
        }

        // GET: TipoViajes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoViaje tipoViaje = _UnityOfWork.TipoViajes.Get(id);
            if (tipoViaje == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", tipoViaje.ServicioId);
            return View(tipoViaje);
        }

        // POST: TipoViajes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoViajeId,Nombre,ServicioId")] TipoViaje tipoViaje)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(tipoViaje);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", tipoViaje.ServicioId);
            return View(tipoViaje);
        }

        // GET: TipoViajes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoViaje tipoViaje = _UnityOfWork.TipoViajes.Get(id);
            if (tipoViaje == null)
            {
                return HttpNotFound();
            }
            return View(tipoViaje);
        }

        // POST: TipoViajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoViaje tipoViaje = _UnityOfWork.TipoViajes.Get(id);
            _UnityOfWork.TipoViajes.Remove(tipoViaje);
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
