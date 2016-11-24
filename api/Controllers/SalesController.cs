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
    public class SalesController : ApiController
    {
        //
        // GET: /Clientes/

        public IEnumerable<Lib_Primavera.Model.SalesOrder> Get()
        {
            return Lib_Primavera.PriIntegration.ListSalesOrder();
        }


        //// GET api/cliente/5    
        public Lib_Primavera.Model.SalesOrder Get(string id)
        {
            Lib_Primavera.Model.SalesOrder salesOrder = Lib_Primavera.PriIntegration.Encomenda_Get(id);
            if (salesOrder == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            else return salesOrder;
        }


        public HttpResponseMessage Post(Lib_Primavera.Model.SalesOrder dv)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateSalesOrder(dv);

            if (erro.Erro == 0)
                return  Request.CreateResponse(HttpStatusCode.Created, dv.id);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Route("api/sales/reps/{salesRepId}")]
        [HttpPut]
        public HttpResponseMessage Put(string id, Lib_Primavera.Model.Customer cliente)
        {

            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();

            try
            {
                erro = Lib_Primavera.PriIntegration.UpdateCustomer(id, cliente);
                if (erro.Erro == 0)
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                else return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.WriteLine(exc.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }

        [Route("api/sales/reps/{salesRepId}")]
        [HttpGet]
        public List<Lib_Primavera.Model.SalesOrder> GetSalesOrderByRep(string salesRepId)
        {
            return Lib_Primavera.PriIntegration.GetSalesOrderByRep(salesRepId);
        }

        [Route("api/sales/costumer/{costumerId}")]
        [HttpGet]
        public List<Lib_Primavera.Model.SalesOrder> GetSalesOrderByCostumer(string costumerId)
        {
            return Lib_Primavera.PriIntegration.GetSalesOrderByCostumer(costumerId);
        }
    }
}
