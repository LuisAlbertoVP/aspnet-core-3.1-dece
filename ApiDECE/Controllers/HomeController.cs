using Microsoft.AspNetCore.Mvc;


namespace ApiDECE.Controllers
{
    [Route("Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Index (){
            var json = new { 
                Mensaje = "Bienvenido a API DECE!"
            };
            return Ok(json);
        }

    }
}