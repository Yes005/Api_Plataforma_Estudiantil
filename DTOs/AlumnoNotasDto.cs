using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plataforma_Estudiantil.DTOs
{
    public class AlumnoNotasDto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public double nota1 { get; set; }
        public double nota2 { get; set; }
        public double nota3 { get; set; }
        public double nota4 { get; set; }

    }
}