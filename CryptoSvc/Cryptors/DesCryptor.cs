using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using CryptoSvc.Extensions;
using CryptoSvc.Models.Crypto;

namespace CryptoSvc.Cryptors
{
    public class DesCryptor : ICryptor
    {
        DESCryptoServiceProvider des;

        public DesCryptor()
        {
            des = new DESCryptoServiceProvider();
        }

        public void Configure(object cryptorInfo)
        {
            CryptRequest cryptoDto = cryptorInfo as CryptRequest;

            des.Mode = (CipherMode)Enum.Parse(typeof(CipherMode), cryptoDto.AlgInfo.CipherMode);
            des.Padding = (PaddingMode)Enum.Parse(typeof(PaddingMode), cryptoDto.AlgInfo.Padding);
            des.Key = Convert.FromBase64String(cryptoDto.AlgInfo.Key);
            des.IV = Convert.FromBase64String(cryptoDto.AlgInfo.IV);
        }

        public string Decrypt(string cipherText)
        {
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                byte[] dataToDecrypt = Convert.FromBase64String(cipherText);
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();

                // Encoding.UTF8.GetString(memoryStream.ToArray());
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public string Encrypt(string clearText)
        {
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
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