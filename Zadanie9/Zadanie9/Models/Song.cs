using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zadanie9.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(100, ErrorMessage = "Maximal length of the name of a song is 100 characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Artist is required!")]
        [StringLength(40, ErrorMessage = "Maximal length song artist is 40 characters!")]
        public string Artist { get; set; }
        public string GenreId { get; set; }

        public Song() { }
    }
}