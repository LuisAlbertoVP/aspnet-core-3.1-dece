using System.Collections.Generic;


namespace WebAppDECE.Models.Seguridad
{
    public class Opcion
    {
        public Opcion() { }

        public Opcion(int id) => Id = id;


        public int? Id { get; set; }

        public string Descripcion { get; set; }

        public string DescripcionLarga { get; set; }

        public int? IdOpcionPadre { get; set; }

        public bool EsOpcion { get; set; }

        public byte[] Imagen { get; set; }

        public int? Orden { get; set; }

        public string OpcionEjecutable { get; set; }

        public string RutaDll { get; set; }

        public bool Disponible { get; set; }

        public string Abreviatura { get; set; }

        public string EditorOpcion { get; set; }

        public string Estado { get; set; }

        public Modulo Modulo { get; set; }

        public List<Actividad> Actividades { get; set; }
    }
}
