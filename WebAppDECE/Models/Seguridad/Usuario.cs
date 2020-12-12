using System;
using System.Collections.Generic;
using WebAppDECE.Models.ViewModel;


namespace WebAppDECE.Models.Seguridad
{
    public class Usuario : IAcceso
    {
        public Usuario() { }

        public Usuario(string id) => Id = id;

        public Usuario(Login viewModel)
        {
            Id = viewModel.Id;
            Clave = viewModel.Clave;
        }

        public Usuario(ActualizarUsuario viewModel)
        {
            Id = viewModel.Id;
            Clave = viewModel.Clave;
            NombreCompleto = viewModel.NombreCompleto;
            FechaExpiracion = viewModel.FechaExpiracion;
            Empresa = new Empresa { Id = viewModel.IdEmpresa };
            Estado = "1";
            EstadoTabla = 1;
        }


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
