using System.ComponentModel.DataAnnotations;
using WebAppDECE.Models.Seguridad;


namespace WebAppDECE.Models.ViewModel
{
    public class ActualizarRol
    {
        public ActualizarRol() { }

        public ActualizarRol(Rol rol)
        {
            Id = rol.Id;
            Descripcion = rol.Descripcion;
            IdEmpresa = rol.Empresa.Id;
        }


        [Required(ErrorMessage = "Identificador es requerido!")]
        public int? Id { get; set; }


        [Required(ErrorMessage = "Descripcion es requerida!")]
        [StringLength(100, ErrorMessage = "La maxima longitud de caracteres es 100!")]
        public string Descripcion { get; set; }


        [Required(ErrorMessage = "Empresa es requerida!")]
        public int? IdEmpresa { get; set; }
    }
}

