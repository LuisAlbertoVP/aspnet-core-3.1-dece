using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppDECE.Models.Seguridad;
using WebAppDECE.Models.Utilidades;
using System.Security.Claims;


namespace WebAppDECE.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        public HandleError Handle => new HandleError() { Handle = this };
        private string Token => HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Hash).Value;


        public async Task<IActionResult> GetMenu()
        {
            var id = HttpContext.User.FindFirst(claim => claim.Type == ClaimTypes.Email).Value;
            var request = new Request<List<List<Modulo>>>(Handle, "/Menu/GetMenu?id=" + id, Token);
            try
            {
                var listaModulos = await request.SendGet();
                return Json(listaModulos);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }
    }
}