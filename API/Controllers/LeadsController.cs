using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Controllers
{
    public class LeadsController : ApiController
    {
        /// <summary>
        ///   GET method for the all the leads registered in the ERP  
        /// </summary>
        /// <returns> List of Leads </returns>
        [Route("api/leads")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Lead> Get()
        {
            return Lib_Primavera.PriIntegration.ListLeads();
        }


        /// <summary>
        ///   GET method for a lead in the ERP, with a given id represented as a String
        /// </summary>
        /// <returns> Lead with the respective specified id </returns>
        [Route("api/leads/{id}")]
        [HttpGet]
        public Lead Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetLead(id);
        }

        /// <summary>
        ///   GET method for a lead in the ERP, with a given id represented as a String
        /// </summary>
        /// <returns> Lead with the respective specified id </returns>
        [Route("api/leads/customer/{id}")]
        [HttpGet]
        public IEnumerable<Lead> GetByCustomer(string id)
        {
            return Lib_Primavera.PriIntegration.ListLeadsByCustomer(id);
        }

        /// <summary>
        ///   POST method for the creation of a given lead
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/leads")]
        [HttpPost]
        public HttpResponseMessage Post(Lib_Primavera.Model.Lead lead)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateLead(lead);

            if (erro.Erro == 0)
                return Request.CreateResponse(HttpStatusCode.Created, lead);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);

        }


        /// <summary>
        ///   DELETE method to erase a certain lead
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/leads/{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.DeleteLead(id);

            if (erro.Erro == 0)
                return Request.CreateResponse(HttpStatusCode.Created, id);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
