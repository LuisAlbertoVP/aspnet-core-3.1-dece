using System;
using ApiDECE.Models.DTO.Seguridad;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ApiDECE.Models.BD
{
    public class EmpresaDAO
    {
        private readonly Conexion instance = Conexion.GetInstance();
        private readonly SqlConnection cnn;

        public EmpresaDAO()
        {
            cnn = instance.GetConnection();
        }


        public List<Empresa> GetEmpresas(int id)
        {
            var empresas = new List<Empresa>();
            try
            {
                var command = new SqlCommand("EXEC dbo.spEmpresaQuery @id", cnn);
                command.Parameters.AddWithValue("@id", id);
                using (var oReader = command.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        var empresa = new Empresa
                        {
                            Id = int.Parse(oReader["IdEmpresa"].ToString()),
                            Nombre = oReader["Nombre"].ToString(),
                        };
                        empresas.Add(empresa);
                    }
                }
                return empresas;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

