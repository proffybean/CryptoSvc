using System;
using System.Security.Cryptography;

namespace CryptoSvc.Hashers
{
    public class NullHasher : IHasher
    {
        HashAlgorithm _hasher = null;

        public NullHasher() { }

        public byte[] Hash(byte[] buffer)
        {
            return null;
        }
    }
}