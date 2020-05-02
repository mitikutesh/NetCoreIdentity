using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3Identity.AutherizationService
{
    public class CustomRequirementClaim : IAuthorizationRequirement
    {
        public string  ClaimType { get;  }
        public CustomRequirementClaim(string claimType)
        {
            ClaimType = claimType;
        }
    }

    public class CustomRequirementHandler : AuthorizationHandler<CustomRequirementClaim>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirementClaim requirement)
        {
            var hasClaim = context.User.Claims.Any(a => a.Type == requirement.ClaimType);
            if (hasClaim)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
