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
        private GestionCitas1Entities2 db = new GestionCitas1Entities2();

        // GET: Citas
        public ActionResult Index()
        {
            var citas = db.Citas.Include(c => c.EstadoCita).Include(c => c.Paciente).Include(c => c.Salone);
            return View(citas.ToList());
        }

        // GET: Citas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: Citas/Create
        public ActionResult Create()
        {
            ViewBag.IdEstadoCita = new SelectList(db.EstadoCitas, "Id", "Descripcion");
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Cedula");
            ViewBag.IdSalon = new SelectList(db.Salones, "Id", "Salon");
            return View();
        }

        // POST: Citas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Seq,IdPaciente,IdSalon,Fecha,Hora,Motivo,IdEstadoCita,FechaCreado")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                cita.Id = Guid.NewGuid();
                db.Citas.Add(cita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEstadoCita = new SelectList(db.EstadoCitas, "Id", "Descripcion", cita.IdEstadoCita);
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Cedula", cita.IdPaciente);
            ViewBag.IdSalon = new SelectList(db.Salones, "Id", "Salon", cita.IdSalon);
            return View(cita);
        }

        // GET: Citas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstadoCita = new SelectList(db.EstadoCitas, "Id", "Descripcion", cita.IdEstadoCita);
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Cedula", cita.IdPaciente);
            ViewBag.IdSalon = new SelectList(db.Salones, "Id", "Salon", cita.IdSalon);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seq,IdPaciente,IdSalon,Fecha,Hora,Motivo,IdEstadoCita,FechaCreado")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstadoCita = new SelectList(db.EstadoCitas, "Id", "Descripcion", cita.IdEstadoCita);
            ViewBag.IdPaciente = new SelectList(db.Pacientes, "Id", "Cedula", cita.IdPaciente);
            ViewBag.IdSalon = new SelectList(db.Salones, "Id", "Salon", cita.IdSalon);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cita cita = db.Citas.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Cita cita = db.Citas.Find(id);
            db.Citas.Remove(cita);
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
