using System;
using System.ComponentModel.DataAnnotations;
using WebAppDECE.Models.Seguridad;
using WebAppDECE.Attributes;


namespace WebAppDECE.Models.ViewModel
{
    public class ActualizarUsuario
    {
        public ActualizarUsuario() { }

        public ActualizarUsuario(Usuario user)
        {
            Id = user.Id;
            Clave = user.Clave;
            ConfirmClave = user.Clave;
            NombreCompleto = user.NombreCompleto;
            FechaExpiracion = user.FechaExpiracion;
            IdEmpresa = user.Empresa.Id;
        }


        [Required(ErrorMessage = "Identificador es requerido!")]
        public string Id { get; set; }


        [Required(ErrorMessage = "Clave es requerida!")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "La longitud de caracteres es de 8 - 16!")]
        public string Clave { get; set; }


        [Compare("Clave", ErrorMessage = "Las claves no son iguales!")]
        public string ConfirmClave { get; set; }


        [Required(ErrorMessage = "El nombre es requerido!")]
        [StringLength(100, ErrorMessage = "La maxima longitud de caracteres es 100!")]
        [RegularExpression(@"^[^\d\W]+(\s[^\d\W]+)+$", ErrorMessage = "El nombre no es valido!")]
        public string NombreCompleto { get; set; }


        [DataType(DataType.Date)]
        [MyDate(ErrorMessage = "La fecha de expiracion debe ser superior a la de hoy!")]
        public DateTime? FechaExpiracion { get; set; }


        [Required(ErrorMessage = "Empresa es requerida!")]
        public int? IdEmpresa { get; set; }
    }
}
