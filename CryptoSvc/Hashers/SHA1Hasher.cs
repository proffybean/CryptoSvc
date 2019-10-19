using System;
using System.Security.Cryptography;

namespace CryptoSvc.Hashers
{
    public class SHA1Hasher : IHasher
    {
        HashAlgorithm _hasher = null;
        public SHA1Hasher()
        {
            _hasher = SHA1.Create();
        }
        public byte[] Hash(byte[] buffer)
        {
            return _hasher.ComputeHash(buffer);
        }
    }
}