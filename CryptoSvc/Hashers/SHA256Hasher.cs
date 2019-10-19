using System;
using System.Security.Cryptography;

namespace CryptoSvc.Hashers
{
    public class SHA256Hasher : IHasher
    {
        HashAlgorithm _hasher = null;
        public SHA256Hasher()
        {
            _hasher = SHA256.Create();
        }
        public byte[] Hash(byte[] buffer)
        {
            return _hasher.ComputeHash(buffer);
        }
    }
}