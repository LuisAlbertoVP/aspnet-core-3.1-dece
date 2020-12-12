using System;
using System.Collections.Generic;
using WebAppDECE.Models.ViewModel;


namespace WebAppDECE.Models.Seguridad
{
    public class Rol : IAcceso
    {
        public Rol() { }

        public Rol(int id) => Id = id;

        public Rol(ActualizarRol viewModel)
        {
            Id = viewModel.Id;
            Descripcion = viewModel.Descripcion;
            Empresa = new Empresa { Id = viewModel.IdEmpresa };
            Estado = "1";
            EstadoTabla = 1;
        }


        public int? Id { get; set; }

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
