using ApiDECE.Models.BD;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiDECE.Controllers
{
    [Route("Opcion")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OpcionController : ControllerBase
    {
        private readonly OpcionDAO dao = new OpcionDAO();
    

        [Route("GetOpciones")]
        [HttpGet]
        public IActionResult GetOpciones(int id)
        {
            var list = dao.GetOpciones(id);
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