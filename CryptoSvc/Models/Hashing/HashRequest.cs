using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoSvc.Models.Hashing
{
    public class HashRequest
    {
        public ClearText ClearText { get; set; }
        public Hash Hash { get; set; }
        public Operation Operation { get; set; }
        public Info Info { get; set; }
    }

    public class ClearText
    {
        public string Text { get; set; }
        public string Encoding { get; set; }
    }

    public class Hash
    {
        public string Text { get; set; }
        public string Encoding { get; set; }
    }

    public class Operation
    {
        public string Task { get; set; }
        public string Alg { get; set; }
        public string Size { get; set; }
    }

    public class Info
    {
        public string Salt { get; set; }
        public bool Pepper { get; set; }
        public string Key { get; set; }
    }
}