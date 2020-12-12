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
    public class UsuarioController : Controller
    {
        public HandleError Handle => new HandleError() { Handle = this };
        private string Token => HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Hash).Value;


        public async Task<IActionResult> Activar(string id)
        {
            var request = new Request<Usuario>(Handle, "/User/Enabled", Token);
            try
            {
                var user = new Usuario(id)
                {
                    UsuarioModificacion = HttpContext.User.Identity.Name,
                    FechaModificacion = DateTime.Now
                };
                await request.SendPost(user);
                return Redirect("/Usuario/Consultar");
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public async Task<IActionResult> Actualizar(string id)
        {
            ViewData["id"] = id;
            var request = new Request<List<Usuario>>(Handle, "/User/GetUsers?id=" + id, Token);
            try
            {
                var user = await request.SendGet();
                return View("Index", new ActualizarUsuario(user[0]));
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar(string id, ActualizarUsuario viewModel)
        {
            ViewData["id"] = id;
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }

            var request = new Request<Usuario>(Handle, "/User/Update", Token);
            try
            {
                var user = new Usuario(viewModel)
                {
                    Id = id,
                    UsuarioModificacion = HttpContext.User.Identity.Name,
                    FechaModificacion = DateTime.Now
                };
                await request.SendPost(HashPassword(user));
                ViewData["success"] = new string[] { "modificar", user.Id };
                return View("Index", viewModel);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public IActionResult Consultar()
        {
            return View("VerUsuarios");
        }

        public async Task<IActionResult> Desactivar(string id)
        {
            var request = new Request<Usuario>(Handle, "/User/Disabled", Token);
            try
            {
                var user = new Usuario(id)
                {
                    UsuarioModificacion = HttpContext.User.Identity.Name,
                    FechaModificacion = DateTime.Now
                };
                await request.SendPost(user);
                var myId = HttpContext.User.FindFirst(claim => claim.Type == ClaimTypes.Email).Value;
                if (myId.Equals(id))
                {
                    return Redirect("/Home/LogOut");
                }
                return Redirect("/Usuario/Consultar");
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        private async Task<bool> FindById(string id)
        {
            var request = new Request<object>(Handle, "/User/FindById?id=" + id, Token);
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

        public async Task<IActionResult> GetUsuarios(string id)
        {
            var request = new Request<List<Usuario>>(Handle, "/User/GetUsers?id=" + id, Token);
            try
            {
                var usuarios = await request.SendGet();
                return Json(usuarios);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public async Task<IActionResult> GetRolesUsuario(string id)
        {
            var request = new Request<List<Rol>>(Handle, "/User/GetRolesUsuario?id=" + id, Token);
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
        public async Task<IActionResult> Ingresar(ActualizarUsuario viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", viewModel);
            }

            bool IdUsuario = await FindById(viewModel.Id);
            if (IdUsuario)
            {
                ModelState.AddModelError("Id", "Identificador de Usuario ya existe.");
                return View("Index", viewModel);
            }

            var request = new Request<Usuario>(Handle, "/User/Insert", Token);
            try
            {
                var user = new Usuario(viewModel)
                {
                    UsuarioIngreso = HttpContext.User.Identity.Name,
                    FechaIngreso = DateTime.Now
                };

                await request.SendPost(HashPassword(user));
                ViewData["success"] = new string [] { "registrar", user.Id };
                return View("Index");
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        public IActionResult RolesUsuario(string id)
        {
            ViewData["usuario"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RolesUsuarioInsert([FromBody] Usuario user)
        {
            var request = new Request<Usuario>(Handle, "/User/RolesUsuarioInsert", Token);
            try
            {
                await request.SendPost(user);
                return Ok(true);
            }
            catch (RequestException ex)
            {
                return ex.Result;
            }
        }

        private Usuario HashPassword(Usuario user)
        {
            var combinedPassword = string.Concat(user.Clave, "S3creT-@D3cE_2o20:LV+1");
            var sha256 = new System.Security.Cryptography.SHA256Managed();
            var bytes = System.Text.Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            user.Clave = Convert.ToBase64String(hash);
            return user;
        }
    }
}