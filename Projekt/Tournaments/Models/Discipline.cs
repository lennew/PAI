using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournaments.Models
{
    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MatchType { get; set; }

        public Discipline(int Id, string Name, string MatchType)
        {
            this.Id = Id;
            this.Name = Name;
            this.MatchType = MatchType;
        }

        public Discipline() { }

        public override string ToString()
        {
            return this.Name;
        }
    }
}