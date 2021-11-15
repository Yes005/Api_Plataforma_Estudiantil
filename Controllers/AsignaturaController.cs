using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Plataforma_Estudiantil.DTOs;
using Plataforma_Estudiantil.Models;

namespace Plataforma_Estudiantil.Controllers
{
    [RoutePrefix("api/Asignatura")]
    public class AsignaturaController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        private static readonly Expression<Func<Asignatura, AsignaturaDto>> AsAsignaturaDto =
              x => new AsignaturaDto
              {
                  idAsignatura = x.idAsignatura,
                  nombre = x.nombre
              };

        // GET: api/Asignatura
        [Route("")]
        public IQueryable<AsignaturaDto> GetAsignatura()
        {
            return db.Asignaturas.Select(AsAsignaturaDto);
        }

        // GET: api/Asignatura/5
        [Route("{id:int}")]
        [ResponseType(typeof(AsignaturaDto))]
        public async Task<IHttpActionResult> Getasignaturas(int id)
        {
            AsignaturaDto asignatura = await db.Asignaturas
                .Where(b => b.idAsignatura == id)
                .Select(AsAsignaturaDto)
                .FirstOrDefaultAsync();
            if (asignatura == null)
            {
                return NotFound();
            }

            return Ok(asignatura);
        }

        // PUT: api/Asignatura/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsignatura(int id, Asignatura asignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asignatura.idAsignatura)
            {
                return BadRequest();
            }

            db.Entry(asignatura).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignaturaExists(id))
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

        // POST: api/Asignatura
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(Asignatura))]
        public IHttpActionResult PostAsignatura(Asignatura asignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asignaturas.Add(asignatura);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asignatura.idAsignatura }, asignatura);
        }

        // DELETE: api/Asignatura/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(Asignatura))]
        public IHttpActionResult DeleteAsignatura(int id)
        {
            Asignatura asignatura = db.Asignaturas.Find(id);
            if (asignatura == null)
            {
                return NotFound();
            }

            db.Asignaturas.Remove(asignatura);
            db.SaveChanges();

            return Ok(asignatura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AsignaturaExists(int id)
        {
            return db.Asignaturas.Count(e => e.idAsignatura == id) > 0;
        }
    }
}