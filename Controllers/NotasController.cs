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
    [RoutePrefix("api/Notas")]
    public class NotasController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        private static readonly Expression<Func<Nota, NotasDto>> AsNotasDto =
             x => new NotasDto
             {
                 nota1 = (double)x.nota1,
                 nota2 = (double)x.nota2,
                 nota3 = (double)x.nota3,
                 nota4 = (double)x.nota4,
                 alumno = x.alumno.grado.grado1,
                 asignatura = x.Asignatura.nombre,
             };

        // GET: api/Notas
        [Route("")]
        public IQueryable<NotasDto> GetNotas()
        {
            return db.Notas.Include(b => b.alumno).Select(AsNotasDto);
        }

        // GET: api/Notas/5
        [Route("{id:int}")]
        [ResponseType(typeof(NotasDto))]
        public async Task<IHttpActionResult> GetNotas(int id)
        {
            NotasDto notas = await db.Notas.Include(b => b.alumno)
                .Where(b => b.idAlumno == id)
                .Select(AsNotasDto)
                .FirstOrDefaultAsync();
            if (notas == null)
            {
                return NotFound();
            }

            return Ok(notas);
        }

       
        // PUT: api/Notas/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNotas(int id, Nota notas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notas.idNotas)
            {
                return BadRequest();
            }

            db.Entry(notas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotasExists(id))
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

        // POST: api/Notas
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(Nota))]
        public IHttpActionResult PostNotas(Nota notas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notas.Add(notas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = notas.idNotas }, notas);
        }

        // DELETE: api/Notas/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(Nota))]
        public IHttpActionResult DeleteNotas(int id)
        {
            Nota notas = db.Notas.Find(id);
            if (notas == null)
            {
                return NotFound();
            }

            db.Notas.Remove(notas);
            db.SaveChanges();

            return Ok(notas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotasExists(int id)
        {
            return db.Notas.Count(e => e.idNotas == id) > 0;
        }
    }
}