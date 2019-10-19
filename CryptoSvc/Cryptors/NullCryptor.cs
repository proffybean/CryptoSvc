using System;

namespace CryptoSvc.Cryptors
{
    public class NullCryptor : ICryptor
    {
        public void Configure(object cryptorInfo)
        {

        }

        public string Decrypt(string cipherText)
        {
            return null;
        }

        public string Encrypt(string clearText)
        {
            return null;
        }

        public (string, string) Handle(object cryptoInfo)
        {
            return ("Error in Your Cryptor", DateTime.MinValue.ToString());
        }
    }
}