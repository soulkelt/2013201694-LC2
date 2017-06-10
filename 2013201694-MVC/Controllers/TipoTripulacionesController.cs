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
    public class TipoTripulacionesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoTripulacionesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: TipoTripulaciones
        public ActionResult Index()
        {
            var tipoTripulacions = _UnityOfWork.TipoTripulacion.GetEntity().Include(t => t.Tripulacion);
            return View(tipoTripulacions.ToList());
        }

        // GET: TipoTripulaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTripulacion tipoTripulacion = _UnityOfWork.TipoTripulacion.Get(id);
            if (tipoTripulacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoTripulacion);
        }

        // GET: TipoTripulaciones/Create
        public ActionResult Create()
        {
            ViewBag.TripulacionId = new SelectList(_UnityOfWork.Tripulacion.GetEntity(), "EmpleadoId", "Nombre");
            return View();
        }

        // POST: TipoTripulaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoTripulacionId,Descripcion,TripulacionId")] TipoTripulacion tipoTripulacion)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.TipoTripulacion.Add(tipoTripulacion);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TripulacionId = new SelectList(_UnityOfWork.Tripulacion.GetEntity(), "EmpleadoId", "Nombre", tipoTripulacion.TripulacionId);
            return View(tipoTripulacion);
        }

        // GET: TipoTripulaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTripulacion tipoTripulacion = _UnityOfWork.TipoTripulacion.Get(id);
            if (tipoTripulacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.TripulacionId = new SelectList(_UnityOfWork.Tripulacion.GetEntity(), "EmpleadoId", "Nombre", tipoTripulacion.TripulacionId);
            return View(tipoTripulacion);
        }

        // POST: TipoTripulaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoTripulacionId,Descripcion,TripulacionId")] TipoTripulacion tipoTripulacion)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(tipoTripulacion);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TripulacionId = new SelectList(_UnityOfWork.Tripulacion.GetEntity(), "EmpleadoId", "Nombre", tipoTripulacion.TripulacionId);
            return View(tipoTripulacion);
        }

        // GET: TipoTripulaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoTripulacion tipoTripulacion = _UnityOfWork.TipoTripulacion.Get(id);
            if (tipoTripulacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoTripulacion);
        }

        // POST: TipoTripulaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoTripulacion tipoTripulacion = _UnityOfWork.TipoTripulacion.Get(id);
            _UnityOfWork.TipoTripulacion.Remove(tipoTripulacion);
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
