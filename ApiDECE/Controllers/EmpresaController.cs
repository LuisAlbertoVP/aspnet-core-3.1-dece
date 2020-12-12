using ApiDECE.Models.BD;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ApiDECE.Controllers
{
    [Route("Empresa")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaDAO dao = new EmpresaDAO();


        [Route("GetEmpresas")]
        [HttpGet]
        public IActionResult GetEmpresas(int id)
        {
            var list = dao.GetEmpresas(id);
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