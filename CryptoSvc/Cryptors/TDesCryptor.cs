using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using CryptoSvc.Models.Crypto;
using CryptoSvc.Extensions;

namespace CryptoSvc.Cryptors
{
    public class TDesCryptor : ICryptor
    {
        TripleDESCryptoServiceProvider _tdes;

        public TDesCryptor()
        {
            _tdes = new TripleDESCryptoServiceProvider();
        }

        public void Configure(object cryptorInfo)
        {
            CryptRequest cryptoDto = cryptorInfo as CryptRequest;

            _tdes.Mode = (CipherMode)Enum.Parse(typeof(CipherMode), cryptoDto.AlgInfo.CipherMode);
            _tdes.Padding = (PaddingMode)Enum.Parse(typeof(PaddingMode), cryptoDto.AlgInfo.Padding);

            _tdes.Key = Convert.FromBase64String(cryptoDto.AlgInfo.Key);
            _tdes.IV = Convert.FromBase64String(cryptoDto.AlgInfo.IV);
        }

        public string Decrypt(string cipherText)
        {
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, _tdes.CreateDecryptor(), CryptoStreamMode.Write);
                byte[] dataToDecrypt = Convert.FromBase64String(cipherText);
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public string Encrypt(string clearText)
        {
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, _tdes.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(clearText);
                cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public (string, string) Handle(object cryptoInfo)
        {
            CryptRequest cryptDto = cryptoInfo as CryptRequest;
            Stopwatch stopwatch = new Stopwatch();
            string returnValue = null;

            stopwatch.Start();
            if (cryptDto.Operation.Task.Sanitize() == "decrypt")
            {
                returnValue = Decrypt(cryptDto.CipherText.Text);
            }
            else if (cryptDto.Operation.Task.Sanitize() == "encrypt")
            {
                returnValue = Encrypt(cryptDto.ClearText.Text);
            }
            stopwatch.Stop();

            return (returnValue, stopwatch.Elapsed.ToString());
        }
    }
}