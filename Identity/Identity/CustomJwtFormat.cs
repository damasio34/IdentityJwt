using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using Thinktecture.IdentityModel.Tokens;

namespace Identity.Identity
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly byte[] _secret = default(byte[]);
        private readonly string _issuer = string.Empty;
        private readonly string _audience = string.Empty;

        public CustomJwtFormat(string issuer, byte[] secret)
        {
            this._issuer = issuer;
            this._secret = secret;
            this._audience = ConfigurationManager.AppSettings["audience"];
        }
        public string Protect(AuthenticationTicket authenticationTicket)
        {
            if (authenticationTicket == null) throw new ArgumentNullException(nameof(authenticationTicket));
            var audienceId = authenticationTicket.Properties.Dictionary.ContainsKey("audience") ? 
                authenticationTicket.Properties.Dictionary["audience"] : null;
            if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            var audience = AudiencesStore.FindAudience(audienceId);
            var symmetricKeyAsBase64 = audience.Base64Secret;
            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
            var signingKey = new HmacSigningCredentials(keyByteArray);
            var issued = authenticationTicket.Properties.IssuedUtc;
            var expires = authenticationTicket.Properties.ExpiresUtc;
            var token = new JwtSecurityToken(_issuer, audienceId,
                    authenticationTicket.Identity.Claims,
                    issued.Value.UtcDateTime,
                    expires.Value.UtcDateTime,
                    signingKey);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);

            return jwt;
        }
        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
