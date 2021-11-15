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
using Plataforma_Estudiantil.Models;

namespace Plataforma_Estudiantil.Controllers
{
    public class alumnoes2Controller : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        // GET: api/alumnoes2
        public IQueryable<alumno> Getalumnoes()
        {
            return db.alumnoes;
        }

        // GET: api/alumnoes2/5
        [ResponseType(typeof(alumno))]
        public IHttpActionResult Getalumno(int id)
        {
            alumno alumno = db.alumnoes.Find(id);
            if (alumno == null)
            {
                return NotFound();
            }

            return Ok(alumno);
        }

        // PUT: api/alumnoes2/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putalumno(int id, alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumno.idAlumno)
            {
                return BadRequest();
            }

            db.Entry(alumno).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!alumnoExists(id))
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

        // POST: api/alumnoes2
        [ResponseType(typeof(alumno))]
        public IHttpActionResult Postalumno(alumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.alumnoes.Add(alumno);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alumno.idAlumno }, alumno);
        }

        // DELETE: api/alumnoes2/5
        [ResponseType(typeof(alumno))]
        public IHttpActionResult Deletealumno(int id)
        {
            alumno alumno = db.alumnoes.Find(id);
            if (alumno == null)
            {
                return NotFound();
            }

            db.alumnoes.Remove(alumno);
            db.SaveChanges();

            return Ok(alumno);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool alumnoExists(int id)
        {
            return db.alumnoes.Count(e => e.idAlumno == id) > 0;
        }
    }
}