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
    [RoutePrefix("api/Notas_Periodos")]
    public class Notas_PeriodosController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        // GET: api/Notas_Periodos
        [Route("")]
        public IQueryable<Notas_Periodos> GetNotas_Periodos()
        {
            return db.Notas_Periodos;
        }

        // GET: api/Notas_Periodos/5
        [Route("{id:int}")]
        [ResponseType(typeof(Notas_Periodos))]
        public IHttpActionResult GetNotas_Periodos(int id)
        {
            Notas_Periodos notas_Periodos = db.Notas_Periodos.Find(id);
            if (notas_Periodos == null)
            {
                return NotFound();
            }

            return Ok(notas_Periodos);
        }

        // PUT: api/Notas_Periodos/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNotas_Periodos(int id, Notas_Periodos notas_Periodos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notas_Periodos.id)
            {
                return BadRequest();
            }

            db.Entry(notas_Periodos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Notas_PeriodosExists(id))
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

        // POST: api/Notas_Periodos
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(Notas_Periodos))]
        public IHttpActionResult PostNotas_Periodos(Notas_Periodos notas_Periodos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notas_Periodos.Add(notas_Periodos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Notas_PeriodosExists(notas_Periodos.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = notas_Periodos.id }, notas_Periodos);
        }

        // DELETE: api/Notas_Periodos/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(Notas_Periodos))]
        public IHttpActionResult DeleteNotas_Periodos(int id)
        {
            Notas_Periodos notas_Periodos = db.Notas_Periodos.Find(id);
            if (notas_Periodos == null)
            {
                return NotFound();
            }

            db.Notas_Periodos.Remove(notas_Periodos);
            db.SaveChanges();

            return Ok(notas_Periodos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Notas_PeriodosExists(int id)
        {
            return db.Notas_Periodos.Count(e => e.id == id) > 0;
        }
    }
}