using System;
using System.Security.Cryptography;

namespace CryptoSvc.Hashers
{
    public class HmacSHA1Hasher : IHasher
    {
        HMAC _hasher = null;

        public HmacSHA1Hasher(byte[] key)
        {
            _hasher = new HMACSHA1(key);
        }

        public HmacSHA1Hasher(string key) : this(Convert.FromBase64String(key)){}

        public byte[] Hash(byte[] buffer)
        {
            return _hasher.ComputeHash(buffer);
        }
    }
}