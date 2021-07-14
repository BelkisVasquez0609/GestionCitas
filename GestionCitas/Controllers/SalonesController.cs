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
    public class SalonesController : Controller
    {
        private GestionCitas1Entities2 db = new GestionCitas1Entities2();

        // GET: Salones
        public ActionResult Index()
        {
            return View(db.Salones.ToList());
        }

        // GET: Salones/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salone salone = db.Salones.Find(id);
            if (salone == null)
            {
                return HttpNotFound();
            }
            return View(salone);
        }

        // GET: Salones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Seq,Salon,FechaCreado")] Salone salone)
        {
            if (ModelState.IsValid)
            {
                salone.Id = Guid.NewGuid();
                db.Salones.Add(salone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salone);
        }

        // GET: Salones/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salone salone = db.Salones.Find(id);
            if (salone == null)
            {
                return HttpNotFound();
            }
            return View(salone);
        }

        // POST: Salones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seq,Salon,FechaCreado")] Salone salone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salone);
        }

        // GET: Salones/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salone salone = db.Salones.Find(id);
            if (salone == null)
            {
                return HttpNotFound();
            }
            return View(salone);
        }

        // POST: Salones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Salone salone = db.Salones.Find(id);
            db.Salones.Remove(salone);
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
