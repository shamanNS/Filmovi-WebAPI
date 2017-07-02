using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmoviService.Models
{
    [Table("Reziseri")]
    public class Reziser
    {
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        public int Starost { get; set; }
        // public virtual List<Film> Filmovi { get; set; }
       // public List<Film> Filmovi { get; set; }
    }
}