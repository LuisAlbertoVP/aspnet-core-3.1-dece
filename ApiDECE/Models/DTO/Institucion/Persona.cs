using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppDECE.Models.DTO.Institucion
{
    public class Persona
    {
       public long IdPersona { get; set; }

        public string Cedula { get; set; }
        
       public string Nombres { get; set; }

       public string Apellidos { get; set; }

       public DateTime FechaNacimiento { get; set; }

       public int Edad { get { return (DateTime.Now - FechaNacimiento).Days / 365; } }

       public string Direccion { get; set; }

       public string EstadoCivil { get; set; }

       public string TipoSangre { get; set; }

       public string Genero { get; set; }

    }
}
