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
    public class CategoryController : ApiController
    {
        /** 
         *  GET method for all the items of the ERP
         */
        public List<string> Get()
        {
            return Lib_Primavera.PriIntegration.CategoryList();
        }

    }
}

