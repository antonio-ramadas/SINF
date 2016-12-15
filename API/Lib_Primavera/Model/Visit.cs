using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class Visit
    {
        public string id { get; set; }

        public string representativeId { get; set; }

        public string customerId { get; set; }

        public string location { get; set; }

        public string description { get; set; }

        public string summary { get; set; }

        public DateTime beginDate { get; set; }

        public DateTime endDate { get; set; }

        public int priority { get; set; }


    }
}