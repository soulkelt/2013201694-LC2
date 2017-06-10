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
    public class AdministrativosController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public AdministrativosController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: Administrativos
        public ActionResult Index()
        {
            var empleados = _UnityOfWork.Administrativos.GetEntity().Include(a => a.Venta);
            return View(empleados.ToList());
        }

        // GET: Administrativos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrativo administrativo = _UnityOfWork.Administrativos.Get(id);
            if (administrativo == null)
            {
                return HttpNotFound();
            }
            return View(administrativo);
        }

        // GET: Administrativos/Create
        public ActionResult Create()
        {
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion");
            return View();
        }

        // POST: Administrativos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpleadoId,Nombre,Apellidos,DNI,Edad,Sueldo,Cargo,VentaId")] Administrativo administrativo)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.Empleados.Add(administrativo);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", administrativo.VentaId);
            return View(administrativo);
        }

        // GET: Administrativos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrativo administrativo = _UnityOfWork.Administrativos.Get(id);
            if (administrativo == null)
            {
                return HttpNotFound();
            }
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", administrativo.VentaId);
            return View(administrativo);
        }

        // POST: Administrativos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpleadoId,Nombre,Apellidos,DNI,Edad,Sueldo,Cargo,VentaId")] Administrativo administrativo)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(administrativo);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VentaId = new SelectList(_UnityOfWork.Ventas.GetEntity(), "VentaId", "Descripcion", administrativo.VentaId);
            return View(administrativo);
        }

        // GET: Administrativos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrativo administrativo = _UnityOfWork.Administrativos.Get(id);
            if (administrativo == null)
            {
                return HttpNotFound();
            }
            return View(administrativo);
        }

        // POST: Administrativos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrativo administrativo = _UnityOfWork.Administrativos.Get(id);
            _UnityOfWork.Empleados.Remove(administrativo);
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
