using CryptoSvc.Models;
using CryptoSvc.Extensions;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;
using CryptoSvc.Models.Crypto;

namespace CryptoSvc.Cryptors
{
    public class AesCryptor : ICryptor
    {
        AesCryptoServiceProvider aes;

        public AesCryptor()
        {
            aes = new AesCryptoServiceProvider();
        }

        public string Encrypt(string clearText)
        {
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] dataToEncrypt = Encoding.UTF8.GetBytes(clearText);
                cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public string Decrypt(string cipherText)
        {
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                byte[] dataToDecrypt = Convert.FromBase64String(cipherText);
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();

                //string base64Decoded = Convert.ToBase64String(memoryStream.ToArray());
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public void Configure(object cryptorInfo)
        {
            CryptRequest cryptoDto = cryptorInfo as CryptRequest;

            // TODO: implicit conversion?
            //aes.Mode = (CipherMode)info.CipherMode;

            // TODO: parse the Mode from Block Chain, or Electronic code book, etc...
            aes.Mode = (CipherMode)Enum.Parse(typeof(CipherMode), cryptoDto.AlgInfo.CipherMode);
            aes.Padding = (PaddingMode)Enum.Parse(typeof(PaddingMode), cryptoDto.AlgInfo.Padding);
            //aes.Key = Encoding.UTF8.GetBytes(info.Key);
            //aes.IV = Encoding.UTF8.GetBytes(info.IV);
            aes.Key = Convert.FromBase64String(cryptoDto.AlgInfo.Key);
            aes.IV = Convert.FromBase64String(cryptoDto.AlgInfo.IV);
        }

        public (string, string) Handle(object cryptoInfo)
        {
            CryptRequest cryptDto = cryptoInfo as CryptRequest;
            Stopwatch stopwatch = new Stopwatch();
            string returnValue = null;

            if (cryptDto.Operation.Task.Sanitize() == "decrypt")
            {
                stopwatch.Start();
                returnValue = Decrypt(cryptDto.CipherText.Text);
                stopwatch.Stop();
            }
            else if (cryptDto.Operation.Task.Sanitize() == "encrypt")
            {
                stopwatch.Start();
                returnValue = Encrypt(cryptDto.ClearText.Text);
                stopwatch.Stop();
            }

            return (returnValue, stopwatch.Elapsed.ToString());
        }
    }
}