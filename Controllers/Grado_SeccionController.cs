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
    [RoutePrefix("api/Grado_Seccion")]
    public class Grado_SeccionController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        // GET: api/Grado_Seccion
        [Route("")]
        public IQueryable<Grado_Seccion> GetGrado_Seccion()
        {
            return db.Grado_Seccion;
        }

        // GET: api/Grado_Seccion/5
        [Route("{id:int}")]
        [ResponseType(typeof(Grado_Seccion))]
        public IHttpActionResult GetGrado_Seccion(int id)
        {
            Grado_Seccion grado_Seccion = db.Grado_Seccion.Find(id);
            if (grado_Seccion == null)
            {
                return NotFound();
            }

            return Ok(grado_Seccion);
        }

        // PUT: api/Grado_Seccion/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGrado_Seccion(int id, Grado_Seccion grado_Seccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grado_Seccion.id)
            {
                return BadRequest();
            }

            db.Entry(grado_Seccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Grado_SeccionExists(id))
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

        // POST: api/Grado_Seccion
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(Grado_Seccion))]
        public IHttpActionResult PostGrado_Seccion(Grado_Seccion grado_Seccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Grado_Seccion.Add(grado_Seccion);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Grado_SeccionExists(grado_Seccion.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = grado_Seccion.id }, grado_Seccion);
        }

        // DELETE: api/Grado_Seccion/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(Grado_Seccion))]
        public IHttpActionResult DeleteGrado_Seccion(int id)
        {
            Grado_Seccion grado_Seccion = db.Grado_Seccion.Find(id);
            if (grado_Seccion == null)
            {
                return NotFound();
            }

            db.Grado_Seccion.Remove(grado_Seccion);
            db.SaveChanges();

            return Ok(grado_Seccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Grado_SeccionExists(int id)
        {
            return db.Grado_Seccion.Count(e => e.id == id) > 0;
        }
    }
}