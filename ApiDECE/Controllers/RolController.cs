using ApiDECE.Models.BD;
using ApiDECE.Models.DTO.Seguridad;
using ApiDECE.Policy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiDECE.Controllers
{
    [Route("Rol")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [OptionPermission("Rol")]
    public class RolController : ControllerBase
    {
        private readonly RolDAO dao = new RolDAO();


        [Route("Insert")]
        [HttpPost]
        [ActionPermission("INSERT")]
        public IActionResult Insert(Rol rol)
        {
            var result = dao.Insert(rol);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("Update")]
        [HttpPost]
        [ActionPermission("UPDATE")]
        public IActionResult Update(Rol rol)
        {
            var result = dao.Update(rol);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("FindById")]
        [HttpGet]
        [ActionPermission("QUERY")]
        public IActionResult FindById(int id)
        {
            bool exits = dao.FindById(id);
            if (exits)
            {
                return Ok();
            }
            return NotFound();
        }

        [Route("GetRoles")]
        [HttpGet]
        [ActionPermission("QUERY")]
        public IActionResult GetRoles(int id)
        {
            var list = dao.GetRoles(id);
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
        public IActionResult Enabled(Rol rol)
        {
            var result = dao.Enabled(rol);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("Disabled")]
        [HttpPost]
        [ActionPermission("DELETE")]
        public IActionResult Disabled(Rol rol)
        {
            var result = dao.Disabled(rol);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("ActividadesRolInsert")]
        [HttpPost]
        [ActionPermission("INSERT")]
        public IActionResult ActividadesRolInsert(Rol rol)
        {
            var result = dao.ActividadesRolInsert(rol);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("OpcionesRolInsert")]
        [HttpPost]
        [ActionPermission("INSERT")]
        public IActionResult OpcionesRolInsert(Rol rol)
        {
            var result = dao.OpcionesRolInsert(rol);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("GetActividadesRol")]
        [HttpGet]
        [ActionPermission("QUERY")]
        public IActionResult GetActividadesRol(int id)
        {
            var list = dao.GetActividadesRol(id);
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

        [Route("GetOpcionesRol")]
        [HttpGet]
        [ActionPermission("QUERY")]
        public IActionResult GetOpcionesRol(int id)
        {
            var list = dao.GetOpcionesRol(id);
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

        [Route("GetActividadesOpcionRol")]
        [HttpGet]
        public IActionResult GetActividadesOpcionRol(int id)
        {
            var list = dao.GetActividadesOpcionRol(id);
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
    }
}