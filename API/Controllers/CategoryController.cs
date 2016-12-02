using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;


namespace SFA_REST.Controllers
{
    public class CategoryController : ApiController
    {
        /// <summary>
        ///     GET method for all the categories and respective subCategories of the ERP
        /// </summary>
        /// <returns> List with the categories existing in the ERP </returns>
        [Route("api/category")]
        [HttpGet]
        public List<Lib_Primavera.Model.Category> Get()
        {
            return Lib_Primavera.PriIntegration.CategoryList();
        }

    }
}

