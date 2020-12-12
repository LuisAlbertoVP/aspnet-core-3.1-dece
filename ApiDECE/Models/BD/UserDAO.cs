using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ApiDECE.Models.DTO.Seguridad;


namespace ApiDECE.Models.BD
{
    public class UserDAO
    {
        private readonly Conexion instance = Conexion.GetInstance();
        private readonly SqlConnection cnn;

        public UserDAO()
        {
            cnn = instance.GetConnection();
        }

        public Usuario Login(Usuario user)
        {
            try 
            { 
                SqlCommand command = new SqlCommand("EXEC dbo.spLoginUsuario @id, @clave", cnn);
                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@clave", user.Clave);
                using SqlDataReader oReader = command.ExecuteReader();
                while (oReader.Read())
                {
                    user.NombreCompleto = oReader["NombreCompleto"].ToString();
                    user.Estado = oReader["Estado"].ToString();
                    user.Empresa = new Empresa { Id = int.Parse(oReader["IdEmpresa"].ToString()) };
                }
                if (!string.IsNullOrEmpty(user.Estado))
                {
                    return user;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Insert(Usuario user)
        {
            try
            {
                Bc.CurrentDataBase.DataBase = Bc.DataBaseType.SqlServer;
                SqlCommand command = new SqlCommand("EXEC dbo.spUsuarioInsert @xml", cnn);
                command.Parameters.AddWithValue("@xml", user.ToXml());
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Usuario user)
        {
            try
            {
                Bc.CurrentDataBase.DataBase = Bc.DataBaseType.SqlServer;
                SqlCommand command = new SqlCommand("EXEC dbo.spUsuarioUpdate @xml", cnn);
                command.Parameters.AddWithValue("@xml", user.ToXml());
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool FindById(string id)
        {
            try
            {
                SqlCommand command = new SqlCommand("EXEC dbo.spUsuarioQueryId @id", cnn);
                command.Parameters.AddWithValue("@id", id);
                return ((int)command.ExecuteScalar()) == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Usuario> GetUsuarios(string id)
        {
            var users = new List<Usuario>();
            try
            {
                var command = new SqlCommand("EXEC dbo.spUsuarioQuery @id", cnn);
                command.Parameters.AddWithValue("@id", id);
                using (var oReader = command.ExecuteReader()) {
                    while (oReader.Read())
                    {
                        var user = new Usuario
                        {
                            NombreCompleto = oReader["NombreCompleto"].ToString(),
                            Id = oReader["IdUsuario"].ToString(),
                            FechaExpiracion = Parse(oReader["FechaExpiracion"].ToString()),
                            Estado = oReader["Estado"].ToString(),
                            UsuarioIngreso = oReader["UsrIng"].ToString(),
                            FechaIngreso = Parse(oReader["FecIng"].ToString()),
                            UsuarioModificacion = oReader["UsrMod"].ToString(),
                            FechaModificacion = Parse(oReader["FecMod"].ToString()),
                            Empresa = new Empresa() {
                                Id = int.Parse(oReader["IdEmpresa"].ToString()),
                                Nombre = oReader["Nombre"].ToString()
                            }
                        };
                        users.Add(user);
                    }
                }
                return users;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Rol> GetRolesUsuario(string user)
        {
            var roles = new List<Rol>();
            try
            {
                var command = new SqlCommand("EXEC dbo.spRolesUsuarioQuery @user", cnn);
                command.Parameters.AddWithValue("@user", user);
                using var oReader = command.ExecuteReader();
                while (oReader.Read())
                {
                    roles.Add(new Rol
                    {
                        Id = int.Parse(oReader["IdRol"].ToString()),
                        Descripcion = oReader["Descripcion"].ToString()
                    });
                }
                return roles;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Enabled(Usuario user)
        {
            try
            {
                SqlCommand command = new SqlCommand("EXEC dbo.spUsuarioEnabled @xml", cnn);
                command.Parameters.AddWithValue("@xml", user.ToXml());
                return ((int)command.ExecuteScalar()) == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Disabled(Usuario user)
        {
            try
            {
                SqlCommand command = new SqlCommand("EXEC dbo.spUsuarioDisabled @xml", cnn);
                command.Parameters.AddWithValue("@xml", user.ToXml());
                return ((int)command.ExecuteScalar()) == 1;
            }
            catch (Exception)
            {
                return false;
            }
        } 

        public bool RolesUsuarioInsert(Usuario user)
        {
            SqlTransaction transaction = null;
            try
            {
                SqlCommand command = new SqlCommand("EXEC dbo.spRolesUsuarioDelete @user", cnn);
                command.Parameters.AddWithValue("@user", user.Id);
                using (SqlDataReader oReader = command.ExecuteReader()){
                    while (oReader.Read())
                    {
                        user.Empresa = new Empresa { Id = int.Parse(oReader["IdEmpresa"].ToString()) };
                    }
                }
                transaction = cnn.BeginTransaction();
                command = new SqlCommand("EXEC dbo.spRolesUsuarioInsert @xml", cnn, transaction);
                command.Parameters.AddWithValue("@xml", user.ToXml());
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if(transaction != null)
                {
                    transaction.Rollback();
                }
                return false;
            }
        }

        private DateTime? Parse(string date)
        {
            if(date.Length > 0)
            {
                return DateTime.Parse(date);
            }
            return null;
        }
    }
}
