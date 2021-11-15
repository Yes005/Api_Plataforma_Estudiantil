using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plataforma_Estudiantil.DTOs
{
    public class TareaDto
    {
        public int idTareas { get; set; }
        public string titulo { get; set; }
        public string idMateria { get; set; }
        public string idDocente { get; set; }
        public string fecha { get; set; }
        public string descripcion { get; set; }
    
    }
}