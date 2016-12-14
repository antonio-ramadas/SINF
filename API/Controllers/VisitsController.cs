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
        
        /// <summary>
        ///   GET method to retrieve all the tasks in the ERP
        /// </summary>
        /// <returns> List containing the visits' information </returns>
        [Route("api/visits")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Visits> Get()
        {
            return Lib_Primavera.PriIntegration.ListVisits();
        }




        /// <summary>
        ///   GET method to retrieve a certain task in the ERP
        /// </summary>
        /// <returns> Visit with the corresponding specified id </returns>
        [Route("api/visits/{id}")]
        [HttpGet]
        public Visits Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetVisit(id);
        }

        
        /// <summary>
        ///   POST method to create a visit task
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/visits")]
        [HttpPost]
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
