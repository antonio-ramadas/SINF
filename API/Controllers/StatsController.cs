using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Controllers
{
    public class StatsController : ApiController
    {
        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/rep/{id}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.SalesYear> Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatBySalesRep(id);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/rep/{id}/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.SalesYear GetByYear(string id, int year)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatByYear(id,year);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/rep/{id}/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.SalesMonth GetByMonth(string id, int year, int month)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatByMonth(id, year, month);
        }
    }
}