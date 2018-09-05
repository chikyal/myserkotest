using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using SerkoAPI.HelperClass;
using SerkoAPI.Models;

namespace SerkoAPI.Controllers
{
    public class AccountsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string txt)
        {
            SerkoResponse resp = new SerkoResponse()
            {
                response = new Response() { Vendor = new VendorDetail() },
                status = new Status() { success = false, detail = new Detail() { } }
            };
            VendorDetail Vendor = new VendorDetail { Expense = new ExpenseDetail() };
            try
            {
                // add xml tags to make it standard xml document
                txt = "<?xml version='1.0' encoding='utf-8' ?> " + "<body>" + txt + "</body>";

                // remove emails addresses from the text as it has '<' and '>'  which is interpreted as tags
                txt = Utility.RemoveEmails(txt);

                XDocument xmldoc = XDocument.Load(new System.IO.StringReader(txt));
                var odata = from o in xmldoc.Elements("body")
                            select o;



                double total, gst, amount;
                bool IsTotalNull = false;
                foreach (var item in odata)
                {

                    if (item.Element("vendor") != null)
                    {
                        Vendor.Vendor = item.Element("vendor").Value;
                    }
                    if (item.Element("description") != null)
                    {
                        Vendor.Description = item.Element("description").Value;
                    }
                    if (item.Element("date") != null)
                    {
                        Vendor.Date = item.Element("date").Value;
                    }

                    var odata1 = from o in item.Elements("expense")
                                 select o;
                    foreach (var item1 in odata1)
                    {
                        if (item1.Element("cost_centre") != null)
                        {
                            Vendor.Expense.CostCentre = item1.Element("cost_centre").Value;
                        }
                        if (string.IsNullOrEmpty(Vendor.Expense.CostCentre)) { Vendor.Expense.CostCentre = "UNKNOWN"; }
                        if (item1.Element("payment_method") != null)
                        {
                            Vendor.Expense.PaymentMethod = item1.Element("payment_method").Value;
                        }
                        if (item1.Element("total") != null)
                        {
                            total = Convert.ToDouble(item1.Element("total").Value);
                            Vendor.Expense.Total = total;
                            amount = total / 1.1; //10% gst
                            gst = total - amount;

                            Vendor.Expense.Amount = amount;
                            Vendor.Expense.GST = gst;
                        }
                        else
                        {
                            IsTotalNull = true;
                        }
                    }
                }

                resp.response.Vendor = Vendor;
                if (IsTotalNull)
                {
                    resp.status.success = false;
                    resp.status.detail = new Detail { errormessage = " total is null" };
                }
                else
                {
                    resp.status.success = true;
                }
            }
            catch (Exception)
            {
                resp.status.success = false;
                resp.status.detail = new Detail { errormessage = "problem with strating or ending tags " };
                resp.response.Vendor = Vendor;
               
            }
            return Ok(resp);
        }

    }
}
