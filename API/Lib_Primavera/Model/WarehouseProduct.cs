using System;
using System.Web;

namespace SFA_REST.Lib_Primavera.Model
{
    public class WarehouseProduct
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        /// 


        public string id
        {
            get;
            set;
        }

        public double quantity
        {
            get;
            set;
        }
        
    }
}
