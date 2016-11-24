using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;


namespace SFA_REST.Controllers
{
    public class DocVendaController : ApiController
    {
        //
        // GET: /Clientes/

        public IEnumerable<Lib_Primavera.Model.SalesOrder> Get()
        {
            return Lib_Primavera.PriIntegration.ListSalesOrder();
        }


        // GET api/cliente/5    
        public Lib_Primavera.Model.SalesOrder Get(string id)
        {
            Lib_Primavera.Model.SalesOrder docvenda = Lib_Primavera.PriIntegration.Encomenda_Get(id);
            if (docvenda == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return docvenda;
            }
        }


        public HttpResponseMessage Post(Lib_Primavera.Model.SalesOrder dv)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateSalesOrder(dv);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, dv.id);
                string uri = Url.Link("SFA_API", new {DocId = dv.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }


        public HttpResponseMessage Put(string id, Lib_Primavera.Model.Customer cliente)
        {

            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();

            try
            {
                erro = Lib_Primavera.PriIntegration.UpdateCustomer(id, cliente);
                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }
            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }
    }
}
