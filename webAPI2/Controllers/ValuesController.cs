using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webAPI2.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST(CREATE) api/values
        /*a value may be passed in the iurl or in the body. If a value is passed in both, the url value is used, 
         * the [FromBody] say to not use the url but only the body*/
        public void Post([FromBody]string value)
        {
        }

        // PUT(UPDATE) api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
