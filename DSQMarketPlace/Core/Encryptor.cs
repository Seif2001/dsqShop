using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class AesCypherHelpers
    {
        //public static (string key, string iv) GenerateRSACyphers()
        //{

        //    using var aesAlgorithm = Aes.Create();
        //    aesAlgorithm.KeySize = (int)lenght;
        //    // AES Proposal: Rijndael: BlockSize always 128 bit
        //    aesAlgorithm.GenerateKey();
        //    aesAlgorithm.GenerateIV();

        //    return (Convert.ToBase64String(aesAlgorithm.Key), Convert.ToBase64String(aesAlgorithm.IV));
        //}


        public static string Encrypt(string plainText, string keyBase64, string vectorBase64)
        {
            using Aes aesAlgorithm = Aes.Create();
            //set the parameters with out keyword
            aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
            aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

            // Create encryptor object
            ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

            byte[] encryptedData;

            //Encryption will be done in a memory stream through a CryptoStream object
            using MemoryStream ms = new MemoryStream();
            using CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            encryptedData = ms.ToArray();

            return Convert.ToBase64String(encryptedData);
        }


        public static string Decrypt(string cipherText, string keyBase64, string vectorBase64)
        {
            using Aes aesAlgorithm = Aes.Create();
            aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
            aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

            var decryptor = aesAlgorithm.CreateDecryptor();

            var cipher = Convert.FromBase64String(cipherText);

            using MemoryStream ms = new MemoryStream(cipher);
            using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
