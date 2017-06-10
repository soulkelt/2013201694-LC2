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
    public class LugarViajesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public LugarViajesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: LugarViajes
        public ActionResult Index()
        {
            var lugarViajes = _UnityOfWork.LugarViajes.GetEntity().Include(l => l.Servicio);
            return View(lugarViajes.ToList());
        }

        // GET: LugarViajes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LugarViaje lugarViaje = _UnityOfWork.LugarViajes.Get(id);
            if (lugarViaje == null)
            {
                return HttpNotFound();
            }
            return View(lugarViaje);
        }

        // GET: LugarViajes/Create
        public ActionResult Create()
        {
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio");
            return View();
        }

        // POST: LugarViajes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LugarViajeId,NombreLugar,ServicioId")] LugarViaje lugarViaje)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.LugarViajes.Add(lugarViaje);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", lugarViaje.ServicioId);
            return View(lugarViaje);
        }

        // GET: LugarViajes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LugarViaje lugarViaje = _UnityOfWork.LugarViajes.Get(id);
            if (lugarViaje == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", lugarViaje.ServicioId);
            return View(lugarViaje);
        }

        // POST: LugarViajes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LugarViajeId,NombreLugar,ServicioId")] LugarViaje lugarViaje)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(lugarViaje);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", lugarViaje.ServicioId);
            return View(lugarViaje);
        }

        // GET: LugarViajes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LugarViaje lugarViaje = _UnityOfWork.LugarViajes.Get(id);

            if (lugarViaje == null)
            {
                return HttpNotFound();
            }
            return View(lugarViaje);
        }

        // POST: LugarViajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LugarViaje lugarViaje = _UnityOfWork.LugarViajes.Get(id);
            _UnityOfWork.LugarViajes.Remove(lugarViaje);
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
