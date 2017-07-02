using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmoviService.Models
{
    [Table("Filmovi")]
    public class Film
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        [Required]
        public string Zanr { get; set; }

        
        public int Godina { get; set; }

        [ForeignKey("Reziser")]
        public int ReziserId { get; set; }

        // public virtual Reziser Reziser { get; set; }
        public Reziser Reziser { get; set; }
    }
}