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

        public string model
        {
            get;
            set;
        }

        public double price
        {
            get;
            set;
        }

        public float vat 
        {
            get;
            set;
        }

        public string description
        {
            get;
            set;
        }

        public double quantity
        {
            get;
            set;
        }

        public List<WarehouseProduct> warehouses
        {
            get;
            set;
        }
        
        public string brand
        {
            get;
            set;
        }

        public double salesCount
        {
            get;
            set;
        }

        public string category
        {
            get;
            set;
        }

        public string subCategory
        {
            get;
            set;
        }

        public string imageURL
        {
            get;
            set;
        }

        public string amountSold
        {
            get;
            set;
        }
    }
}