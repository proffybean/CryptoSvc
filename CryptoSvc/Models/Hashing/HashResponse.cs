using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoSvc.Models.Hashing
{
    public class HashResponse
    {
        public string Hex { get; set; }
        public string Base64 { get; set; }
        public string Timestamp { get; set; }
    }
}