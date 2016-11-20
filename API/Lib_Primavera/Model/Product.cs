using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class Product
    {

        public string id
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public string model
        {
            get;
            set;
        }

        public float price
        {
            get;
            set;
        }

        public float vat 
        {
            get;
            set;
        }

        public string categoryId
        {
            get;
            set;
        }

        public string description
        {
            get;
            set;
        }

        public int quantity
        {
            get;
            set;
        }

        /*public string[] wharehouses
        {
            get;
            set;
        }

        public string productCode
        {
            get;
            set;
        }*/

        public string brand
        {
            get;
            set;
        }

    }
}