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
    public class CitasController : Controller
    {
        private GestionCitas1Entities1 db = new GestionCitas1Entities1();

        // GET: Citas
        public ActionResult Index()
        {
            var citas = db.Citas.Include(c => c.EstadoCitas).Include(c => c.Salones).Include(c => c.Pacientes);
            return View(citas.ToList());
        }

        // GET: Citas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            return View(citas);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            ViewBag.IdEstadoCita = new SelectList(db.EstadoCitas, "Id", "Descripcion");
            ViewBag.IdSalon = new SelectList(db.Salones, "Id", "Salon");
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre");
            return View();
        }

        // POST: Citas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Seq,IdPaciente,IdSalon,Fecha,Hora,Motivo,IdEstadoCita,FechaCreado")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                citas.Id = Guid.NewGuid();
                citas.FechaCreado = DateTime.Now;
                db.Citas.Add(citas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEstadoCita = new SelectList(db.EstadoCitas, "Id", "Descripcion", citas.IdEstadoCita);
            ViewBag.IdSalon = new SelectList(db.Salones, "Id", "Salon", citas.IdSalon);
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", citas.IdPaciente);
            return View(citas);
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstadoCita = new SelectList(db.EstadoCitas, "Id", "Descripcion", citas.IdEstadoCita);
            ViewBag.IdSalon = new SelectList(db.Salones, "Id", "Salon", citas.IdSalon);
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", citas.IdPaciente);
            return View(citas);
        }

        // POST: Citas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seq,IdPaciente,IdSalon,Fecha,Hora,Motivo,IdEstadoCita,FechaCreado")] Citas citas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstadoCita = new SelectList(db.EstadoCitas, "Id", "Descripcion", citas.IdEstadoCita);
            ViewBag.IdSalon = new SelectList(db.Salones, "Id", "Salon", citas.IdSalon);
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Nombre", citas.IdPaciente);
            return View(citas);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citas citas = db.Citas.Find(id);
            if (citas == null)
            {
                return HttpNotFound();
            }
            return View(citas);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Citas citas = db.Citas.Find(id);
            db.Citas.Remove(citas);
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
