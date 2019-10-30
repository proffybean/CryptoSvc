using System;
using System.Security.Cryptography;

namespace CryptoSvc.Hashers
{
    public class HmacSHA256Hasher : IHasher
    {
        HMAC _hasher = null;

        public HmacSHA256Hasher(byte[] key)
        {
            _hasher = new HMACSHA256(key);
        }

        public HmacSHA256Hasher(string key) : this(Convert.FromBase64String(key)) { }

        public byte[] Hash(byte[] buffer)
        {
            return _hasher.ComputeHash(buffer);
        }
    }
}