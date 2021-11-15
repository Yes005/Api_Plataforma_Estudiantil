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
    [RoutePrefix("api/dtarea")]
    public class Alumno_TareaController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        private static readonly Expression<Func<Alumno_Tarea, AlumnoTareaDto>> asAlumnoTareaDto =
            x => new AlumnoTareaDto
            {


                id = x.id,
                idAlumno = x.alumno.nombre,
                idTareas = x.tarea.idTareas,
                titulo = x.tarea.titulo,
                idMateria = x.tarea.docente.Asignatura.nombre,
                IdDocente = x.tarea.docente.nombre,
                fecha = x.tarea.fecha.ToString(),
                descripcion = x.tarea.descripcion,

            };

        [Route("")]
        // GET: api/Alumno_Tarea
        public IQueryable<AlumnoTareaDto> GetAlumno_Tarea()
        {
            return db.Alumno_Tarea.Include(b => b.id).Select(asAlumnoTareaDto);
        }

      

        [Route("materia/{id:int}")]
        public IQueryable<AlumnoTareaDto> GetMateria(int id)
        {
            return db.Alumno_Tarea.Include(b => b.id).Where(b => b.idTarea == id).Select(asAlumnoTareaDto);
        }


        [Route("alumno/{id:int}")]
        public IQueryable<AlumnoTareaDto> GetTareaid(int id)
        {
            return db.Alumno_Tarea.Include(b => b.id).Where(b => b.idTarea == id).Select(asAlumnoTareaDto);
        }


        // GET: api/Alumno_Tarea/5
        [ResponseType(typeof(Alumno_Tarea))]
        public async Task<IHttpActionResult> GetAlumno_Tarea(int id)
        {
            Alumno_Tarea alumno_Tarea = await db.Alumno_Tarea.FindAsync(id);
            if (alumno_Tarea == null)
            {
                return NotFound();
            }

            return Ok(alumno_Tarea);
        }

        // PUT: api/Alumno_Tarea/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlumno_Tarea(int id, Alumno_Tarea alumno_Tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumno_Tarea.id)
            {
                return BadRequest();
            }

            db.Entry(alumno_Tarea).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Alumno_TareaExists(id))
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

        // POST: api/Alumno_Tarea
        [ResponseType(typeof(Alumno_Tarea))]
        public async Task<IHttpActionResult> PostAlumno_Tarea(Alumno_Tarea alumno_Tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alumno_Tarea.Add(alumno_Tarea);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alumno_Tarea.id }, alumno_Tarea);
        }

        // DELETE: api/Alumno_Tarea/5
        [ResponseType(typeof(Alumno_Tarea))]
        public async Task<IHttpActionResult> DeleteAlumno_Tarea(int id)
        {
            Alumno_Tarea alumno_Tarea = await db.Alumno_Tarea.FindAsync(id);
            if (alumno_Tarea == null)
            {
                return NotFound();
            }

            db.Alumno_Tarea.Remove(alumno_Tarea);
            await db.SaveChangesAsync();

            return Ok(alumno_Tarea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Alumno_TareaExists(int id)
        {
            return db.Alumno_Tarea.Count(e => e.id == id) > 0;
        }
    }
}