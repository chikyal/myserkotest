using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SerkoAPI.Models;
namespace SerkoAPI.HelperClass
{
    public class SerkoResponse
    {
        public Response response { get; set; }
        public Status status { get; set; }
    }
    public class Response
    {
        public VendorDetail Vendor { get; set; }
    }

    public class Status
    {
        public Detail detail { get; set; }
        public bool success { get; set; }
    }

    public class Detail
    {
        public string operation { get; set; }
        public string errormessage { get; set; }
        public string error { get; set; }
        public string errorcode { get; set; }
    }
}