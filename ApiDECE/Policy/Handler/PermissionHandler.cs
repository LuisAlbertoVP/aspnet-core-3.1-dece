using System.Threading.Tasks;
using System.Security.Claims;
using ApiDECE.Models.BD;
using Microsoft.AspNetCore.Authorization;


namespace ApiDECE.Policy
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserDAO udao = new UserDAO();
        private readonly RolDAO rdao = new RolDAO();

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                return Task.CompletedTask;
            }

            var id = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;

            var roles = udao.GetRolesUsuario(id);
             
            if(roles != null)
            {
                foreach (var rol in roles)
                {
                    var actividades = rdao.GetActividadesRol(rol.Id);
                    foreach (var actv in actividades)
                    {
                        if (actv.Opcion.Descripcion == requirement.Option && actv.NombreActividad == requirement.Action)
                        {
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }            
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
