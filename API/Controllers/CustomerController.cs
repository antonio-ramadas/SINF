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
        ///     GET method for the all the customers of the ERP
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
        ///     GET method for a customer of the ERP, with a given string represented his name as a String
        /// </summary>
        /// <returns> Customer with the given name </returns>
        [Route("api/customer/search/{name}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Customer> GetByName(string name)
        {
            return Lib_Primavera.PriIntegration.GetCustomerByName(name);
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

        /// <summary>
        ///     GET method to get a list of all the costumers with a label
        /// </summary>
        /// <returns> List with all the customers in the system with a specific tag </returns>
        [Route("api/customer/label/{label}")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Customer> GetCostumersByLabel(string label)
        {
            return Lib_Primavera.PriIntegration.ListCostumerByLabel(label);
        }

        /// <summary>
        ///     Put method to add a label to a costumer
        /// </summary>
        /// <returns> HttpResponseMessage with the output from the server </returns>
        [Route("api/customer/label/{customerId}/{label}")]
        [HttpPut]
        public HttpResponseMessage PutLabel(string customerId, string label)
        {

            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();

            try
            {
                erro = Lib_Primavera.PriIntegration.AddLabelToCostumer(customerId, label);

                if (erro.Erro == 0)
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }

        /// <summary>
        ///     Delete method to delete a label from a costumer
        /// </summary>
        /// <returns> HttpResponseMessage with the output from the server </returns>
        [Route("api/customer/label/{customerId}/{label}")]
        [HttpDelete]
        public HttpResponseMessage DeleteLabel(string customerId, string label)
        {

            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();

            try
            {
                erro = Lib_Primavera.PriIntegration.DeleteLabelToCostumer(customerId, label);

                if (erro.Erro == 0)
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}
