using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tournaments.Models
{
    public class Tournament : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nazwa")]
        [StringLength(30, ErrorMessage = "Maksymalna długość nazwy turnieju to 30 znaków!")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayName("Lokalizacja")]
        [Required(ErrorMessage = "Wybierz lokalizację")]
        public string Lattitude { get; set; }
        [DisplayName("Lokalizacja")]
        public string Longitude { get; set; }

        [Required]
        [DisplayName("Limit uczestników")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Wartość powinna być większa od 0!")]
        public int Limit { get; set; }

        [Required]
        [DisplayName("Koniec zapisów")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Deadline { get; set; }

        [Required]
        [DisplayName("Liczba uczestników")]
        public int NumberOfParticipants { get; set; }

        public Boolean PlayersMatched { get; set; } = false;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date < Deadline)
            {
                yield return
                  new ValidationResult(errorMessage: "Zapisy nie mogą skończyć się po turnieju",
                                       memberNames: new[] { "Date", "Deadline" });
            }
            if (Date < DateTime.Now)
            {
                yield return
                  new ValidationResult(errorMessage: "Turniej nie może rozpocząć się w przeszłości",
                                       memberNames: new[] { "Date" });
            }
            if (Deadline < DateTime.Now)
            {
                yield return
                  new ValidationResult(errorMessage: "Nie można skończyć zapisów w przeszłości",
                                       memberNames: new[] { "Deadline" });
            }
        }

        [DisplayName("Organizator")]
        public virtual ApplicationUser Organizer { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [DisplayName("Logo")]
        public virtual ICollection<Logo> Logos { get; set; }
        [DisplayName("Dyscyplina")]
        public virtual Discipline Discipline { get; set; }
        [Required(ErrorMessage = "Wybierz dyscyplinę")]
        public int DisciplineId { get; set; }
        public virtual ICollection<Battle> Battles { get; set; }


        public Tournament() {
            this.Enrollments = new List<Enrollment>();
            //this.Logos = new HashSet<Logo>();
            //this.Battles = new HashSet<Battle>();
            //this.ApplicationUsers = new HashSet<ApplicationUser>();
        }
    }
}