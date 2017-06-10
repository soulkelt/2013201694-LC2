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
    public class TipoComprobantesController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoComprobantesController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: TipoComprobantes
        public ActionResult Index()
        {
            var tipoComprobantes = _UnityOfWork.TipoComprobantes.GetEntity().Include(t => t.Venta);
            return View(tipoComprobantes.ToList());
        }

        // GET: TipoComprobantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoComprobante tipoComprobante = _UnityOfWork.TipoComprobantes.Get(id);
            if (tipoComprobante == null)
            {
                return HttpNotFound();
            }
            return View(tipoComprobante);
        }

        // GET: TipoComprobantes/Create
        public ActionResult Create()
        {
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion");
            return View();
        }

        // POST: TipoComprobantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoComprobanteId,NombreComprobante,VentaId")] TipoComprobante tipoComprobante)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.TipoComprobantes.Add(tipoComprobante);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", tipoComprobante.VentaId);
            return View(tipoComprobante);
        }

        // GET: TipoComprobantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoComprobante tipoComprobante = _UnityOfWork.TipoComprobantes.Get(id);
            if (tipoComprobante == null)
            {
                return HttpNotFound();
            }
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", tipoComprobante.VentaId);
            return View(tipoComprobante);
        }

        // POST: TipoComprobantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoComprobanteId,NombreComprobante,VentaId")] TipoComprobante tipoComprobante)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(tipoComprobante);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", tipoComprobante.VentaId);
            return View(tipoComprobante);
        }

        // GET: TipoComprobantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoComprobante tipoComprobante = _UnityOfWork.TipoComprobantes.Get(id);
            if (tipoComprobante == null)
            {
                return HttpNotFound();
            }
            return View(tipoComprobante);
        }

        // POST: TipoComprobantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoComprobante tipoComprobante = _UnityOfWork.TipoComprobantes.Get(id);
            _UnityOfWork.TipoComprobantes.Remove(tipoComprobante);
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
