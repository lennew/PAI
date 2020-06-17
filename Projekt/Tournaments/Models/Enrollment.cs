using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tournaments.Models
{
    public class Enrollment
    {
        [Key, Column(Order = 1)]
        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; }
        [Key, Column(Order = 2)]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int LicenceNumber { get; set; }
        public int Ranking { get; set; }
        [DisplayName("Punkty")]
        public int Points { get; set; }
    }
}