using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppDECE.Models.ViewModel;
using WebAppDECE.Models.Seguridad;
using WebAppDECE.Models.Utilidades;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;


namespace WebAppDECE.Controllers
{
    public class HomeController : Controller
    {
        public HandleError Handle => new HandleError() { Handle = this };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login viewModel)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Home/Index");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var request = new Request<Usuario>(Handle, "/Account/Login");
            try
            {
                var user = HashPassword(new Usuario(viewModel));
                var token = await request.Authenticate(user);
                CreateCookie(token);
                return RedirectToAction("Index");
            }
            catch (RequestException ex)
            {
                if (ex.Message.Equals("NotFound"))
                {
                    ModelState.AddModelError("Clave", "Las credenciales de acceso son erroneas!");
                    return View(viewModel);
                }
                return ex.Result;
            }
        }

        private Usuario HashPassword(Usuario user)
        {
            var combinedPassword = string.Concat(user.Clave, "S3creT-@D3cE_2o20:LV+1");
            var sha256 = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            user.Clave = Convert.ToBase64String(hash);
            return user;
        }

        private void CreateCookie(Token token)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, token.Nombres),
                new Claim(ClaimTypes.Email, token.Id),
                new Claim(ClaimTypes.Hash, token.Key)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "User Identity");
            
            HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

        }

        [Authorize]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}