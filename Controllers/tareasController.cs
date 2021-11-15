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
    [RoutePrefix("api/ptarea")]
    public class tareasController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();


        private static readonly Expression<Func<tarea, TareaDto>> asTareaDto =
              x => new TareaDto
              {
                  idTareas = x.idTareas,
                  titulo = x.titulo,
                  idMateria = x.idMateria.ToString(),
                  //idDocente = x.idDoc,
                  idDocente = x.idDocente.ToString(),
                  fecha = x.fecha.ToString(),
                  descripcion = x.descripcion,
                 
              };



        // GET: api/tareas
        [Route("")]
        public IQueryable<TareaDto> Gettareas()
        {
            return db.tareas.Include(b => b.idTareas).Select(asTareaDto);
        }

        // GET: api/tareas/5
        [ResponseType(typeof(tarea))]
        public async Task<IHttpActionResult> Gettarea(int id)
        {
            tarea tarea = await db.tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            return Ok(tarea);
        }

        // PUT: api/tareas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttarea(int id, tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarea.idTareas)
            {
                return BadRequest();
            }

            db.Entry(tarea).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tareaExists(id))
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

        // POST: api/tareas
        [ResponseType(typeof(tarea))]
        public async Task<IHttpActionResult> Posttarea(tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tareas.Add(tarea);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tarea.idTareas }, tarea);
        }

        // DELETE: api/tareas/5
        [ResponseType(typeof(tarea))]
        public async Task<IHttpActionResult> Deletetarea(int id)
        {
            tarea tarea = await db.tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            db.tareas.Remove(tarea);
            await db.SaveChangesAsync();

            return Ok(tarea);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tareaExists(int id)
        {
            return db.tareas.Count(e => e.idTareas == id) > 0;
        }
    }
}