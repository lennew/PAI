using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tournaments.Models;

namespace Tournaments.Controllers
{
    public class BattlesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Battles/Edit/5
        public ActionResult Edit(int? id, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Battle battle = db.Battles.Find(id);
            if (battle == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> DropdownNames = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = battle.Player1.Name + " " + battle.Player1.Surname,
                    Value = battle.Player1.Id
                },
                new SelectListItem
                {
                    Text = battle.Player2.Name + " " + battle.Player2.Surname,
                    Value = battle.Player2.Id
                }
            };
            ViewBag.DropdownNames = DropdownNames;
            return View(battle);
        }

        // POST: Battles/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, string WinnerId, string ReturnUrl)
        {
            Battle battle = db.Battles.FirstOrDefault(b => b.Id == Id);
            ViewBag.ReturnUrl = ReturnUrl;
            List<SelectListItem> DropdownNames = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = battle.Player1.Name + " " + battle.Player1.Surname,
                    Value = battle.Player1.Id
                },
                new SelectListItem
                {
                    Text = battle.Player2.Name + " " + battle.Player2.Surname,
                    Value = battle.Player2.Id
                }
            };
            ViewBag.DropdownNames = DropdownNames;

            if (WinnerId == null || WinnerId == "")
            {
                return View();
            }
            string currentUserId = User.Identity.GetUserId();
            if (currentUserId == battle.Player1.Id) battle.Winner1Id = WinnerId;
            else battle.Winner2Id = WinnerId;

            if (battle.Winner1Id != null && battle.Winner2Id != null)
            {
                if (battle.Winner1Id == battle.Winner2Id)
                {
                    db.Enrollments.FirstOrDefault(e => e.ApplicationUserId == WinnerId && e.TournamentId == battle.Tournament.Id).Points += 3;
                }
                else
                {
                    battle.Winner1Id = null;
                    battle.Winner2Id = null;
                }
            }
            db.SaveChanges();
            return Redirect(ReturnUrl);
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
