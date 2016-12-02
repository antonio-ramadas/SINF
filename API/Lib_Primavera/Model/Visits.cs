using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class Visits
    {
        public string id { get; set; }

        public string representativeID { get; set; }

        public string customerID { get; set; }

        public string date { get; set; }

        public string summary { get; set; }

        public string notes { get; set; }
    }
}