using System;
using System.Collections.Generic;
using Bc.Lite;


namespace ApiDECE.Models.DTO.Seguridad
{
    public class Usuario : EntityBase, IAcceso
    {
        public string Id { get; set; }

        public string Clave { get; set; }

        public string NombreCompleto { get; set; }

        public DateTime? FechaExpiracion { get; set; }

        public int EstadoTabla { get; set; }

        public string Estado { get; set; }

        public Empresa Empresa { get; set; }

        public List<Rol> Roles { get; set; }

        public string UsuarioIngreso { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
