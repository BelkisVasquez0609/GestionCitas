using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionCitas.Models;

namespace GestionCitas.Controllers
{
    public class EstadoCitasController : Controller
    {
        private GestionCitas1Entities1 db = new GestionCitas1Entities1();

        // GET: EstadoCitas
        public ActionResult Index()
        {
            return View(db.EstadoCitas.ToList());
        }

        // GET: EstadoCitas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCitas estadoCita = db.EstadoCitas.Find(id);
            if (estadoCita == null)
            {
                return HttpNotFound();
            }
            return View(estadoCita);
        }

        // GET: EstadoCitas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoCitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Seq,Descripcion,FechaCreado")] EstadoCitas estadoCita)
        {
            if (ModelState.IsValid)
            {
                estadoCita.Id = Guid.NewGuid();
                estadoCita.FechaCreado = DateTime.Now;
                db.EstadoCitas.Add(estadoCita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoCita);
        }

        // GET: EstadoCitas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCitas estadoCita = db.EstadoCitas.Find(id);
            if (estadoCita == null)
            {
                return HttpNotFound();
            }
            return View(estadoCita);
        }

        // POST: EstadoCitas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seq,Descripcion,FechaCreado")] EstadoCitas estadoCita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoCita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoCita);
        }

        // GET: EstadoCitas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoCitas estadoCita = db.EstadoCitas.Find(id);
            if (estadoCita == null)
            {
                return HttpNotFound();
            }
            return View(estadoCita);
        }

        // POST: EstadoCitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EstadoCitas estadoCita = db.EstadoCitas.Find(id);
            db.EstadoCitas.Remove(estadoCita);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
