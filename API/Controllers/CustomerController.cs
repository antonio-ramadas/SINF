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
        /** 
         *  GET method for the all the customer of the ERP
         */
        // api/customer
        public IEnumerable<Lib_Primavera.Model.Customer> Get() {
            return Lib_Primavera.PriIntegration.ListCustomers();
        }


        /** 
         *  GET method for a customer of the ERP, with a given id represented as a String
         */
        // api/customer/{id}
        public Customer Get(string id)
        {
            Lib_Primavera.Model.Customer customer = Lib_Primavera.PriIntegration.GetCustomer(id);
            if (customer != null)
                return customer;
            else throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        }

        /** 
         *  POST method for the customer class
         */
        // api/customer
        public HttpResponseMessage Post(Lib_Primavera.Model.Customer customer)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.CreateCustomer(customer);

            if (erro.Erro == 0) {
                var response = Request.CreateResponse(HttpStatusCode.Created, customer);
                /*string uri = Url.Link("SFA_API", new { id = customer.id });
                response.Headers.Location = new Uri(uri);*/
                return response;
            }
            else return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        /** 
         *  PUT method for the edition of a customer's info.
         */
        // api/customer/{id}
        public HttpResponseMessage Put(string id, Lib_Primavera.Model.Customer cliente) {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();

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
