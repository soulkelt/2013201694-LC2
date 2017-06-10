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
    public class TipoPagosController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoPagosController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: TipoPagos
        public ActionResult Index()
        {
            var tipoPagos = _UnityOfWork.TipoPagos.GetEntity().Include(t => t.Venta);
            return View(tipoPagos.ToList());
        }

        // GET: TipoPagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = _UnityOfWork.TipoPagos.Get(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // GET: TipoPagos/Create
        public ActionResult Create()
        {
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion");
            return View();
        }

        // POST: TipoPagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoPagoId,MetodoPago,VentaId")] TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.TipoPagos.Add(tipoPago);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", tipoPago.VentaId);
            return View(tipoPago);
        }

        // GET: TipoPagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = _UnityOfWork.TipoPagos.Get(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", tipoPago.VentaId);
            return View(tipoPago);
        }

        // POST: TipoPagos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoPagoId,MetodoPago,VentaId")] TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(tipoPago);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", tipoPago.VentaId);
            return View(tipoPago);
        }

        // GET: TipoPagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = _UnityOfWork.TipoPagos.Get(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // POST: TipoPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPago tipoPago = _UnityOfWork.TipoPagos.Get(id);
            _UnityOfWork.TipoPagos.Remove(tipoPago);
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
