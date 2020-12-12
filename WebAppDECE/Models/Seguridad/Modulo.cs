using System.Collections.Generic;


namespace WebAppDECE.Models.Seguridad
{
    public class Modulo
    {
        public Modulo() { }

        public Modulo(int id) => Id = id;


        public int? Id { get; set; }

        public string Descripcion { get; set; }

        public byte[] Imagen { get; set; }

        public int? Orden { get; set; }

        public string Color { get; set; }

        public string Estado { get; set; }

        public List<Opcion> Opciones { get; set; }
    }
}
