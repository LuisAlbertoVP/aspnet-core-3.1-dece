using System;


namespace WebAppDECE.Models.Utilidades
{
    public class Token
    {
        public string Id { get; set; }

        public string Nombres { get; set; }

        public string Key { get; set; }

        public DateTime Expiration { get; set; }
    }
}
