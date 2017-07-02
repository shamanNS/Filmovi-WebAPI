using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FilmoviService.Models;

namespace FilmoviService.Models
{
    public class FilmoviServiceContext : DbContext
    {
        public DbSet<Film> Filmovi { get; set; }
        public DbSet<Reziser> Reziseri { get; set; }

        public FilmoviServiceContext(): base("name=FilmoviServiceContext")
        {

        }
    }
}