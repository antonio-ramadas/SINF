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

        public string productID { get; set; }

        public string quantity { get; set; }
    }
}