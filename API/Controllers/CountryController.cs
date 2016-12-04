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
        /// <summary>
        ///     GET method to retrieve the countrie's list
        /// </summary>
        /// <returns> List with all the countries in the system</returns>
        [Route("api/country")]
        public IEnumerable<Lib_Primavera.Model.Country> Get()
        {
            return Lib_Primavera.PriIntegration.GetCountries();
        }

    }
}