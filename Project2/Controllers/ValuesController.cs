﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
//using Auth0.AuthenticationApi;

namespace Project2.Controllers
{
	[EnableCors(origins: "*", headers:"*", methods:"*")]
	[Authorize]
    public class ValuesController : ApiController
    {
        
		// GET api/values
        public IEnumerable<string> Get()
        {
			var u = User.Identity.Name;
			//ClaimsPrincipal.
			return new string[] { "value1", "value2", u };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
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
