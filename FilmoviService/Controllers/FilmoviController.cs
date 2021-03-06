﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FilmoviService.Models;
using FilmoviService.Models.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FilmoviService.Repository.Interfaces;
using FilmoviService.Repository;

namespace FilmoviService.Controllers
{
    public class FilmoviController : ApiController
    {
        //private FilmoviServiceContext db = new FilmoviServiceContext();
        private IFilmRepository _filmRepository;

        public FilmoviController(IFilmRepository repository)
        {
            _filmRepository = repository;
        }


        // GET: api/Filmovi
        [HttpGet]
        //public IHttpActionResult GetFilmovi(int? godinaOd = null, int? godinaDo = null, string genre = null)
        public IHttpActionResult GetFilmovi([FromUri] FilmFilters filter)
        {

            //var filmovi = _filmRepository.GetAll();
            IEnumerable<Film> filmovi = null;

            //if (filter != null)
            if (filter.godinaOd != null || filter.godinaDo != null || !String.IsNullOrWhiteSpace(filter.genre))
            {
                if (filter.godinaOd != null && filter.godinaDo != null)
                {
                    if (filter.godinaDo < filter.godinaOd)
                    {
                        return BadRequest("Godina DO ne može biti manja od godine OD.");
                    }

                }
                filmovi = _filmRepository.GetAll(filter);
            }
            else
            {
                filmovi = _filmRepository.GetAll();
            }
            #region filtriranje pre Repository-uma
            //var filmovi = db.Filmovi.Include(f => f.Reziser);

            /* if (!String.IsNullOrWhiteSpace(genre))
             {
                 filmovi = filmovi.Where(f => f.Zanr.ToLower().Contains(genre.ToLower()));
             }

             if (godinaOd != null && godinaDo != null)
             {
                 if (godinaDo < godinaOd)
                 {
                     return BadRequest("Godina DO ne može biti manja od godine OD.");
                 }
             }
             //
             if (godinaOd != null)
             //if (!String.IsNullOrWhiteSpace(godinaOd))
             {
                 //int godinaStart = int.Parse(godinaOd);
                 filmovi = filmovi.Where( f => f.Godina >= godinaOd);
             }
             if (godinaDo != null)
             //if (!String.IsNullOrWhiteSpace(godinaDo))
             {
                 //int godinaEnd = int.Parse(godinaDo);
                 filmovi = filmovi.Where(f => f.Godina <= godinaDo);
             }
             */
            #endregion

            var filmoviDTO = filmovi.OrderBy(f => f.Naziv).AsQueryable().ProjectTo<FilmDTO>();
            //List<FilmDTO> filmovi = db.Filmovi.Include(f => f.Reziser).Select(f=> new FilmDTO { Naziv = f.Naziv, ReziserIme = f.Reziser.Ime , ReziserPrezime = f.Reziser.Prezime}).ToList();
            //var filmovi = db.Filmovi.Include( f=> f.Reziser).ToList();
            return Ok(filmoviDTO);
        }

        // GET: api/Filmovi/5
        [ResponseType(typeof(Film))]
        public IHttpActionResult GetFilm(int id)
        {
            //Film film = db.Filmovi.Find(id);

            var film = _filmRepository.GetById(id);
            //Film film = db.Filmovi.Include(f => f.Reziser).SingleOrDefault(f => f.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            //return Ok(film);
            return Ok(Mapper.Map<FilmDTO>(film));
        }


        // PUT: api/Filmovi/5
        [ResponseType(typeof(void))]
        //public IHttpActionResult PutFilm(int id, Film film)
        public IHttpActionResult PutFilm(int id, FilmDTO filmDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != filmDTO.Id)
            {
                return BadRequest();
            }

            var film = Mapper.Map<Film>(filmDTO);

            //if (!FilmExists(id))
            //{
            //    return NotFound();
            //}

            //_filmRepository.Update(film);

            //return StatusCode(HttpStatusCode.NoContent);


            
                //db.Entry(film).State = EntityState.Modified;

                try
                {
                _filmRepository.Update(film);
                //db.SaveChanges();
            }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
              
        }

        // POST: api/Filmovi
        [ResponseType(typeof(FilmDTO))]
        //public IHttpActionResult PostFilm(Film film)
        public IHttpActionResult PostFilm(FilmDTO filmDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var film = Mapper.Map<Film>(filmDTO);
            _filmRepository.Add(film);

            //db.Filmovi.Add(film);
            //db.SaveChanges();

            //db.Entry(film).Reference(x => x.Reziser).Load();
            return CreatedAtRoute("DefaultApi", new { id = film.Id }, film);
        }

        // DELETE: api/Filmovi/5
        //[ResponseType(typeof(Film))]
        //public IHttpActionResult DeleteFilm(int id)
        //{
        //    Film film = db.Filmovi.Find(id);
        //    if (film == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Filmovi.Remove(film);
        //    db.SaveChanges();

        //    return Ok(film);
        //}


        [ResponseType(typeof(Film))]
        public IHttpActionResult DeleteFilm(int id)
        {
            Film film = _filmRepository.GetById(id);
            if (film == null)
            {
                return NotFound();
            }

            _filmRepository.Delete(film);

            return Ok(film);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool FilmExists(int id)
        {
            //return db.Filmovi.Count(e => e.Id == id) > 0;
            return _filmRepository.GetById(id) != null;
        }

    }
}