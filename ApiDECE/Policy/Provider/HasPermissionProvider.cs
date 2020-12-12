using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace ApiDECE.Policy
{
    public class HasPermissionProvider : IAuthorizationPolicyProvider
    {
        readonly int POLICY_PREFIX = 16;
        string option;


        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith("OptionPermission", StringComparison.OrdinalIgnoreCase))
            {
                option = policyName.Substring(POLICY_PREFIX);
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser();
                return Task.FromResult(policy.Build());
            }

            if (policyName.StartsWith("ActionPermission", StringComparison.OrdinalIgnoreCase))
            {
                var action = policyName.Substring(POLICY_PREFIX);
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                policy.AddRequirements(new PermissionRequirement(option, action));
                return Task.FromResult(policy.Build());
            }

            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser();
            return Task.FromResult(policy.Build());
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy>(null);
    }
}
