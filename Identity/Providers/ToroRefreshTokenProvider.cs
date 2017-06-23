using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Identity.Providers
{
    // sample persistence of refresh tokens
    // this is not production ready!
    public class ToroRefreshTokenProvider : IAuthenticationTokenProvider
    {
        ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        public void Create(AuthenticationTokenCreateContext context) => this.CreateAsync(context).Wait();
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            // maybe only create a handle the first time, then re-use
            _refreshTokens.TryAdd(token, context.Ticket);

            // consider storing only the hash of the handle
            context.SetToken(token);
        }
        public void Receive(AuthenticationTokenReceiveContext context) => this.ReceiveAsync(context).Wait();
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var ticket = default(AuthenticationTicket);
            if (_refreshTokens.TryRemove(context.Token, out ticket))
            {
                context.SetTicket(ticket);
            }
        }
    }
}
