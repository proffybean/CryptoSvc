using System;
using System.Security.Cryptography;

namespace CryptoSvc.Hashers
{
    public class MD5Hasher : IHasher
    {
        HashAlgorithm _hasher = null;
        public MD5Hasher()
        {
            _hasher = MD5.Create();
        }

        public byte[] Hash(byte[] buffer)
        {
            return _hasher.ComputeHash(buffer);
        }
    }
}