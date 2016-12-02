﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Controllers
{
    public class SalesRepController : ApiController
    {
        
        /// <summary>
        ///   GET method for the all the sales representative of the ERP
        /// </summary>
        /// <returns> Array with JSON's containing the sales representatives information </returns>
        [Route("api/salesrep")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.SalesRepresentative> Get()
        {
            return Lib_Primavera.PriIntegration.listSalesRepresentatives();
        }


        /// <summary>
        ///   GET method for a sales representative of the ERP, with a given id represented as a String
        /// </summary>
        /// <returns> JSON with sales representative information or null, in case the sales representative with the given id doesn't exist </returns>
        [Route("api/salesrep/{id}")]
        [HttpGet]
        public SalesRepresentative Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetSalesRepresentative(id);
        }

        
        /// <summary>
        ///   POST method for creation of a sales representative class
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/salesrep")]
        [HttpPost]
        public HttpResponseMessage Post(Lib_Primavera.Model.SalesRepresentative salesRepresentative)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateSalesRepresentative(salesRepresentative);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, salesRepresentative);
                string uri = Url.Link("SFA_API", new { id = salesRepresentative.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        
        /// <summary>
        ///   PUT Method to disable a certain salesman
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/salesrep/disable/{id}")]
        [HttpPut]
        public HttpResponseMessage Put(string id)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();

            try
            {
                erro = Lib_Primavera.PriIntegration.DeactivateSalesRepresentative(id);

                if (erro.Erro == 0)
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }
    }
}