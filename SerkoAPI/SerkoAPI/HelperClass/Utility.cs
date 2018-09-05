using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SerkoAPI.HelperClass
{
    public static  class Utility
    {
        public static string RemoveEmails(string data)
        {
            Regex emailRegex = new Regex(@"<\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*>", RegexOptions.IgnoreCase);
            //find items that matches with our pattern
            MatchCollection emailMatches = emailRegex.Matches(data);

            foreach (Match emailMatch in emailMatches)
            {
                data = data.Replace(emailMatch.Value, "");
            }
            return data;
        }
    }
}