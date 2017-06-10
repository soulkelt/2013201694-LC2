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
    public class TransportesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TransportesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: Transportes
        public ActionResult Index()
        {
            var servicios = _UnityOfWork.Transportes.GetEntity().Include(t => t.Venta);
            return View(servicios.ToList());
        }

        // GET: Transportes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transporte transporte = _UnityOfWork.Transportes.Get(id);
            if (transporte == null)
            {
                return HttpNotFound();
            }
            return View(transporte);
        }

        // GET: Transportes/Create
        public ActionResult Create()
        {
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion");
            return View();
        }

        // POST: Transportes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServicioId,NombreServicio,Tarifa,VentaId,Observaciones")] Transporte transporte)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Servicios.Add(transporte);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", transporte.VentaId);
            return View(transporte);
        }

        // GET: Transportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transporte transporte = _UnityOfWork.Transportes.Get(id);
            if (transporte == null)
            {
                return HttpNotFound();
            }
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", transporte.VentaId);
            return View(transporte);
        }

        // POST: Transportes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServicioId,NombreServicio,Tarifa,VentaId,Observaciones")] Transporte transporte)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(transporte);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", transporte.VentaId);
            return View(transporte);
        }

        // GET: Transportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transporte transporte = _UnityOfWork.Transportes.Get(id);
            if (transporte == null)
            {
                return HttpNotFound();
            }
            return View(transporte);
        }

        // POST: Transportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transporte transporte = _UnityOfWork.Transportes.Get(id);
            _UnityOfWork.Servicios.Remove(transporte);
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
