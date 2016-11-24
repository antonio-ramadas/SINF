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
        /** 
        *  GET method for the all the customer of the ERP
        */
        // api/customer
        public IEnumerable<Lib_Primavera.Model.Lead> Get()
        {
            return Lib_Primavera.PriIntegration.ListLeads();
        }


        /** 
         *  GET method for a customer of the ERP, with a given id represented as a String
         */
        // api/customer/{id}
        public Lead Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetLead(id);
        }

        /** 
         *  POST method for the customer class
         */
        // api/customer
        public HttpResponseMessage Post(Lib_Primavera.Model.Lead lead)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateLead(lead);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, lead);
                string uri = Url.Link("SFA_API", new { id = lead.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else return Request.CreateResponse(HttpStatusCode.BadRequest);

        }
    }
}
