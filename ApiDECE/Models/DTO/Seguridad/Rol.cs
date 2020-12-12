using System;
using System.Collections.Generic;
using Bc.Lite;


namespace ApiDECE.Models.DTO.Seguridad
{
    public class Rol : EntityBase, IAcceso
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public int EstadoTabla { get; set; }

        public string Estado { get; set; }

        public Empresa Empresa { get; set; }

        public List<Modulo> Modulos { get; set; }

        public List<Opcion> Opciones { get; set; }

        public List<Actividad> Actividades { get; set; }

        public string UsuarioIngreso { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
