using System;
using System.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Identity.Filters
{
    public class CustomAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            context.Principal = null;
            var authentication = context.Request.Headers.Authorization;
            if (authentication != null && authentication.Scheme == "Bearer")
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadToken(authentication.Parameter) as JwtSecurityToken;
                var roles = new string[] { "user" };
                var login = token.Actor; //authData[0]
                var genericIdentity = new GenericIdentity(login);
                genericIdentity.AddClaims(token.Claims);
                context.Principal = new GenericPrincipal(genericIdentity, roles);
            }
            if (context.Principal == null)
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[] {
                    new AuthenticationHeaderValue("Bearer") }, context.Request);
            }
            return Task.FromResult<object>(null);
        }

        public bool AllowMultiple => false;
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken) 
            => Task.FromResult<object>(null);        
    }
}
