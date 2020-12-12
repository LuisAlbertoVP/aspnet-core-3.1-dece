using System;
using ApiDECE.Models.DTO.Seguridad;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ApiDECE.Models.BD
{
    public class RolDAO
    {
        private readonly Conexion instance = Conexion.GetInstance();
        private readonly SqlConnection cnn;

        public RolDAO()
        {
            cnn = instance.GetConnection();
        }

        public bool Insert(Rol rol)
        {
            try
            {
                Bc.CurrentDataBase.DataBase = Bc.DataBaseType.SqlServer;
                SqlCommand command = new SqlCommand("EXEC dbo.spRolInsert @xml", cnn);
                command.Parameters.AddWithValue("@xml", rol.ToXml());
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Rol rol)
        {
            try
            {
                Bc.CurrentDataBase.DataBase = Bc.DataBaseType.SqlServer;
                SqlCommand command = new SqlCommand("EXEC dbo.spRolUpdate @xml", cnn);
                command.Parameters.AddWithValue("@xml", rol.ToXml());
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool FindById(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand("EXEC dbo.spRolQueryId @id", cnn);
                command.Parameters.AddWithValue("@id", id);
                return ((int)command.ExecuteScalar()) == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Rol> GetRoles(int id)
        {
            var roles = new List<Rol>();
            try
            {
                var command = new SqlCommand("EXEC dbo.spRolQuery @id", cnn);
                command.Parameters.AddWithValue("@id", id);
                using (var oReader = command.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        var rol = new Rol
                        {
                            Id = int.Parse(oReader["IdRol"].ToString()),
                            Descripcion = oReader["Descripcion"].ToString(),
                            Estado = oReader["Estado"].ToString(),
                            UsuarioIngreso = oReader["UsrIng"].ToString(),
                            FechaIngreso = Parse(oReader["FecIng"].ToString()),
                            UsuarioModificacion = oReader["UsrMod"].ToString(),
                            FechaModificacion = Parse(oReader["FecMod"].ToString()),
                            Empresa = new Empresa
                            {
                                Id = int.Parse(oReader["IdEmpresa"].ToString()),
                                Nombre = oReader["Nombre"].ToString()
                            }
                        };
                        roles.Add(rol);
                    }
                }
                return roles;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Modulo> GetMenuRol(Rol rol)
        {
            var modulos = new List<Modulo>();
            try
            {
                var command = new SqlCommand("EXEC dbo.spMenuModulosQuery @rol", cnn);
                command.Parameters.AddWithValue("@rol", rol.Id);
                using (var oReader = command.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        modulos.Add(new Modulo
                        {
                            Id = int.Parse(oReader["IdModulo"].ToString()),
                            Descripcion = oReader["Descripcion"].ToString()
                        });
                    }
                }
                foreach(var modulo in modulos)
                {
                    var opciones = new List<Opcion>();
                    command = new SqlCommand("EXEC dbo.spMenuOpcionesQuery @rol, @mod", cnn);
                    command.Parameters.AddWithValue("@rol", rol.Id);
                    command.Parameters.AddWithValue("@mod", modulo.Id);
                    using (var oReader = command.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            opciones.Add(new Opcion
                            {
                                Id = int.Parse(oReader["IdOpcion"].ToString()),
                                Descripcion = oReader["Descripcion"].ToString(),
                                DescripcionLarga = oReader["DescripcionLarga"].ToString(),
                                Orden = int.Parse(oReader["Orden"].ToString()),
                                RutaDll = oReader["RutaDll"].ToString()
                            });
                        }
                    };
                    modulo.Opciones = opciones;
                }
                return modulos;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Opcion> GetOpcionesRol(int rol)
        {
            var opciones = new List<Opcion>();
            try
            {
                var command = new SqlCommand("EXEC dbo.spOpcionesRolQuery @rol", cnn);
                command.Parameters.AddWithValue("@rol", rol);
                using var oReader = command.ExecuteReader();
                while (oReader.Read())
                {
                    opciones.Add(new Opcion
                    {
                        Id = int.Parse(oReader["IdOpcion"].ToString()),
                        DescripcionLarga = oReader["DescripcionLarga"].ToString(),
                        Modulo = new Modulo
                        {
                            Id = int.Parse(oReader["IdModulo"].ToString())
                        }                        
                    });
                }
                return opciones;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Opcion> GetActividadesOpcionRol(int rol)
        {
            var opciones = GetOpcionesRol(rol);
            try
            {
                foreach (var opcion in opciones)
                {
                    var actividades = new List<Actividad>();
                    var command = new SqlCommand("EXEC dbo.spActividadOpcionQuery @id", cnn);
                    command.Parameters.AddWithValue("@id", opcion.Id);
                    using var oReader = command.ExecuteReader();
                    while (oReader.Read())
                    {
                        var actv = new Actividad
                        {
                            NombreActividad = oReader["Actividad"].ToString(),
                            Descripcion = oReader["Descripcion"].ToString()
                        };
                        actividades.Add(actv);
                    }
                    opcion.Actividades = actividades;
                }
                return opciones;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Actividad> GetActividadesRol(int rol)
        {
            var actividades = new List<Actividad>();
            try
            {
                var command = new SqlCommand("EXEC dbo.spActividadesRolQuery @rol", cnn);
                command.Parameters.AddWithValue("@rol", rol);
                using var oReader = command.ExecuteReader();
                while (oReader.Read())
                {
                    actividades.Add(new Actividad
                    {
                        NombreActividad = oReader["Actividad"].ToString(),
                        Opcion = new Opcion
                        {
                            Id = int.Parse(oReader["IdOpcion"].ToString()),
                            Descripcion = oReader["Descripcion"].ToString(),
                            Modulo = new Modulo 
                            {
                                Id = int.Parse(oReader["IdModulo"].ToString())
                            }
                        }
                    });
                }
                return actividades;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Enabled(Rol rol)
        {
            try
            {
                SqlCommand command = new SqlCommand("EXEC dbo.spRolEnabled @xml", cnn);
                command.Parameters.AddWithValue("@xml", rol.ToXml());
                return ((int)command.ExecuteScalar()) == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Disabled(Rol rol)
        {
            try
            {
                SqlCommand command = new SqlCommand("EXEC dbo.spRolDisabled @xml", cnn);
                command.Parameters.AddWithValue("@xml", rol.ToXml());
                return ((int)command.ExecuteScalar()) == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool OpcionesRolInsert(Rol rol)
        {
            SqlTransaction transaction = null;
            try
            {
                SqlCommand command = new SqlCommand("EXEC dbo.spOpcionesRolDelete @rol", cnn);
                command.Parameters.AddWithValue("@rol", rol.Id);
                using (SqlDataReader oReader = command.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        rol.Empresa = new Empresa { Id = int.Parse(oReader["IdEmpresa"].ToString()) };
                    }
                }
                transaction = cnn.BeginTransaction();
                command = new SqlCommand("EXEC dbo.spOpcionesRolInsert @xml", cnn, transaction);               
                command.Parameters.AddWithValue("@xml", rol.ToXml());
                command.ExecuteNonQuery();        
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                return false;
            }
        }

        public bool ActividadesRolInsert(Rol rol)
        {
            SqlTransaction transaction = null;
            try
            {
                SqlCommand command = new SqlCommand("EXEC dbo.spActividadesRolDelete @rol", cnn);
                command.Parameters.AddWithValue("@rol", rol.Id);
                using (SqlDataReader oReader = command.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        rol.Empresa = new Empresa { Id = int.Parse(oReader["IdEmpresa"].ToString()) };
                    }
                }
                transaction = cnn.BeginTransaction();
                command = new SqlCommand("EXEC dbo.spActividadesRolInsert @xml", cnn, transaction);
                command.Parameters.AddWithValue("@xml", rol.ToXml());
                command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                return false;
            }
        }

        private DateTime? Parse(string date)
        {
            if (date.Length > 0)
            {
                return DateTime.Parse(date);
            }
            return null;
        }
    }
}
