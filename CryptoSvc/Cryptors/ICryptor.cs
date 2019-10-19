namespace CryptoSvc.Cryptors
{
    public interface ICryptor
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
        void Configure(object cryptorInfo);
        (string, string) Handle(object cryptoInfo);
    }
}
