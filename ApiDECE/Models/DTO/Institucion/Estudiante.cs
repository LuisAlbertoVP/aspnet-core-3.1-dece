using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppDECE.Models.DTO.Institucion
{
    public class Estudiante : Persona
    {
        public string IdCurso { get; set; }

        public string IdParalelo { get; set; }

        public long NumCarnet { get; set; }
    }
}
