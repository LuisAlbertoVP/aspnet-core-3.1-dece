using ApiDECE.Models.BD;
using ApiDECE.Models.DTO.Seguridad;
using ApiDECE.Policy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiDECE.Controllers
{
    [Route("User")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [OptionPermission("Usuario")]
    public class UserController : ControllerBase
    {
        private readonly UserDAO dao = new UserDAO();


        [Route("Insert")]
        [HttpPost]
        [ActionPermission("INSERT")]
        public IActionResult Insert(Usuario user)
        {
            var result = dao.Insert(HashPassword(user));
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("Update")]
        [HttpPost]
        [ActionPermission("UPDATE")]
        public IActionResult Update(Usuario user)
        {
            var result = dao.Update(HashPassword(user));
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("FindById")]
        [HttpGet]
        [ActionPermission("QUERY")]
        public IActionResult FindById(string id)
        {
            bool exits = dao.FindById(id);
            if (exits)
            {
                return Ok();
            }
            return NotFound();
        }

        [Route("GetUsers")]
        [HttpGet]
        [ActionPermission("QUERY")]
        public IActionResult GetUsers(string id)
        {
            var list = dao.GetUsuarios(id);
            if(list != null)
            {
                if (list.Count > 0)
                {
                    return Ok(list);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [Route("GetRolesUsuario")]
        [HttpGet]
        [ActionPermission("QUERY")]
        public IActionResult GetRolesUsuario(string id)
        {
            var list = dao.GetRolesUsuario(id);
            if (list != null)
            {
                if (list.Count > 0)
                {
                    return Ok(list);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [Route("Enabled")]
        [HttpPost]
        [ActionPermission("DELETE")]
        public IActionResult Enabled(Usuario user)
        {
            var result = dao.Enabled(user);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("Disabled")]
        [HttpPost]
        [ActionPermission("DELETE")]
        public IActionResult Disabled(Usuario user)
        {
            var result = dao.Disabled(user);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("RolesUsuarioInsert")]
        [HttpPost]
        [ActionPermission("INSERT")]
        public IActionResult RolesUsuarioInsert(Usuario user)
        {
            var result = dao.RolesUsuarioInsert(user);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        private Usuario HashPassword(Usuario user)
        {
            var combinedPassword = string.Concat(user.Clave, "S3creT-@D3cE_2o20:LV+1");
            var sha256 = new System.Security.Cryptography.SHA256Managed();
            var bytes = System.Text.Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            user.Clave = System.Convert.ToBase64String(hash);
            return user;
        }
    }
}