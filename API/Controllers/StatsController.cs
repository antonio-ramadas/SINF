﻿using System;
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
        ///   GET method for income registered in the ERP for a given sales representative since 2015
        /// </summary>
        /// <returns> List of IncomeYear </returns>
        [Route("api/stats/income/{id}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.IncomeYear> Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetIncomeStatBySalesRep(id);
        }

        /// <summary>
        ///   GET method for income registered in the ERP for a given sales representative in a given year
        /// </summary>
        /// <returns> IncomeYear </returns>
        [Route("api/stats/income/{id}/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomeYear GetByYear(string id, int year)
        {
            return Lib_Primavera.PriIntegration.GetIncomeStatByYear(id,year);
        }

        /// <summary>
        ///   GET method for income registered in the ERP for a given sales representative in a given month
        /// </summary>
        /// <returns> IncomeMonth </returns>
        [Route("api/stats/income/{id}/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomeMonth GetByMonth(string id, int year, int month)
        {
            return Lib_Primavera.PriIntegration.GetIncomeStatByMonth(id, year, month);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2015
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/sales/{id}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.SalesYear> GetSales(string id)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatBySalesRep(id);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2015
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/sales/{id}/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.SalesYear GetSalesByYear(string id, int year)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatByYear(id, year);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2015
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/sales/{id}/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.SalesMonth GetSalesByMonth(string id, int year, int month)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatByMonth(id, year, month);
        }

        /// <summary>
        ///   GET method for the average income for each sale in of a sales rep since 2015
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/income-per-sale/{id}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.IncomePerSaleYear> GetIncomePerSalesByRep(string id)
        {
            return Lib_Primavera.PriIntegration.GetIncomePerYearBySalesRep(id);
        }

        /// <summary>
        ///   GET method for the average income for each sale in given year
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/income-per-sale/{id}/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomePerSaleYear GetIncomePerSalesByYear(string id, int year)
        {
            return Lib_Primavera.PriIntegration.GetIncomePerYear(id, year);
        }

        /// <summary>
        ///   GET method for the average income for each sale in given month.
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/income-per-sale/{id}/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomePerSaleMonth GetIncomePerSalesByMonth(string id, int year, int month)
        {
            return Lib_Primavera.PriIntegration.GetIncomePerMonth(id, year, month);
        }

        /// <summary>
        ///   GET method for the Top Categories sold by the sales representative
        /// </summary>
        /// <returns> List of TopCategory </returns>
        [Route("api/stats/category-top/{id}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.TopCategory> GetTopCategories(string id)
        {
            return Lib_Primavera.PriIntegration.GetSalesTopCategories(id);
        }

        /// <summary>
        ///   GET method for the Top Categories sold by the sales representative
        /// </summary>
        /// <returns> List of TopCategory </returns>
        [Route("api/stats/total/category-top")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Stats.TopCategory> GetTopCategories()
        {
            return Lib_Primavera.PriIntegration.GetSalesTopCategories();
        }

        /// <summary>
        ///   GET method for the Top SalesRepresentative of the company
        /// </summary>
        /// <returns> List of Top SalesRepresentative </returns>
        [Route("api/stats/salesRep/{number}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.SalesRepresentative> GetTopSalesRep(string number)
        {
            return Lib_Primavera.PriIntegration.GetTopSalesRep(number);
        }

        /// <summary>
        ///   GET method for the the profits of a year
        /// </summary>
        /// <returns> Money made in a year </returns>
        [Route("api/stats/total/income/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomeYear getYearTotalMerc(int year)
        {
            return Lib_Primavera.PriIntegration.GetIncomeStatByYear(year);
        }

        /// <summary>
        ///   GET method for the the profits of a month
        /// </summary>
        /// <returns> Money made in a year </returns>
        [Route("api/stats/total/income/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomeMonth getYearTotalMerc(int year,int month)
        {
            return Lib_Primavera.PriIntegration.GetIncomeStatByMonth(year, month);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2015
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/total/sales/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.SalesYear GetSalesByYear(int year)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatByYear(year);
        }

        /// <summary>
        ///   GET method for the all the sales states registered in the ERP for a given sales representative since 2015
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/total/sales/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.SalesMonth GetSalesByMonth(int year, int month)
        {
            return Lib_Primavera.PriIntegration.GetSalesStatByMonth(year, month);
        }

        /// <summary>
        ///   GET method for the average income for each sale in given year
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/total/income-per-sale/{year}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomePerSaleYear GetIncomePerSalesByYear(int year)
        {
            return Lib_Primavera.PriIntegration.GetIncomePerYear(year);
        }

        /// <summary>
        ///   GET method for the average income for each sale in given month.
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/stats/total/income-per-sale/{year}/{month}")]
        [HttpGet]
        public Lib_Primavera.Model.Stats.IncomePerSaleMonth GetIncomePerSalesByMonth(int year, int month)
        {
            return Lib_Primavera.PriIntegration.GetIncomePerMonth(year, month);
        }

    }
}