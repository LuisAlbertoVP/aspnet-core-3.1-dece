using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppDECE.Models.ViewModel;
using WebAppDECE.Models.Seguridad;
using WebAppDECE.Models.Utilidades;
using System.Security.Claims;


namespace WebAppDECE.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        public HandleError Handle => new HandleError() { Handle = this };
        private string Token => HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Hash).Value;


        public async Task<IActionResult> Activar(int id)
        {
            var request = new Request<Rol>(Handle, "/Rol/Enabled", Token);
            try
            {
                var rol = new Rol(id)
                {
                    UsuarioModificacion = HttpContext.User.Identity.Name,
                    FechaModificacion = DateTime.Now
                };
                await request.SendPost(rol);
                return Redirect("/Rol/Consultar");
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public IActionResult ActividadesRol(string id, string name)
        {
            ViewData["rol"] = id;
            ViewData["rol-name"] = name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ActividadesRolInsert([FromBody] Rol rol)
        {
            var request = new Request<Rol>(Handle, "/Rol/ActividadesRolInsert", Token);
            try
            {
                await request.SendPost(rol);
                return Ok(true);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public async Task<IActionResult> Actualizar(int id)
        {
            ViewData["id"] = id;
            var request = new Request<List<Rol>>(Handle, "/Rol/GetRoles?id=" + id, Token);
            try
            {
                var rol = await request.SendGet();
                return View("Index", new ActualizarRol(rol[0]));
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar(int id, ActualizarRol viewModel)
        {
            ViewData["id"] = id;
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }

            var request = new Request<Rol>(Handle, "/Rol/Update", Token);
            try
            {
                var rol = new Rol(viewModel)
                {
                    Id = id,
                    UsuarioModificacion = HttpContext.User.Identity.Name,
                    FechaModificacion = DateTime.Now
                };
                await request.SendPost(rol);
                ViewData["success"] = new string[] { "modificar", rol.Id.ToString(), rol.Descripcion };
                return View("Index", viewModel);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public IActionResult Consultar()
        {
            return View("VerRoles");
        }

        public async Task<IActionResult> Desactivar(int id)
        {
            var request = new Request<Rol>(Handle, "/Rol/Disabled", Token);
            try
            {
                var rol = new Rol(id)
                {
                    UsuarioModificacion = HttpContext.User.Identity.Name,
                    FechaModificacion = DateTime.Now
                };
                await request.SendPost(rol);
                return Redirect("/Rol/Consultar");
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        private async Task<bool> FindById(int? id)
        {
            var request = new Request<object>(Handle, "/Rol/FindById?id=" + id, Token);
            try
            {
                await request.SendGet();
                return true;
            }
            catch (RequestException)
            {
                return false;
            }
        }

        public async Task<IActionResult> GetActividadesRol(int id)
        {
            var request = new Request<List<Actividad>>(Handle, "/Rol/GetActividadesRol?id=" + id, Token);
            try
            {
                var actividades = await request.SendGet();
                return Json(actividades);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public async Task<IActionResult> GetActividadesOpcionRol(int id)
        {
            var request = new Request<List<Opcion>>(Handle, "/Rol/GetActividadesOpcionRol?id=" + id, Token);
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

        public async Task<IActionResult> GetOpcionesRol(int id)
        {
            var request = new Request<List<Opcion>>(Handle, "/Rol/GetOpcionesRol?id=" + id, Token);
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

        public async Task<IActionResult> GetRoles(int id)
        {
            var request = new Request<List<Rol>>(Handle, "/Rol/GetRoles?id=" + id, Token);
            try
            {
                var roles = await request.SendGet();
                return Json(roles);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public IActionResult Ingresar()
        {
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ingresar(ActualizarRol viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }

            bool IdRol = await FindById(viewModel.Id);
            if (IdRol)
            {
                ModelState.AddModelError("Id", "Identificador del Rol ya existe.");
                return View("Index", viewModel);
            }

            var request = new Request<Rol>(Handle, "/Rol/Insert", Token);
            try
            {
                var rol = new Rol(viewModel)
                {
                    UsuarioIngreso = HttpContext.User.Identity.Name,
                    FechaIngreso = DateTime.Now
                };
                await request.SendPost(rol);
                ViewData["success"] = new string[] { "registrar", rol.Id.ToString(), rol.Descripcion };
                return View("Index");
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public IActionResult OpcionesRol(string id, string name)
        {
            ViewData["rol"] = id;
            ViewData["rol-name"] = name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OpcionesRolInsert([FromBody] Rol rol)
        {
            var request = new Request<Rol>(Handle, "/Rol/OpcionesRolInsert", Token);
            try
            {
                await request.SendPost(rol);
                return Ok(true);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }
    }
}