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
    public class BusesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public BusesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: Buses
        public ActionResult Index()
        {
            var buses = _UnityOfWork.Buses.GetEntity().Include(b => b.Servicio);
            return View(buses.ToList());
        }

        // GET: Buses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = _UnityOfWork.Buses.Get(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // GET: Buses/Create
        public ActionResult Create()
        {
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio");
            return View();
        }

        // POST: Buses/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusId,Placa,SerieMotor,ServicioId")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Buses.Add(bus);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", bus.ServicioId);
            return View(bus);
        }

        // GET: Buses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = _UnityOfWork.Buses.Get(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", bus.ServicioId);
            return View(bus);
        }

        // POST: Buses/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusId,Placa,SerieMotor,ServicioId")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(bus);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ServicioId = new SelectList(_UnityOfWork.Servicios.GetEntity(), "ServicioId", "NombreServicio", bus.ServicioId);
            return View(bus);
        }

        // GET: Buses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = _UnityOfWork.Buses.Get(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Buses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bus bus = _UnityOfWork.Buses.Get(id);
            _UnityOfWork.Buses.Remove(bus);
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
