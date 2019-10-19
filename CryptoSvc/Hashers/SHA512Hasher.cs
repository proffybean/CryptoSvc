using System;
using System.Security.Cryptography;

namespace CryptoSvc.Hashers
{
    public class SHA512Hasher : IHasher
    {
        HashAlgorithm _hasher = null;
        public SHA512Hasher()
        {
            _hasher = SHA512.Create();
        }
        public byte[] Hash(byte[] buffer)
        {
            return _hasher.ComputeHash(buffer);
        }
    }
}