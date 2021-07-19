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
    public class OcupacionsController : Controller
    {
        private GestionCitas1Entities1 db = new GestionCitas1Entities1();

        // GET: Ocupacions
        public ActionResult Index()
        {
            return View(db.Ocupacion.ToList());
        }

        // GET: Ocupacions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocupacion ocupacion = db.Ocupacion.Find(id);
            if (ocupacion == null)
            {
                return HttpNotFound();
            }
            return View(ocupacion);
        }

        // GET: Ocupacions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ocupacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Seq,Descripcion,FechaCreado")] Ocupacion ocupacion)
        {
            if (ModelState.IsValid)
            {
                ocupacion.Id = Guid.NewGuid();
                ocupacion.FechaCreado = DateTime.Now;
                db.Ocupacion.Add(ocupacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ocupacion);
        }

        // GET: Ocupacions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocupacion ocupacion = db.Ocupacion.Find(id);
            if (ocupacion == null)
            {
                return HttpNotFound();
            }
            return View(ocupacion);
        }

        // POST: Ocupacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seq,Descripcion,FechaCreado")] Ocupacion ocupacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocupacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ocupacion);
        }

        // GET: Ocupacions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocupacion ocupacion = db.Ocupacion.Find(id);
            if (ocupacion == null)
            {
                return HttpNotFound();
            }
            return View(ocupacion);
        }

        // POST: Ocupacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Ocupacion ocupacion = db.Ocupacion.Find(id);
            db.Ocupacion.Remove(ocupacion);
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
