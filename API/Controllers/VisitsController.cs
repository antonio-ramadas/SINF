using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Controllers
{
    public class VisitsController : ApiController
    {
        /** 
        *  GET method for the all the customer of the ERP
        */
        // api/visits
        public IEnumerable<Lib_Primavera.Model.Visits> Get()
        {
            return Lib_Primavera.PriIntegration.ListVisits();
        }


        /** 
         *  GET method for a customer of the ERP, with a given id represented as a String
         */
        // api/customer/{id}
        public Visits Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetVisit(id);
        }

        /** 
         *  POST method for the customer class
         */
        // api/customer
        public HttpResponseMessage Post(Lib_Primavera.Model.Visits visit)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateVisit(visit);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, visit);
                string uri = Url.Link("SFA_API", new { id = visit.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            else return Request.CreateResponse(HttpStatusCode.BadRequest);

        }
    }
}
