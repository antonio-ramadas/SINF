using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Controllers
{
    public class CustomerController : ApiController
    {
        /// <summary>
        ///     GET method for the all the customer of the ERP
        /// </summary>
        /// <returns> List with all the customers in the system</returns>
        [Route("api/customer")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Customer> Get() {
            return Lib_Primavera.PriIntegration.ListCustomers();
        }

        /// <summary>
        ///     GET method for a customer of the ERP, with a given id represented as a String
        /// </summary>
        /// <returns> Customer with the given id </returns>
        [Route("api/customer/{id}")]
        [HttpGet]
        public Customer Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetCustomer(id);
        }

        /// <summary>
        ///     POST method for the customer class
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/customer")]
        [HttpPost]
        public HttpResponseMessage Post(Lib_Primavera.Model.Customer customer)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateCustomer(customer);

            if (erro.Erro == 0) 
                return Request.CreateResponse(HttpStatusCode.Created, customer);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        /// <summary>
        ///     PUT method edit a customer's info
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/customer/{id}")]
        [HttpPut]
        public HttpResponseMessage Put(string id, Lib_Primavera.Model.Customer cliente) {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();

            try {
                erro = Lib_Primavera.PriIntegration.UpdateCustomer(id, cliente);

                if (erro.Erro == 0)
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                else 
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
            }
            catch (Exception exc) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }
    }
}
