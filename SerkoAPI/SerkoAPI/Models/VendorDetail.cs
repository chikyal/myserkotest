using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerkoAPI.Models
{
    public class VendorDetail
    {
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public ExpenseDetail Expense { get; set; }
    }
    public class ExpenseDetail
    {
        public string CostCentre { get; set; }
        public string PaymentMethod { get; set; }
        public double Total { get; set; }
        public double GST { get; set; }
        public double Amount { get; set; }
    }
}