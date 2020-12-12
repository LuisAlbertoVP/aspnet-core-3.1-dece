using Microsoft.AspNetCore.Authorization;


namespace ApiDECE.Policy
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string option, string action)
        {
            Option = option;
            Action = action;
        }

        public string Option { get; set;}

        public string Action { get; set; }
    }
}
