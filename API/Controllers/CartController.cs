using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Controllers
{
    public class CartController : ApiController
    {

        /// <summary>
        ///   GET method for a lead in the ERP, with a given id represented as a String
        /// </summary>
        /// <returns> Lead with the respective specified id </returns>
        [Route("api/wishlist/{id}")]
        [HttpGet]
        public Cart Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetCart(id);
        }

        /// <summary>
        ///   GET method for a lead in the ERP, with a given id represented as a String
        /// </summary>
        /// <returns> Lead with the respective specified id </returns>
        [Route("api/wishlist/customer/{id}")]
        [HttpGet]
        public IEnumerable<Cart.CartLine> GetCartByCustomer(string id)
        {
            return Lib_Primavera.PriIntegration.GetCartByCustomer(id);
        }

        /// <summary>
        ///   POST method for the creation of a given lead
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/wishlist")]
        [HttpPost]
        public HttpResponseMessage Post(Lib_Primavera.Model.Cart lead)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateCart(lead);

            if (erro.Erro == 0)
                return Request.CreateResponse(HttpStatusCode.Created, lead);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);

        }


        /// <summary>
        ///   DELETE method to erase a certain lead
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/wishlist/delete")]
        [HttpPost]
        public HttpResponseMessage Delete(Lib_Primavera.Model.Cart.CartLine line)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.DeleteCartLine(line);

            if (erro.Erro == 0)
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
