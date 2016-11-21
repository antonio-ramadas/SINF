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
    public class SalesRepController : ApiController
    {
        /** 
         *  GET method for the all the sales representative of the ERP
         *  
         *  RETURN: Array with JSON's containing the sales representatives information
        */
        // api/salesRep
        public IEnumerable<Lib_Primavera.Model.SalesRepresentative> Get()
        {
            return Lib_Primavera.PriIntegration.listSalesRepresentatives();
        }


        /** 
         *  GET method for a sales representative of the ERP, with a given id represented as a String
         *  
         *  RETURN: JSON with sales representative information or null, in case the sales representative with the given id doesn't exist
         */
        // api/saleRep/{id}
        public SalesRepresentative Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetSalesRepresentative(id);
        }

        /** 
         *  POST method for the sales representative class
         */
        // api/customer
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

        /** 
         *  PUT method for disabling a sales representative activity.
         */
        public HttpResponseMessage Put(string id, Lib_Primavera.Model.Customer cliente)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();

            try
            {
                erro = Lib_Primavera.PriIntegration.UpdateCustomer(id, cliente);

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

        /** 
         *  PUT method for the edition of a customer's info.
         */
        // api/customer/{id}
        /*public HttpResponseMessage Put(string id, Lib_Primavera.Model.Customer cliente)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();

            try
            {
                erro = Lib_Primavera.PriIntegration.UpdateCustomer(id, cliente);

                if (erro.Erro == 0)
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
            }
            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }*/

    }
}
