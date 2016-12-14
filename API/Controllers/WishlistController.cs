﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Controllers
{
    public class WishlistController : ApiController
    {

        /// <summary>
        ///   GET method for a lead in the ERP, with a given id represented as a String
        /// </summary>
        /// <returns> Lead with the respective specified id </returns>
        [Route("api/wishlist/{id}")]
        [HttpGet]
        public WishList Get(string id)
        {
            return Lib_Primavera.PriIntegration.GetWish(id);
        }

        /// <summary>
        ///   GET method for a lead in the ERP, with a given id represented as a String
        /// </summary>
        /// <returns> Lead with the respective specified id </returns>
        [Route("api/wishlist/customer/{id}")]
        [HttpGet]
        public IEnumerable<WishList.WishLine> GetWishByCustomer(string id)
        {
            return Lib_Primavera.PriIntegration.ListWishesByCustomer(id);
        }

        /// <summary>
        ///   POST method for the creation of a given lead
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/wishlist")]
        [HttpPost]
        public HttpResponseMessage Post(Lib_Primavera.Model.WishList lead)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.CreateWish(lead);

            if (erro.Erro == 0)
                return Request.CreateResponse(HttpStatusCode.Created, lead);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);

        }


        /// <summary>
        ///   DELETE method to erase a certain lead
        /// </summary>
        /// <returns> HttpResponse with the output from the server </returns>
        [Route("api/wishlist/delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(Lib_Primavera.Model.WishList.WishLine line)
        {
            Lib_Primavera.Model.ErrorResponse erro = new Lib_Primavera.Model.ErrorResponse();
            erro = Lib_Primavera.PriIntegration.DeleteWish(line);

            if (erro.Erro == 0)
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
