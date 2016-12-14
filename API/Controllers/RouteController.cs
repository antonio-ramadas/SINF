using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;


namespace SFA_REST.Controllers
{
    public class RouteController : ApiController
    {

        /// <summary>
        ///   GET method for an ordered list of tasks registered in the ERP for a given sales representative.
        /// </summary>
        /// <returns> List of Task </returns>
        [Route("api/route/{salesRepId}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Task> Get(string salesRepId)
        {
            return Lib_Primavera.PriIntegration.ListRoutes(salesRepId);
        }

        

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

    }
}