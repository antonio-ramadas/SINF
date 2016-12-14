using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using SFA_REST.Lib_Primavera.Model;

namespace SFA_REST.Lib_Primavera.Model
{
    public class User
    {
        public string username { get; set; }

        public string password { get; set; }
    }
}