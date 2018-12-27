using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
namespace BookStore.Authentication
{
    public class RSAHelper
    {
        public Tuple<string, string> GenerateRSAKeys()
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            string publicKey = provider.ToXmlString(false);
            string privateKey = provider.ToXmlString(true);
            return Tuple.Create<string, string>(publicKey, privateKey);
        }

        public string Encrypt(string publicKey, string originalCon)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            string encryptCon = Convert.ToBase64String(provider.Encrypt(Encoding.UTF8.GetBytes(originalCon), false));
            return encryptCon;
        }

        public string Decrypt(string privateKey, string encryptCon)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            string originalCon = Encoding.UTF8.GetString(provider.Decrypt(Convert.FromBase64String(encryptCon), false));
            return originalCon;
        }
    }
}