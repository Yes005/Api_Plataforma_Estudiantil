using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plataforma_Estudiantil.DTOs
{
    public class AlumnoDto
    {
        public int idAlumno { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public string direccion { get; set; }
        public string nie { get; set; }
        public string grado { get; set; }
    }
}