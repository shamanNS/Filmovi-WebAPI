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

        public void Update(Film film)
        {
            //db.Filmovi.Attach(film);
            //db.Entry(film).Reference(f => f.Reziser).Load();
            //db.Entry(film).State = EntityState.Modified;
            //db.Filmovi.Find(film.Id);
            db.Entry(film).State = EntityState.Modified;
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


            if (filter.godinaOd != null)
            {
                filmovi = filmovi.Where( f => f.Godina >= filter.godinaOd);
            }
            if (filter.godinaDo != null)
            {
                filmovi = filmovi.Where(f => f.Godina <= filter.godinaDo);
            }
            return filmovi.AsEnumerable<Film>();
        }

        public Film GetById(int id)
        {
            return db.Filmovi/*.AsNoTracking()*/.Include(f => f.Reziser).SingleOrDefault(f => f.Id == id);
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