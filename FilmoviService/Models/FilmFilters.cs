using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmoviService.Models
{
    public class FilmFilters
    {
        public int? godinaOd { get; set; }
        public int? godinaDo { get; set; }
        public string genre { get; set; }
    }
}