using System;
using System.Text;
using System.Web.Http;
using CryptoSvc.Models.Hashing;
using CryptoSvc.Hashers;
using CryptoSvc.Factories;
using System.Security.Cryptography;

namespace CryptoSvc.Controllers
{
    public class HashController : ApiController
    {
        public IHttpActionResult Post([FromBody] HashRequest hashRequest)
        {
            IHasher hasher = new HashFactory().Instantiate(hashRequest.Operation, hashRequest.Info.Key);

            byte[] stringToHash = CalculateStringToHash(hashRequest);

            byte[] hash = hasher.Hash(stringToHash);

            HashResponse hashResponse = new HashResponse
            {
                Hex = BitConverter.ToString(hash).Replace("-", ""),
                Base64 = Convert.ToBase64String(hash),
                Timestamp = DateTime.Now.ToString()
            };

            return Ok(hashResponse);
        }

        private byte[] CalculateStringToHash(HashRequest hashRequest)
        {
            string salt = hashRequest.Info.Salt;

            byte[] pepper = new byte[1];
            string pepperString = null;
            if (hashRequest.Info.Pepper)
            {
                using(var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(pepper);
                    pepperString = Encoding.UTF8.GetString(pepper);
                }
            }

            byte[] buffer = Encoding.UTF8.GetBytes(salt + hashRequest.ClearText.Text + pepperString);
            return buffer;
        }
    }
}
