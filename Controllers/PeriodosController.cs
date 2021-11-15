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
    [RoutePrefix("api/Periodos")]
    public class PeriodosController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        private static readonly Expression<Func<Periodo, PeriodosDto>> AsPeriodoDto =
             x => new PeriodosDto
             {
                 idPeriodo = x.idPeriodos,
                 periodo = (int)x.periodo1
             };

        // GET: api/Periodos
        [Route("")]
        public IQueryable<PeriodosDto> GetPeriodos()
        {
            return db.Periodos.Select(AsPeriodoDto);
        }

        // GET: api/Periodos/5
        [Route("{id:int}")]
        [ResponseType(typeof(PeriodosDto))]
        public async Task<IHttpActionResult> GetPeriodos(int id)
        {
            PeriodosDto periodos = await db.Periodos
                .Where(b => b.idPeriodos == id)
                .Select(AsPeriodoDto)
                .FirstOrDefaultAsync();
            if (periodos == null)
            {
                return NotFound();
            }

            return Ok(periodos);
        }

        // PUT: api/Periodos/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPeriodos(int id, Periodo periodos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != periodos.idPeriodos)
            {
                return BadRequest();
            }

            db.Entry(periodos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodosExists(id))
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

        // POST: api/Periodos
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(Periodo))]
        public IHttpActionResult PostPeriodos(Periodo periodos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Periodos.Add(periodos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = periodos.idPeriodos }, periodos);
        }

        // DELETE: api/Periodos/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(Periodo))]
        public IHttpActionResult DeletePeriodos(int id)
        {
            Periodo periodos = db.Periodos.Find(id);
            if (periodos == null)
            {
                return NotFound();
            }

            db.Periodos.Remove(periodos);
            db.SaveChanges();

            return Ok(periodos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PeriodosExists(int id)
        {
            return db.Periodos.Count(e => e.idPeriodos == id) > 0;
        }
    }
}