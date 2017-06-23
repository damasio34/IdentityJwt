using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Identity.Proxys;
using Identity.Model;
using System.Security.Claims;
using Identity.Identity;
using System.Configuration;

namespace Identity.Providers
{
    public class OAuthAuthorizationToroProvider : OAuthAuthorizationServerProvider
    {        
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {                        
            var sessionProxyMock = new SessionProxyMock();
            var loginResult = sessionProxyMock.Login(context.UserName, context.Password);

            //var loginResult = new SessionService().Login(context.UserName, context.Password);

            if (loginResult == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Actor, context.UserName));
            oAuthIdentity.AddClaim(CreateClaim("FTE", "1"));            
            var properties = CreateProperties("");
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);

            SessionIdStore.AddSessionId(new Token("", DateTime.Now.Add(context.Options.AccessTokenExpireTimeSpan)), loginResult.Token);
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
                context.AdditionalResponseParameters.Add(property.Key, property.Value);

            return Task.FromResult<object>(null);
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var clientId = string.Empty;
            var clientSecret = string.Empty;
            var symmetricKeyAsBase64 = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
                context.TryGetFormCredentials(out clientId, out clientSecret);

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }
            var audience = AudiencesStore.FindAudience(context.ClientId);
            if (audience == null)
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == ConfigurationManager.AppSettings["audience"])
            {
                var expectedRootUri = new Uri(context.Request.Uri, "/");
                if (expectedRootUri.AbsoluteUri == context.RedirectUri) context.Validated();
            }

            return Task.FromResult<object>(null);
        }
        public static AuthenticationProperties CreateProperties(string userName = null)
        {
            var customProperties = new Dictionary<string, string>();
            customProperties.Add("audience", ConfigurationManager.AppSettings["audience"]);
            if (!string.IsNullOrEmpty(userName)) customProperties.Add("userName", userName);
            //{
            //    AllowRefresh = true,
            //    ExpiresUtc = new DateTimeOffset().AddMinutes(5),
            //    IsPersistent = false,
            //    IssuedUtc = DateTime.UtcNow,
            //    RedirectUri = "http://localhost:9001/api/values"
            //};

            var authenticationProperties = new AuthenticationProperties(customProperties);
            return authenticationProperties;
        }
    }
}
