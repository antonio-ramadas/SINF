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
    public class ProductController : ApiController
    {
        /** 
         *  GET method for all the items of the ERP
         */
        public IEnumerable<Lib_Primavera.Model.Product> Get(){
            return Lib_Primavera.PriIntegration.ListaArtigos();
        }

        /** 
        *  GET method for an item of the ERP, with a given id represented as a String
        */
        public Product Get(string id){
            Lib_Primavera.Model.Product artigo = Lib_Primavera.PriIntegration.GetProduct(id);
            if (artigo == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            else return artigo;
        }

    }
}

