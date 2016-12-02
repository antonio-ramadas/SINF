using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class SalesOrderHistory
    {
        public string name { get; set; }

        public string date { get; set; }

        public string customerID { get; set; }

        public string salesID { get; set; }
    }
}