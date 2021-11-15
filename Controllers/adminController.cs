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
    [RoutePrefix("api/admin")]
    public class adminController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        // GET: api/admin
        [Route("")]
        public IQueryable<admin> Getadmin()
        {
            return db.admins;
        }

        // GET: api/admin/5
        [Route("{id:int}")]
        [ResponseType(typeof(admin))]
        public IHttpActionResult Getadmin(int id)
        {
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        // PUT: api/admin/5
        [Route("{id:int}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putadmin(int id, admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admin.idAdmin)
            {
                return BadRequest();
            }

            db.Entry(admin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!adminExists(id))
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

        // POST: api/admin
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(admin))]
        public IHttpActionResult Postadmin(admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.admins.Add(admin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = admin.idAdmin }, admin);
        }

        // DELETE: api/admin/5
        [Route("{id:int}")]
        [HttpDelete]
        [ResponseType(typeof(admin))]
        public IHttpActionResult Deleteadmin(int id)
        {
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            db.admins.Remove(admin);
            db.SaveChanges();

            return Ok(admin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool adminExists(int id)
        {
            return db.admins.Count(e => e.idAdmin == id) > 0;
        }
    }
}