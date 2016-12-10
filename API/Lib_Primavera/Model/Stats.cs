using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SFA_REST.Lib_Primavera.Model
{
    public class Stats
    {
        public class IncomeMonth{
            public int month { get; set; }
            public double income { get; set; }
        } 

        public class IncomeYear
        {
            public int year { get; set; }
            public List<IncomeMonth> sales { get; set; }
            public double totalIncome { get; set; }
        }

        public class SalesMonth
        {
            public int month { get; set; }
            public double salesNumber { get; set; }
        }

        public class SalesYear
        {
            public int year { get; set; }
            public List<SalesMonth> sales { get; set; }
            public double salesNumber { get; set; }
        }

        public class IncomePerMonth
        {
            public int month { get; set; }
            public double incomePerMonth { get; set; }
        }

        public class IncomePerYear
        {
            public int year { get; set; }
            public List<IncomePerMonth> monthRates { get; set; }
            public double incomePerYear { get; set; }
        }

        public class TopCategory
        {
            public int numSales { get; set; }
            public double percent { get; set; }
            public Category category { get; set; }
        }

    }
}
