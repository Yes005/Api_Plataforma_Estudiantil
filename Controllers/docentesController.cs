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
    [RoutePrefix("api/docentes")]
    public class docentesController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        private static readonly Expression<Func<docente, DocenteDto>> AsDocenteDto =
             x => new DocenteDto
             {
                 nombre = x.nombre,
                 apellido = x.apellido,
                 correo = x.correo,
                 password = x.password,
                 direccion = x.direccion,
                 telefono = x.telefono,
                 dui = x.dui,
                 asignatura = x.Asignatura.nombre,
                 tipo = x.tipoUsuario.descripcion,
             };

        // GET: api/docentes
        [Route("")]
        public IQueryable<DocenteDto> Getdocente()
        {
            return db.docentes.Include(b => b.Asignatura).Select(AsDocenteDto);
        }

        // GET: api/docentes/5
        [Route("{id:int}")]
        [ResponseType(typeof(docente))]
        public async Task<IHttpActionResult> Getdocente(int id)
        {
            DocenteDto docente = await db.docentes.Include(b => b.Asignatura)
                .Where(b => b.idDocente == id)
                .Select(AsDocenteDto)
                .FirstOrDefaultAsync();
            if (docente == null)
            {
                return NotFound();
            }

            return Ok(docente);
        }

        // PUT: api/docentes/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdocente(int id, docente docente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != docente.idDocente)
            {
                return BadRequest();
            }

            db.Entry(docente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!docenteExists(id))
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

        // POST: api/docentes
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(docente))]
        public IHttpActionResult Postdocente(docente docente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.docentes.Add(docente);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = docente.idDocente }, docente);
        }

        // DELETE: api/docentes/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(docente))]
        public IHttpActionResult Deletedocente(int id)
        {
            docente docente = db.docentes.Find(id);
            if (docente == null)
            {
                return NotFound();
            }

            db.docentes.Remove(docente);
            db.SaveChanges();

            return Ok(docente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool docenteExists(int id)
        {
            return db.docentes.Count(e => e.idDocente == id) > 0;
        }
    }
}