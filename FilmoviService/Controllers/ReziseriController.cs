using System;
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

namespace FilmoviService.Controllers
{
    public class ReziseriController : ApiController
    {
        private FilmoviServiceContext db = new FilmoviServiceContext();

        // GET: api/Reziseri
        public IQueryable<Reziser> GetReziseri()
        {
            return db.Reziseri;
        }

        // GET: api/Reziseri/5
        [ResponseType(typeof(Reziser))]
        public IHttpActionResult GetReziser(int id)
        {
            Reziser reziser = db.Reziseri.Find(id);
            if (reziser == null)
            {
                return NotFound();
            }

            return Ok(reziser);
        }

        // PUT: api/Reziseri/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReziser(int id, Reziser reziser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reziser.Id)
            {
                return BadRequest();
            }

            db.Entry(reziser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReziserExists(id))
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

        // POST: api/Reziseri
        [ResponseType(typeof(Reziser))]
        public IHttpActionResult PostReziser(Reziser reziser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reziseri.Add(reziser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reziser.Id }, reziser);
        }

        // DELETE: api/Reziseri/5
        [ResponseType(typeof(Reziser))]
        public IHttpActionResult DeleteReziser(int id)
        {
            Reziser reziser = db.Reziseri.Find(id);
            if (reziser == null)
            {
                return NotFound();
            }

            db.Reziseri.Remove(reziser);
            db.SaveChanges();

            return Ok(reziser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReziserExists(int id)
        {
            return db.Reziseri.Count(e => e.Id == id) > 0;
        }
    }
}