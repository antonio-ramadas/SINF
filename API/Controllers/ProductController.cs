﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;


namespace SFA_REST.Controllers
{
    public class ProductController : ApiController
    {
        /// <summary>
        ///   Get method to retrieve all the products in the ERP
        /// </summary>
        /// <returns> List with all the instances of the products </returns>
        [Route("api/product")]
        [HttpGet]
        public List<SFA_REST.Lib_Primavera.Model.Product> Get(){
            return Lib_Primavera.PriIntegration.ListProducts();
        }

        /// <summary>
        ///   Get method to retrieve a certain product in the ERP
        /// </summary>
        /// <returns> Product with the correspondind id </returns>
        [Route("api/product/{id}")]
        [HttpGet]
        public Product Get(string id){
            Lib_Primavera.Model.Product artigo = Lib_Primavera.PriIntegration.GetProduct(id);
            if (artigo == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            else return artigo;
        }

        /// <summary>
        ///   Get method to retrieve a certain set of products given a hint
        /// </summary>
        /// <returns> Product with the correspondind id </returns>
        [Route("api/product/search/category/{hint}")]
        [HttpGet]
        public List<SFA_REST.Lib_Primavera.Model.Product> ListProductsByHint(string hint)
        {
            return Lib_Primavera.PriIntegration.ListProductsByHint(hint);
        }

        /// <summary>
        ///   Get method to retrieve a certain set of products given a its category
        /// </summary>
        /// <returns> Product with the correspondind id </returns>
        [Route("api/product/category/{id}")]
        [HttpGet]
        public List<SFA_REST.Lib_Primavera.Model.Product> GetProductsByCategory(string id)
        {
            return Lib_Primavera.PriIntegration.GetProductsByCategory(id);
        }

        /// <summary>
        ///   Get method to retrieve a certain set of products given its subCategory
        /// </summary>
        /// <returns> Product with the correspondind id </returns>
        [Route("api/product/subcategory/{id}")]
        [HttpGet]
        public List<SFA_REST.Lib_Primavera.Model.Product> GetProductsBySubCategory(string id)
        {
            return Lib_Primavera.PriIntegration.GetProductsBySubCategory(id);
        }

        /// <summary>
        ///   Get method to retrieve the most sold products in the ERP
        /// </summary>
        /// <returns> List with the number of specified top products </returns>
        [Route("api/product/top/{number}")]
        [HttpGet]
        public List<SFA_REST.Lib_Primavera.Model.Product> GetTopProducts(string number)
        {
            return Lib_Primavera.PriIntegration.GetTopProducts(number);
        }
    }
}

