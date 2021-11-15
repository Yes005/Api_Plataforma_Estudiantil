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
    [RoutePrefix("api/alumno")]
    public class alumnoController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        private static readonly Expression<Func<alumno, AlumnoDto>> AsAlumnoDto =
              x => new AlumnoDto
              {
                  idAlumno = x.idAlumno,
                  nombre = x.nombre,
                  apellido = x.apellido,
                  correo = x.correo,
                  password = x.password,
                  direccion = x.direccion,
                  nie = x.nie,
                  grado = x.grado.grado1,
              };

        

        // GET: api/alumno
        [Route("")]
        public IQueryable<AlumnoDto> Getalumno()
        {
            return db.alumnoes.Include(b => b.grado).Select(AsAlumnoDto);
        }

        [Route("git/{id:int}")]
        public IQueryable<AlumnoDto> Getalumno2(int id)
        {
            return db.alumnoes.Include(b => b.grado).Where(b => b.idAlumno == id).Select(AsAlumnoDto);
        }


        // GET: api/alumno/5
        [Route("{id:int}")]
        [ResponseType(typeof(AlumnoDto))]
        public async Task<IHttpActionResult> Getalumno(int id)
        {
            AlumnoDto alumno = await db.alumnoes.Include(b => b.grado)
                .Where(b => b.idAlumno == id)
                .Select(AsAlumnoDto)
                .FirstAsync();
            if (alumno == null)
            {
                return NotFound();
            }

            return Ok(alumno);
        }
        

        // PUT: api/alumno/5
        [Route("{id:int}")]
        [HttpPut]
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

        // POST: api/alumno
        [HttpPost]
        [Route("")]
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

        // DELETE: api/alumno/5
        [Route("{id:int}")]
        [HttpDelete]
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