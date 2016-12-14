using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class Task
    {
        
        public string id { get; set;}
            
        public string location { get; set; }
            
        public string salesmanId { get; set; }
            
        public string clientId { get; set; }
            
        public string summary { get; set; }
            
        public string description { get; set; }
            
        public DateTime beginDate { get; set; }

        public DateTime endDate { get; set; }

        public int priority { get; set; }
        
    }
}