using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plataforma_Estudiantil.DTOs
{
    public class NotasDto
    {
        public int idNotas { get; set; }
        public double nota1 { get; set; }
        public double nota2 { get; set; }
        public double nota3 { get; set; }
        public double nota4 { get; set; }
        public string alumno { get; set; }
        public string asignatura { get; set; }
    }
}