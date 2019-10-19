using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using CryptoSvc.Models.Hashing;
using CryptoSvc.Hashers;

namespace CryptoSvc.Factories
{
    public class HashFactory
    {
        Dictionary<string, Type> _hashers;

        public HashFactory()
        {
            LoadTypesOfHashers();
        }

        private void LoadTypesOfHashers()
        {
            _hashers = new Dictionary<string, Type>();

            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                if (type.GetInterface(typeof(IHasher).ToString()) != null)
                {
                    _hashers.Add(type.Name.ToLower(), type);
                }
            }
        }

        public IHasher Instantiate(Operation operation, string key = null)
        {
            string hashType = operation.Alg.ToLower();

            if (hashType == "sha2")
            {
                hashType = "sha" + operation.Size;
            }

            if (key != null)
            {
                if (hashType == "sha2")
                {
                    hashType = "HMAC" + operation.Alg.ToUpper() + operation.Size;
                }
                else
                {
                    hashType = "HMAC" + operation.Alg.ToUpper();
                }
            }

            Type type = GetTypeToCreate(hashType);

            if (type == null)
            {
                return new NullHasher();
            }
            else if (type.Name.ToLower().StartsWith("hmac"))
            {
                return Activator.CreateInstance(type, key) as IHasher;
            }

            return Activator.CreateInstance(type) as IHasher;
        }

        private Type GetTypeToCreate(string hasherName)
        {
            foreach (var cryptor in _hashers)
            {
                if (cryptor.Key.StartsWith(hasherName.ToLower()))
                {
                    return _hashers[cryptor.Key];
                }
            }

            return null;
        }

    }
}