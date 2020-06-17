using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tournaments.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("Uczestnik")]
        public string Name { get; set; }
        [DisplayName("Uczestnik")]
        public string Surname { get; set; }
        public virtual ICollection<Tournament> TournamentsAsOrganizers { get; set; }
        public virtual ICollection<Battle> Battles { get; set; }
        public virtual ICollection<Enrollment> Enrollments{ get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika

            return userIdentity;
        }

        public ApplicationUser()
        {
            this.Enrollments = new List<Enrollment>();
            this.TournamentsAsOrganizers = new List<Tournament>();
            this.Battles = new List<Battle>();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Logo> Logoes { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (!db.Disciplines.Any())
            {
                IEnumerable<Discipline> disciplines = new List<Discipline>
                {
                    new Discipline(1, "Ping Pong", "ALL_VERSUS_ALL"),
                    new Discipline(2, "Judo", "BEST_WITH_WORST"),
                    new Discipline(3, "Darts", "PAIRS_FROM_TOP")
                };
                db.Disciplines.AddRange(disciplines);
                db.SaveChanges();
            }
            return db;
        }
    }
}