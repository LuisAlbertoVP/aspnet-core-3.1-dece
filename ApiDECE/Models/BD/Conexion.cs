using System;
using System.Data.SqlClient;

namespace ApiDECE.Models.BD
{
    public class Conexion
    {
        public string Url { get; set; }
        private static Conexion instance;
        private readonly SqlConnection cnn;

        private Conexion() {
            Url = @"Data Source=WIN-0LL5QMNU2HE\BACKCORESA; Initial Catalog = DECE; User ID = sa; Password = 123;";
            cnn = new SqlConnection(Url);
            try
            {
                cnn.Open();
            }catch(Exception)
            {
                cnn.Close();
            }
        }

        public static Conexion GetInstance()
        {
            if (instance == null)
                instance = new Conexion();
            return instance;
        }

        public SqlConnection GetConnection()
        {
            return cnn;
        }
    }
}
