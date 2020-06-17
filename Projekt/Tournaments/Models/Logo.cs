using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tournaments.Models
{
    public class Logo
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public virtual Tournament Tournament{ get; set; }
        public Logo() { }
        public Logo (string Address, Tournament Tournament)
        {
            this.Address = Address;
            this.Tournament = Tournament;
        }
    }
}