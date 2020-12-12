using System;
using ApiDECE.Models.DTO.Seguridad;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ApiDECE.Models.BD
{
    public class OpcionDAO
    {
        private readonly Conexion instance = Conexion.GetInstance();
        private readonly SqlConnection cnn;

        public OpcionDAO()
        {
            cnn = instance.GetConnection();
        }

        
        public List<Opcion> GetOpciones(int id)
        {
            var opciones = new List<Opcion>();
            try
            {
                var command = new SqlCommand("EXEC dbo.spOpcionQuery @id", cnn);
                command.Parameters.AddWithValue("@id", id);
                using var oReader = command.ExecuteReader();
                while (oReader.Read())
                {
                    var opcion = new Opcion
                    {
                        Id = int.Parse(oReader["IdOpcion"].ToString()),
                        DescripcionLarga = oReader["DescripcionLarga"].ToString(),
                        Modulo = new Modulo { Id = int.Parse(oReader["IdModulo"].ToString()) }
                    };
                    opciones.Add(opcion);
                }
                return opciones;
            }
            catch (Exception)
            {
                return null;
            }
        }  
    }
}
