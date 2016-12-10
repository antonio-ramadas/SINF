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
        [Route("api/stats/income/{id}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.IncomeYear> Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetIncomeStatBySalesRep(id);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/income/{id}/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomeYear GetByYear(string id, int year)
        {
            return Lib_Primavera.PriIntegration.GetIncomeStatByYear(id,year);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/income/{id}/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomeMonth GetByMonth(string id, int year, int month)
        {
            return Lib_Primavera.PriIntegration.GetIncomeStatByMonth(id, year, month);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/sales/{id}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.SalesYear> GetSales(string id)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatBySalesRep(id);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/sales/{id}/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.SalesYear GetSalesByYear(string id, int year)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatByYear(id, year);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/sales/{id}/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.SalesMonth GetSalesByMonth(string id, int year, int month)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatByMonth(id, year, month);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/income-per-sale/{id}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.IncomePerYear> GetIncomePerSalesByRep(string id)
        {
            return Lib_Primavera.PriIntegration.GetIncomePerYearBySalesRep(id);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/income-per-sale/{id}/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomePerYear GetIncomePerSalesByYear(string id, int year)
        {
            return Lib_Primavera.PriIntegration.GetIncomePerYear(id, year);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/income-per-sale/{id}/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomePerMonth GetIncomePerSalesByMonth(string id, int year, int month)
        {
            return Lib_Primavera.PriIntegration.GetIncomePerMonth(id, year, month);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2008
        /// </summary>
        /// <returns> List of TopCategory </returns>
        [Route("api/stats/category-top/{id}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.TopCategory> GetTopCategories(string id)
        {
            return Lib_Primavera.PriIntegration.GetSalesTopCategories(id);
        }
    }
}