using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        ///   POST method to authenticate an user
        /// </summary>
        /// <returns> true if the user and password match. False if they don't match </returns>
        [Route("api/login")]
        [HttpPost]
        public HttpResponseMessage Login(Lib_Primavera.Model.User user)
        {
            if(Lib_Primavera.PriIntegration.AuthenticateUser(user))
                return Request.CreateResponse(HttpStatusCode.Created);
            else return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        ///   PUT method to register an user
        /// </summary>
        /// <returns> true if the user and password match. False if they don't match </returns>
        [Route("api/register/{user}/{email}/{password}/{password_confirmation}")]
        [HttpGet]
        public bool Register(string user, string email, string password, string password_confirmation)
        {
            if (!password.Equals(password_confirmation))
                return false;

            return false;
        }

        
    }
}