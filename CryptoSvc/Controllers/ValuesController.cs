using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CryptoSvc.Models.Crypto;
using CryptoSvc.Factories;
using CryptoSvc.Cryptors;

namespace CryptoSvc.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]CryptRequest cryptDto)
        {
            ICryptor cryptor = new CryptorFactory().Instantiate(cryptDto.Operation.Algorithm);

            cryptor.Configure(cryptDto);

            var values = cryptor.Handle(cryptDto);

            var response = new EncryptoResponseDto()
            {
                CipherText = values.Item1,
                Encoding = cryptDto.CipherText.Encoding,
                Time = values.Item2
            };

            return Ok(response);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
