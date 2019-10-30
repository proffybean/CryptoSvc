using System;
using System.Security.Cryptography;

namespace CryptoSvc.Hashers
{
    public class HmacSHA512Hasher : IHasher
    {
        HMAC _hasher = null;

        public HmacSHA512Hasher(byte[] key)
        {
            _hasher = new HMACSHA512(key);
        }

        public HmacSHA512Hasher(string key) : this(Convert.FromBase64String(key)) { }

        public byte[] Hash(byte[] buffer)
        {
            return _hasher.ComputeHash(buffer);
        }
    }
}