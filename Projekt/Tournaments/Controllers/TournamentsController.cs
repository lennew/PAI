using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tournaments.Models;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using Microsoft.Ajax.Utilities;
using System.IO;
using PagedList;

namespace Tournaments.Controllers
{
    public class TournamentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tournaments
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            var tournaments = from t in db.Tournaments select t;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                tournaments = tournaments.Where(t => t.Name.Contains(searchString));
            }

            tournaments = tournaments.OrderBy(t => t.Date);

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(tournaments.ToPagedList(pageNumber, pageSize));
        }

        // GET: Tournaments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            ViewBag.Organizer = tournament.Organizer == currentUser;
            ViewBag.LimitExceeded = tournament.NumberOfParticipants >= tournament.Limit;
            ViewBag.AlreadyEnrolled = !(db.Enrollments.FirstOrDefault(e => (e.TournamentId == id && e.ApplicationUserId == currentUserId)) is null);
            ViewBag.CurrentUserId = currentUserId;
            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }

        // GET: Tournaments/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            return View();
        }

        // POST: Tournaments/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DisciplineId,Date,Limit,Deadline,Lattitude,Longitude")] Tournament tournament, HttpPostedFileBase[] logos)
        {
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            Debug.WriteLine(tournament.Name + " " + tournament.Lattitude + " " + tournament.Longitude);
            if (ModelState.IsValid)
            {
                if (logos[0] != null)
                {
                    foreach (var image in logos)
                    {
                        string path = Path.Combine("~\\Logos", Path.GetFileName(image.FileName));
                        string absolutePath = Path.Combine(Server.MapPath("~/Logos"), Path.GetFileName(image.FileName));
                        image.SaveAs(absolutePath);
                        Logo logo = new Logo(path, tournament);
                        db.Logoes.Add(logo);
                        tournament.Logos.Add(logo);
                    }
                }
                tournament.Discipline = db.Disciplines.Find(tournament.DisciplineId);
                tournament.NumberOfParticipants = 0;
                tournament.Organizer = currentUser;
                currentUser.TournamentsAsOrganizers.Add(tournament);
                db.Tournaments.Add(tournament);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            
            if (!currentUser.TournamentsAsOrganizers.Contains(tournament))
            {
                ViewBag.errorMessage = "Nie jesteś organizatorem tego turnieju, nie możesz go edytować.";
                return View("Error");
            } 
            else if (DateTime.Now > tournament.Deadline)
            {
                ViewBag.errorMessage = "Turniej trwa, nie możesz go edytować.";
                return View("Error");
            }
            else
            {
                return View(tournament);
            }
        }

        // POST: Tournaments/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DisciplineId,Date,Limit,Deadline,Lattitude,Longitude")] Tournament tournament)
        {
            ViewBag.Disciplines = new SelectList(db.Disciplines, "Id", "Name");
            if (ModelState.IsValid)
            {
                db.Entry(tournament).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tournament);
        }

        // GET: Tournaments/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (!currentUser.TournamentsAsOrganizers.Contains(tournament))
            {
                ViewBag.errorMessage = "Nie jesteś organizatorem tego turnieju, nie możesz go usunąć.";
                return View("Error");
            }
            else if (DateTime.Now > tournament.Deadline)
            {
                ViewBag.errorMessage = "Turniej trwa, nie możesz go usunąć.";
                return View("Error");
            } else
            {
                return View(tournament);
            }
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            db.Tournaments.Remove(tournament);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tournaments/Enroll/5
        [Authorize]
        public ActionResult Enroll(int? id, string ReturnUrl)
        {
            string currentUserId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = db.Tournaments.Find(id);
            ViewBag.ReturnUrl = ReturnUrl;
            if (tournament == null)
            {
                return HttpNotFound();
            }
            if(tournament.Enrollments.FirstOrDefault(e => e.ApplicationUserId == currentUserId) != null)
            {
                ViewBag.errorMessage = "Nie możesz zapisać się dwukrotnie na ten sam turniej!";
                return View("Error");
            } else if (DateTime.Now > tournament.Deadline)
            {
                ViewBag.errorMessage = "Termin zapisów minął!";
                return View("Error");
            } else if (tournament.NumberOfParticipants >= tournament.Limit)
            {
                ViewBag.errorMessage = "Limit uczestników został osiągnięty!";
                return View("Error");
            }
            ViewBag.TournamentName = tournament.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Enroll([Bind(Include = "LicenceNumber,Ranking")] Enrollment enrollment, int id, string ReturnUrl)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            Tournament tournament = db.Tournaments.FirstOrDefault(x => x.Id == id);
            ViewBag.ReturnUrl = ReturnUrl;
            if (!(db.Enrollments.Find(id, currentUserId) is null))
            {
                ViewBag.errorMessage = "Nie możesz zapisać się dwukrotnie na ten sam turniej!";
                return View("Error");
            }
            if (ModelState.IsValid)
            {    
                enrollment.ApplicationUser = currentUser;
                enrollment.Tournament = tournament;
                db.Enrollments.Add(enrollment);
                db.Tournaments.Find(id).NumberOfParticipants++;
                db.SaveChanges();
                return Redirect(ReturnUrl);
            }

            return View(enrollment);
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
