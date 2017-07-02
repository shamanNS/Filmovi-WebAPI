using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmoviService.Repository.Interfaces;
using FilmoviService.Models;
using System.Data.Entity;

namespace FilmoviService.Repository
{
    public class FilmRepository : IFilmRepository, IDisposable
    {
        private FilmoviServiceContext db = new FilmoviServiceContext();

        public void Add(Film film)
        {
            db.Filmovi.Add(film);
            db.SaveChanges();
        }

        public void Delete(Film film)
        {
            db.Filmovi.Remove(film);
            db.SaveChanges();
        }

        public IEnumerable<Film> GetAll()
        {
            return db.Filmovi.Include(f => f.Reziser);
        }

        public IEnumerable<Film> GetAll(FilmFilters filter)
        {
            IQueryable<Film> filmovi = filmovi = db.Filmovi.Include(f => f.Reziser);

               if (!String.IsNullOrWhiteSpace(filter.genre))
            {
                filmovi = filmovi.Where(f => f.Zanr.ToLower().Contains(filter.genre.ToLower()));
            }

            //if (filter.godinaOd != null && filter.godinaDo != null)
            //{
            //    if (filter.godinaDo < filter.godinaOd)
            //    {
            //        return BadRequest("Godina DO ne može biti manja od godine OD.");
            //    }
           // }
            //
            if (filter.godinaOd != null)
            //if (!String.IsNullOrWhiteSpace(filter.godinaOd))
            {
                //int godinaStart = int.Parse(filter.godinaOd);
                filmovi = filmovi.Where( f => f.Godina >= filter.godinaOd);
            }
            if (filter.godinaDo != null)
            //if (!String.IsNullOrWhiteSpace(filter.godinaDo))
            {
                //int godinaEnd = int.Parse(filter.godinaDo);
                filmovi = filmovi.Where(f => f.Godina <= filter.godinaDo);
            }
            return filmovi.AsEnumerable<Film>();
        }

        public Film GetById(int id)
        {
            return db.Filmovi.Include(f => f.Reziser).SingleOrDefault(f => f.Id == id);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
    }

}