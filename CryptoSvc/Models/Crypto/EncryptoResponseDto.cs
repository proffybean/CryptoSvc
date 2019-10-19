using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace CryptoSvc.Models.Crypto
{
    [DataContract]
    public class EncryptoResponseDto
    {
        [DataMember(EmitDefaultValue = false)]
        public string CipherText { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ClearText { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Encoding { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Time { get; set; }
    }
}