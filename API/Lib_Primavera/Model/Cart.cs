using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class Cart
    {
        public string id { get; set; }

        public string customerId { get; set; }

        public string creationDate { get; set; }

        public string expirationDate { get; set; }

        public string description { get; set; }

        public string summary { get; set; }

        public string value { get; set; }

        public string salesRepId { get; set; }

        public string type { get; set; }

        public class CartLine
        {
            public string id { get; set; }

            public string numberProposal { get; set; }

            public string numberLine { get; set; }

            public string productId { get; set; }

            public string description { get; set; }

            public string quantity { get; set; }

            public string costPrice { get; set; }

            public string sellingPrice { get; set; }
        }

        public List<Model.Cart.CartLine> lines { get; set; }
    }
}