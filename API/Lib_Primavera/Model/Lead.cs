using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class Lead
    {
        public string id { get; set; }

        public string customerID { get; set; }

        public string expirationDate { get; set; }

        public string description { get; set; }

        public string summary { get; set; }

        public string value { get; set; }

        public string salesRepID { get; set; }

        public string type { get; set; }
    }
}