﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SFA_REST.Lib_Primavera.Model
{
    public class Stats
    {
        public class SalesMonth{
            public int month { get; set; }
            public double income { get; set; }
        } 

        public class SalesYear
        {
            public int year { get; set; }
            public List<SalesMonth> sales { get; set; }
            public double totalIncome { get; set; }
        }


    }
}