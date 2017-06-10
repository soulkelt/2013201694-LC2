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
    public class TipoLugaresController : Controller
    {
        private readonly IUnityOfWork _UnityOfWork;

        public TipoLugaresController(IUnityOfWork unityOfWork)
        {
            _UnityOfWork = unityOfWork;
        }

        // GET: TipoLugares
        public ActionResult Index()
        {
            var tipoLugares = _UnityOfWork.TipoLugares.GetEntity().Include(t => t.LugarViaje);
            return View(tipoLugares.ToList());
        }

        // GET: TipoLugares/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoLugar tipoLugar = _UnityOfWork.TipoLugares.Get(id);
            if (tipoLugar == null)
            {
                return HttpNotFound();
            }
            return View(tipoLugar);
        }

        // GET: TipoLugares/Create
        public ActionResult Create()
        {
            ViewBag.LugarViajeId = new SelectList(_UnityOfWork.LugarViajes.GetEntity(), "LugarViajeId", "NombreLugar");
            return View();
        }

        // POST: TipoLugares/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoLugarId,NombreTipo,LugarViajeId")] TipoLugar tipoLugar)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.TipoLugares.Add(tipoLugar);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LugarViajeId = new SelectList(_UnityOfWork.LugarViajes.GetEntity(), "LugarViajeId", "NombreLugar", tipoLugar.LugarViajeId);
            return View(tipoLugar);
        }

        // GET: TipoLugares/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoLugar tipoLugar = _UnityOfWork.TipoLugares.Get(id);
            if (tipoLugar == null)
            {
                return HttpNotFound();
            }
            ViewBag.LugarViajeId = new SelectList(_UnityOfWork.LugarViajes.GetEntity(), "LugarViajeId", "NombreLugar", tipoLugar.LugarViajeId);
            return View(tipoLugar);
        }

        // POST: TipoLugares/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoLugarId,NombreTipo,LugarViajeId")] TipoLugar tipoLugar)
        {
            if (ModelState.IsValid)
            {
                _UnityOfWork.StateModified(tipoLugar);
                _UnityOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LugarViajeId = new SelectList(_UnityOfWork.LugarViajes.GetEntity(), "LugarViajeId", "NombreLugar", tipoLugar.LugarViajeId);
            return View(tipoLugar);
        }

        // GET: TipoLugares/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoLugar tipoLugar = _UnityOfWork.TipoLugares.Get(id);
            if (tipoLugar == null)
            {
                return HttpNotFound();
            }
            return View(tipoLugar);
        }

        // POST: TipoLugares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoLugar tipoLugar = _UnityOfWork.TipoLugares.Get(id);
            _UnityOfWork.TipoLugares.Remove(tipoLugar);
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
