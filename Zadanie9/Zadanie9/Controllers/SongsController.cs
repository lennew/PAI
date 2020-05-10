using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Zadanie9.Models;

namespace Zadanie9.Controllers
{
    public class SongsController : Controller
    {
        private MusicDbContext db = new MusicDbContext();

        // GET: Songs
        public ActionResult Index()
        {
            ViewBag.Genres = db.Genres.ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_SongsList", db.Songs.ToList());
            }
            return View(db.Songs.ToList());
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            ViewBag.Genres = db.Genres.ToList();
            return View();
        }

        // POST: Songs/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Artist,GenreId")] Song song)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Genres = db.Genres.ToList();
                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.Genres = db.Genres.ToList();
            return View(song);
        }

        // POST: Songs/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Artist,GenreId")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Genres = db.Genres.ToList();
                return RedirectToAction("Index");
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpDelete, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
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
