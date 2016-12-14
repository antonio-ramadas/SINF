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

        /// <summary>
        ///   GET method for an ordered list of tasks registered in the ERP for a given sales representative after the given date.
        /// </summary>
        /// <returns> List of Task </returns>
        [Route("api/route/{salesRepId}/date/{year}/{month}/{day}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Task> Get(string salesRepId, int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);

            return Lib_Primavera.PriIntegration.ListRoutesAfterDate(salesRepId, date);
        }
        /// <summary>
        ///   GET method for an ordered list of tasks registered in the ERP for a given sales representative for today onwards.
        /// </summary>
        /// <returns> List of Task </returns>
        [Route("api/route/{salesRepId}/today")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Task> GetTDailyRoute(string salesRepId)
        {
            DateTime date = DateTime.Now;

            return Lib_Primavera.PriIntegration.ListRoutesAfterDate(salesRepId, date);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

    }
}