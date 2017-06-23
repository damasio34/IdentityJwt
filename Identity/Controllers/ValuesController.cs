using Identity.Filters;
using System.Collections.Generic;
using System.Web.Http;

namespace Identity.Controllers
{
    [RoutePrefix("api/values"), Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values 
        [ClaimsAuthorization(ClaimType = "FTE", ClaimValue = "1"), Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }
        
        // GET api/values/5 
        [AllowAnonymous, Route("{dateTime:datetime}")]
        public string Get2([FromUri] System.DateTimeOffset dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy - hh:mm:ss");
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
