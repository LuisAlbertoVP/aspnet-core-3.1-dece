using System.Collections.Generic;


namespace ApiDECE.Models.DTO.Seguridad
{
    public class Modulo 
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public byte[] Imagen { get; set; }

        public int Orden { get; set; }

        public string Color { get; set; }

        public string Estado { get; set; }

        public List<Opcion> Opciones { get; set; }
    }
}
