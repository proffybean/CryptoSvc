using System;
using System.Security.Cryptography;

namespace CryptoSvc.Hashers
{
    public class HmacMd5Hasher : IHasher
    {
        HMAC _hasher = null;

        public HmacMd5Hasher(byte[] key)
        {
            _hasher = new HMACMD5(key);
        }
        public byte[] Hash(byte[] buffer)
        {
            throw new NotImplementedException();
        }
    }
}