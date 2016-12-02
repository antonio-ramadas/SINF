using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class SalesOrder
    {

        public string id
        {
            get;
            set;
        }

        public string entity
        {
            get;
            set;
        }

        public int numDoc
        {
            get;
            set;
        }

        public DateTime date
        {
            get;
            set;
        }

        public double totalMerc
        {
            get;
            set;
        }

        public string serie
        {
            get;
            set;
        }

        public string salesRep
        {
            get;
            set;
        }

        public List<Model.LinhaDocVenda> LinhasDoc

        {
            get;
            set;
        }
 

    }
}