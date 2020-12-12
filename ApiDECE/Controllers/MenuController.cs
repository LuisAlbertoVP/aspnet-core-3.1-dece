using ApiDECE.Models.BD;
using ApiDECE.Models.DTO.Seguridad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiDECE.Controllers
{
    [Route("Menu")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuController : ControllerBase
    {
        private readonly UserDAO udao = new UserDAO();
        private readonly RolDAO rdao = new RolDAO();


        [Route("GetMenu")]
        [HttpGet]
        public IActionResult GetMenu(string id)
        {
            var list = udao.GetRolesUsuario(id);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    var menu = new List<List<Modulo>>();
                    foreach (var rol in list)
                    {
                        menu.Add(rdao.GetMenuRol(rol));
                    }
                    return Ok(menu);
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}