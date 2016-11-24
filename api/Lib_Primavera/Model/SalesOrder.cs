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

        public string Entidade
        {
            get;
            set;
        }

        public int NumDoc
        {
            get;
            set;
        }

        public DateTime Data
        {
            get;
            set;
        }

        public double TotalMerc
        {
            get;
            set;
        }

        public string Serie
        {
            get;
            set;
        }

        public string Responsavel
        {
            get;
            set;
        }

        public string Client
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