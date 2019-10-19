using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using CryptoSvc.Cryptors;
using CryptoSvc.Extensions;

namespace CryptoSvc.Factories
{
    public class CryptorFactory
    {
        private Dictionary<string, Type> _cryptors;

        public CryptorFactory()
        {
            LoadTypesOfCryptors();
        }

        void LoadTypesOfCryptors()
        {
            _cryptors = new Dictionary<string, Type>();

            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                if (type.GetInterface(typeof(ICryptor).ToString()) != null)
                {
                    _cryptors.Add(type.Name.ToLower(), type);
                }
            }
        }

        public ICryptor Instantiate(string cryptorName)
        {
            Type type = GetTypeToCreate(cryptorName);

            return (type == null) ? new NullCryptor() : Activator.CreateInstance(type) as ICryptor;
        }

        Type GetTypeToCreate(string cryptorName)
        {
            foreach (var cryptor in _cryptors)
            {
                if (cryptor.Key.StartsWith(cryptorName.ToLower()))
                {
                    return _cryptors[cryptor.Key];
                }
            }

            return null;
        }

        [Obsolete]
        public ICryptor Create(string algorithm)
        {
            ICryptor cryptor;

            switch (algorithm.Sanitize())
            {
                case "aes":
                    cryptor = new AesCryptor();
                    break;
                case "des":
                    cryptor = new DesCryptor();
                    break;
                case "3des":
                    cryptor = new TDesCryptor();
                    break;
                default:
                    throw new Exception();
            }

            return cryptor;
        }
    }
}