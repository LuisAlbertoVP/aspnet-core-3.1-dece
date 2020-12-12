using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppDECE.Models.Seguridad;
using WebAppDECE.Models.Utilidades;


namespace WebAppDECE.Controllers
{
    [Authorize]
    public class EmpresaController : Controller
    {
        public HandleError Handle => new HandleError() { Handle = this };
        private string Token => HttpContext.User.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.Hash).Value;


        public async Task<IActionResult> GetEmpresas(int id)
        {
            var request = new Request<List<Empresa>>(Handle, "/Empresa/GetEmpresas?id=" + id, Token);
            try
            {
                var empresas = await request.SendGet();
                return Json(empresas);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public async Task<IActionResult> Modificar(int id)
        {
            ViewData["id"] = id;
            var request = new Request<List<Empresa>>(Handle, "/Empresa/GetEmpresas?id=" + id, Token);
            try
            {
                var empresa = await request.SendGet();
                return View("Actualizar", empresa[0]);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }


        public IActionResult Registrar()
        {
            return View("Actualizar");
        }

        public IActionResult VerEmpresas()
        {
            return View();
        }
    }
}