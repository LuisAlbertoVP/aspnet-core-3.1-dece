using System;
using Microsoft.AspNetCore.Mvc;


namespace WebAppDECE.Models.Utilidades
{
    public class RequestException : Exception
    {
        public IActionResult Result { get; set; }


        public RequestException(string message) : base(message) { }


        public RequestException Handle(HandleError handle)
        {
            //Agregar aqui nuevo codigo de estado
            Result = Message switch
            {
                "Unauthorized"  =>  handle.Handle.StatusCode(401),
                "Forbidden"     =>  handle.Handle.StatusCode(403),
                "NotFound"      =>  handle.Handle.StatusCode(404),
                _               =>  handle.Handle.Redirect("/Error/Index"),
            };
            return this;
        }

    }
}
