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
    [RoutePrefix("api/tipoUsuarios")]
    public class tipoUsuariosController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        // GET: api/tipoUsuarios
        [Route("")]
        public IQueryable<tipoUsuario> GettipoUsuario()
        {
            return db.tipoUsuarios;
        }

        // GET: api/tipoUsuarios/5
        [Route("{id:int}")]
        [ResponseType(typeof(tipoUsuario))]
        public IHttpActionResult GettipoUsuario(int id)
        {
            tipoUsuario tipoUsuario = db.tipoUsuarios.Find(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return Ok(tipoUsuario);
        }

        // PUT: api/tipoUsuarios/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttipoUsuario(int id, tipoUsuario tipoUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoUsuario.id_tipo)
            {
                return BadRequest();
            }

            db.Entry(tipoUsuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipoUsuarioExists(id))
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

        // POST: api/tipoUsuarios
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(tipoUsuario))]
        public IHttpActionResult PosttipoUsuario(tipoUsuario tipoUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipoUsuarios.Add(tipoUsuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoUsuario.id_tipo }, tipoUsuario);
        }

        // DELETE: api/tipoUsuarios/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(tipoUsuario))]
        public IHttpActionResult DeletetipoUsuario(int id)
        {
            tipoUsuario tipoUsuario = db.tipoUsuarios.Find(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            db.tipoUsuarios.Remove(tipoUsuario);
            db.SaveChanges();

            return Ok(tipoUsuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipoUsuarioExists(int id)
        {
            return db.tipoUsuarios.Count(e => e.id_tipo == id) > 0;
        }
    }
}