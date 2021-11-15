using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plataforma_Estudiantil.DTOs
{
    public class DocenteDto
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public string telefono { get; set; }
        public string dui { get; set; }
        public string asignatura { get; set; }
        public string tipo { get; set; }
    }
}