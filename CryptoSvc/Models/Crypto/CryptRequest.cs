using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoSvc.Models.Crypto
{
    public class CryptRequest
    {
        public Cleartext ClearText { get; set; }
        public Ciphertext CipherText { get; set; }
        public Operation Operation { get; set; }
        public Alginfo AlgInfo { get; set; }
    }

    public class Cleartext
    {
        public string Text { get; set; }
        public string Encoding { get; set; }
    }

    public class Ciphertext
    {
        public string Text { get; set; }
        public string Encoding { get; set; }
    }

    public class Operation
    {
        public string Task { get; set; }
        public string Algorithm { get; set; }
    }

    public class Alginfo
    {
        public string Key { get; set; }
        public string IV { get; set; }
        public string Padding { get; set; }
        public string CipherMode { get; set; }
    }
}