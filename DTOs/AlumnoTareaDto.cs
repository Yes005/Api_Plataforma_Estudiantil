using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plataforma_Estudiantil.DTOs
{
    public class AlumnoTareaDto
    {


        public int id { get; set; }
        public string idAlumno { get; set; }
        public int idTareas { get; set; }
        public string titulo { get; set; }
        public string idMateria { get; set; }
        public string IdDocente { get; set; }
        public string fecha { get; set; }
        public string descripcion { get; set; }
    }
}