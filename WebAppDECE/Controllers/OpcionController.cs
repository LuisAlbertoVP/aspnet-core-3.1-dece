using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppDECE.Models.Seguridad;
using WebAppDECE.Models.Utilidades;


namespace WebAppDECE.Controllers
{
    [Authorize]
    public class OpcionController : Controller
    {
        public HandleError Handle => new HandleError() { Handle = this };
        private string Token => HttpContext.User.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.Hash).Value;


        public async Task<IActionResult> GetOpciones(int id)
        {
            var request = new Request<List<Opcion>>(Handle, "/Opcion/GetOpciones?id=" + id, Token);
            try
            {
                var opciones = await request.SendGet();
                return Json(opciones);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }
    }
}