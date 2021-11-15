using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plataforma_Estudiantil.DTOs
{
    public class loginDto
    {
        public int idA { get; set; }
        public int idD { get; set; }
        public int idAd { get; set; }
        public string correoA { get; set; }
        public string correoD { get; set; }
        public string correoAd { get; set; }

        public string passA { get; set; }
        public string passD { get; set; }
        public string passAd { get; set; }
    }
}