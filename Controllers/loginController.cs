
using Plataforma_Estudiantil.DTOs;
using Plataforma_Estudiantil.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Plataforma_Estudiantil.Controllers
{
    [RoutePrefix("api/login")]
    public class loginController : ApiController
    {
        private dbEstudiantesEntities db = new dbEstudiantesEntities();

        private static readonly Expression<Func<alumno, loginDto>> AsAlumnoDto =
              x => new loginDto
              {
                  idA = x.idAlumno,
                  correoA = x.correo,
                  passA = x.password
              };

        [Route("")]
        public IQueryable<loginDto> Getalumno()
        {
            return db.alumnoes.Include(b => b.grado).Select(AsAlumnoDto);
        }


        [Route("{mail:alpha}")]
        [ResponseType(typeof(loginDto))]
        public async Task<IHttpActionResult> GetBookDetail(string mail)
        {
            var vari = await (from b in db.alumnoes.Include(path: b => b.grado)
                                   where (b.correo == mail)
                                   select new loginDto
                                   {
                                       idA = b.idAlumno,
                                       correoA = b.correo,
                                       passA = b.password
                                   }).FirstOrDefaultAsync();

            if (vari == null)
            {
                return NotFound();
            }
            return Ok(vari);
        }



    }
}
