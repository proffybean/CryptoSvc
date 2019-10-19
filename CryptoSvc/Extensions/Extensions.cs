using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoSvc.Extensions
{
    public static class Extensions
    {
        public static string Sanitize(this string s)
        {
            return s.Trim();
        }
    }
}