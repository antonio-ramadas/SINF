using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Controllers
{
    public class User : ApiController
    {
        /// <summary>
        ///   POST method to authenticate an user
        /// </summary>
        /// <returns> true if the user and password match. False if they don't match </returns>
        [Route("api/login/{user}/{password}")]
        [HttpGet]
        public bool Login(string user, string password)
        {
            return false;
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