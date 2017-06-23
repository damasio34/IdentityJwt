using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;

namespace Identity.Controllers
{
    [RoutePrefix("api/account"), Authorize]
    public class AccountController : ApiController
    {
        // GET api/values 
        [HttpGet, Route("")]
        public IEnumerable<Claim> Get()
        {
            var identity = (ClaimsIdentity) User.Identity;
            var claims = identity.Claims;

            return claims;
        }
    }
}
