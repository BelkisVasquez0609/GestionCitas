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
    public class PacientesController : Controller
    {
        private GestionCitas1Entities2 db = new GestionCitas1Entities2();

        // GET: Pacientes
        public ActionResult Index()
        {
            var pacientes = db.Pacientes.Include(p => p.EstadoCivil).Include(p => p.Ocupacion);
            return View(pacientes.ToList());
        }

        // GET: Pacientes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            ViewBag.EstadoCivilID = new SelectList(db.EstadoCivils, "Id", "Descripcion");
            ViewBag.OcupacionID = new SelectList(db.Ocupacions, "Id", "Descripcion");
            return View();
        }

        // POST: Pacientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Seq,Cedula,Nombre,Direccion,Telefono,Email,Sexo,OcupacionID,Celular,EstadoCivilID,FechaNacimiento,FechaCreado")] Paciente paciente)
        {
            if (paciente.Cedula != null)
            {
                if (ModelState.IsValid)
                {
                    paciente.Id = Guid.NewGuid();
                    paciente.FechaCreado = DateTime.Now;
                    db.Pacientes.Add(paciente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.EstadoCivilID = new SelectList(db.EstadoCivils, "Id", "Descripcion", paciente.EstadoCivilID);
            ViewBag.OcupacionID = new SelectList(db.Ocupacions, "Id", "Descripcion", paciente.OcupacionID);
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Pacientes.Find(id);
            
            if (paciente == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoCivilID = new SelectList(db.EstadoCivils, "Id", "Descripcion", paciente.EstadoCivilID);
            ViewBag.OcupacionID = new SelectList(db.Ocupacions, "Id", "Descripcion", paciente.OcupacionID);
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seq,Cedula,Nombre,Direccion,Telefono,Email,Sexo,OcupacionID,Celular,EstadoCivilID,FechaNacimiento")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoCivilID = new SelectList(db.EstadoCivils, "Id", "Descripcion", paciente.EstadoCivilID);
            ViewBag.OcupacionID = new SelectList(db.Ocupacions, "Id", "Descripcion", paciente.OcupacionID);
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = db.Pacientes.Find(id);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Paciente paciente = db.Pacientes.Find(id);
            db.Pacientes.Remove(paciente);
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
