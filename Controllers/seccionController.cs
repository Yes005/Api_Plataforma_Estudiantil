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
    [RoutePrefix("api/seccion")]
    public class seccionController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        private static readonly Expression<Func<seccion, SeccionDto>> AsSeccionDto =
             x => new SeccionDto
             {
                 idSeccion = x.idSeccion,
                 seccion = x.seccion1,
             };

        // GET: api/seccion
        [Route("")]
        public IQueryable<SeccionDto> Getseccion()
        {
            return db.seccions.Select(AsSeccionDto);
        }

        // GET: api/seccion/5
        [Route("{id:int}")]
        [ResponseType(typeof(SeccionDto))]
        public async Task<IHttpActionResult> Getseccion(int id)
        {
            SeccionDto seccion = await db.seccions
                .Where(b => b.idSeccion == id)
                .Select(AsSeccionDto)
                .FirstOrDefaultAsync();
            if (seccion == null)
            {
                return NotFound();
            }

            return Ok(seccion);
        }

        // PUT: api/seccion/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putseccion(int id, seccion seccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != seccion.idSeccion)
            {
                return BadRequest();
            }

            db.Entry(seccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!seccionExists(id))
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

        // POST: api/seccion
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(seccion))]
        public IHttpActionResult Postseccion(seccion seccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.seccions.Add(seccion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = seccion.idSeccion }, seccion);
        }

        // DELETE: api/seccion/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(seccion))]
        public IHttpActionResult Deleteseccion(int id)
        {
            seccion seccion = db.seccions.Find(id);
            if (seccion == null)
            {
                return NotFound();
            }

            db.seccions.Remove(seccion);
            db.SaveChanges();

            return Ok(seccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool seccionExists(int id)
        {
            return db.seccions.Count(e => e.idSeccion == id) > 0;
        }
    }
}