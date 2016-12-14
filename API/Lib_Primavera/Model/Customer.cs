using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class Customer
    {
        public string id { get; set; }

        public string name { get; set; }

        public string address { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public string phoneNumber { get; set; }

        public string nationality { get; set; }

        public string dateOfBirth { get; set; }

        public string nif { get; set; }

        public string notes { get; set; }

        public List<string> labels { get; set; }
    }
}