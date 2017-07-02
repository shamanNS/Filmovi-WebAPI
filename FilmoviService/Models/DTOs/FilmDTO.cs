using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmoviService.Models.DTOs
{
    public class FilmDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Zanr { get; set; }
        public int Godina { get; set; }
        public int ReziserId { get; set; }
        public string ReziserIme { get; set; }
        public string ReziserPrezime { get; set; }
    }
}