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
    [RoutePrefix("api/grado")]
    public class gradoController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        private static readonly Expression<Func<grado, GradoDto>> AsGradoDto =
             x => new GradoDto
             {
                 idGrado = x.idGrado,
                 grado = x.grado1
             };

        // GET: api/grado
        [Route("")]
        public IQueryable<GradoDto> Getgrado()
        {
            return db.gradoes.Select(AsGradoDto);
        }

        // GET: api/grado/5
        [Route("{id:int}")]
        [ResponseType(typeof(GradoDto))]
        public async Task<IHttpActionResult> Getalumno(int id)
        {
            GradoDto grado = await db.gradoes
                .Where(b => b.idGrado == id)
                .Select(AsGradoDto)
                .FirstOrDefaultAsync();
            if (grado == null)
            {
                return NotFound();
            }

            return Ok(grado);
        }

        // PUT: api/grado/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgrado(int id, grado grado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != grado.idGrado)
            {
                return BadRequest();
            }

            db.Entry(grado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!gradoExists(id))
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

        // POST: api/grado
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(grado))]
        public IHttpActionResult Postgrado(grado grado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.gradoes.Add(grado);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = grado.idGrado }, grado);
        }

        // DELETE: api/grado/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(grado))]
        public IHttpActionResult Deletegrado(int id)
        {
            grado grado = db.gradoes.Find(id);
            if (grado == null)
            {
                return NotFound();
            }

            db.gradoes.Remove(grado);
            db.SaveChanges();

            return Ok(grado);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool gradoExists(int id)
        {
            return db.gradoes.Count(e => e.idGrado == id) > 0;
        }
    }
}