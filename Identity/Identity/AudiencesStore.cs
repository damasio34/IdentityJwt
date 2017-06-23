using System;
using System.Collections.Concurrent;
using Identity.Model;
using System.Configuration;
using System.Security.Cryptography;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Identity.Identity
{
    public static class AudiencesStore
    {
        public static ConcurrentDictionary<string, Audience> AudiencesList = new ConcurrentDictionary<string, Audience>();

        static AudiencesStore()
        {
            var audience = ConfigurationManager.AppSettings["audience"];
            AudiencesList.TryAdd(audience,
            new Audience
            {
                ClientId = audience,
                Base64Secret = ConfigurationManager.AppSettings["secret"],
                Name = "ResourceServer.Api 1"
            });
        }
        public static Audience AddAudience(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            var newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            AudiencesList.TryAdd(clientId, newAudience);
            return newAudience;
        }
        public static Audience FindAudience(string clientId)
        {
            var audience = default(Audience);
            if (AudiencesList.TryGetValue(clientId, out audience)) return audience;
            return null;
        }
    }
}
