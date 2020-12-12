using System;


namespace WebAppDECE.Models.Seguridad
{
    interface IAcceso
    {
        public string UsuarioIngreso { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
