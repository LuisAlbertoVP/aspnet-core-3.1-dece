using System;
using System.Collections.Generic;
using Bc.Lite;


namespace ApiDECE.Models.DTO.Seguridad
{
    public class Empresa : EntityBase, IAcceso
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Ruc { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string RazonSocial { get; set; }

        public string Mail { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public List<Rol> Roles { get; set; }

        public string UsuarioIngreso { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
