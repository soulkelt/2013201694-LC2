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
    public class TripulacionesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TripulacionesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: Tripulaciones
        public ActionResult Index()
        {
            var empleadoes = _UnityOfWork.Tripulacion.GetEntity().Include(t => t.Bus);
            return View(empleadoes.ToList());
        }

        // GET: Tripulaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tripulacion tripulacion = _UnityOfWork.Tripulacion.Get(id);
            if (tripulacion == null)
            {
                return HttpNotFound();
            }
            return View(tripulacion);
        }

        // GET: Tripulaciones/Create
        public ActionResult Create()
        {
            ViewBag.BusId = new SelectList(_UnityOfWork.Buses.GetEntity(), "BusId", "Placa");
            return View();
        }

        // POST: Tripulaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpleadoId,Nombre,Apellidos,DNI,Edad,Sueldo,NombreTripulacion,BusId")] Tripulacion tripulacion)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Empleados.Add(tripulacion);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusId = new SelectList(_UnityOfWork.Buses.GetEntity(), "BusId", "Placa", tripulacion.BusId);
            return View(tripulacion);
        }

        // GET: Tripulaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tripulacion tripulacion = _UnityOfWork.Tripulacion.Get(id);
            if (tripulacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusId = new SelectList(_UnityOfWork.Buses.GetEntity(), "BusId", "Placa", tripulacion.BusId);
            return View(tripulacion);
        }

        // POST: Tripulaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpleadoId,Nombre,Apellidos,DNI,Edad,Sueldo,NombreTripulacion,BusId")] Tripulacion tripulacion)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(tripulacion);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusId = new SelectList(_UnityOfWork.Buses.GetEntity(), "BusId", "Placa", tripulacion.BusId);
            return View(tripulacion);
        }

        // GET: Tripulaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tripulacion tripulacion = _UnityOfWork.Tripulacion.Get(id);
            if (tripulacion == null)
            {
                return HttpNotFound();
            }
            return View(tripulacion);
        }

        // POST: Tripulaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tripulacion tripulacion = _UnityOfWork.Tripulacion.Get(id);
            _UnityOfWork.Empleados.Remove(tripulacion);
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
