using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournaments.Models
{
    public class Battle
    {
        public int Id { get; set; }
        public virtual ApplicationUser Player1 { get; set; }
        public virtual ApplicationUser Player2 { get; set; }
        public string Winner1Id { get; set; } = null;
        public string Winner2Id { get; set; } = null;
        public virtual Tournament Tournament { get; set; }
        public Battle() { }
        public Battle(ApplicationUser Player1, ApplicationUser Player2, Tournament Tournament)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;
            this.Tournament = Tournament;
        }
    }
}