using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace ManageOwnerships.Domain.Helpers
{
    public static class TokenHelper
    {
        public static int GetOwnerId(HttpContext httpContext)
        {
            int ownerId = 0;
            Claim claim = httpContext.User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "OwnerId");
            if (claim is not null)
            {
                ownerId = Convert.ToInt32(claim.Value);
            }
            return ownerId;
        }
    }
}
