using System;
using Microsoft.AspNetCore.Authorization;


namespace ApiDECE.Policy
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class OptionPermissionAttribute : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "OptionPermission";

        public OptionPermissionAttribute(string permission) => Permission = permission;


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
