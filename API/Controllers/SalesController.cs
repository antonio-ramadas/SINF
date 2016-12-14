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
        /// <summary>
        ///   Get method to retrieve all the sales in the ERP
        /// </summary>
        /// <returns> List with all the instances of the sales </returns>
        [Route("api/sales")]
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.SalesOrder> Get()
        {
            return Lib_Primavera.PriIntegration.ListSalesOrder();
        }

        /// <summary>
        ///   Get method to retrieve a single sale in the ERP
        /// </summary>
        /// <returns> The sale with the corresponding specified id </returns>
        [Route("api/sales/{id}")]
        [HttpGet]
        public Lib_Primavera.Model.SalesOrder Get(string id)
        {
            Lib_Primavera.Model.SalesOrder salesOrder = Lib_Primavera.PriIntegration.Encomenda_Get(id);
            if (salesOrder == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            else return salesOrder;
        }

        /// <summary>
        ///   Post method for creation of a sale order
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/sales")]
        [HttpPost]
        public HttpResponseMessage Post(Lib_Primavera.Model.SalesOrder dv)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateSalesOrder(dv);

            if (erro.Erro == 0)
                return Request.CreateResponse(HttpStatusCode.Created, dv.id);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        ///   Get method to retrieve the sales obtained by a salesman
        /// </summary>
        /// <returns> List of SalesOrder </returns>
        [Route("api/sales/reps/{salesRepId}/{number}")]
        [HttpGet]
        public List<Lib_Primavera.Model.SalesOrder> GetSalesOrderByRep(string salesRepId, string number)
        {
            return Lib_Primavera.PriIntegration.GetSalesOrderByRep(salesRepId, number);
        }

        /// <summary>
        ///   Get method to retrieve the sales relative to a certain customer
        /// </summary>
        /// <returns> List of SalesOrder </returns>
        [Route("api/sales/customer/{costumerId}/{number}")]
        [HttpGet]
        public List<Lib_Primavera.Model.SalesOrder> GetSalesOrderByCustomer(string costumerId, string number)
        {
            return Lib_Primavera.PriIntegration.GetSalesOrderByCustomer(costumerId, number);
        }

        /// <summary>
        ///   Get method to retrieve the sales relative to a certain customer
        /// </summary>
        /// <returns> List of SalesOrder </returns>
        [Route("api/sales/history/{productId}/{number}")]
        [HttpGet]
        public List<Lib_Primavera.Model.SalesOrderHistory> GetSalesOrderHistoryByProduct(string productId, string number)
        {
            return Lib_Primavera.PriIntegration.GetSalesOrderByProductForHistory(productId, number);
        }
    }
}