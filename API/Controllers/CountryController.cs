using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SFA_REST.Controllers
{
    public class CountryController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Lib_Primavera.Model.Country> Get()
        {
            return Lib_Primavera.PriIntegration.GetCountries();
        }

    }
}