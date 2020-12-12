using Microsoft.AspNetCore.Authorization;


namespace ApiDECE.Policy
{
    public class ActionPermissionAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "ActionPermission";

        public ActionPermissionAttribute(string permission) => Permission = permission;


        public string Permission
        {
            get
            {
                return Policy.Substring(POLICY_PREFIX.Length);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value}";
            }
        }
    }
}
