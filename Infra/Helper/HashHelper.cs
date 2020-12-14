using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helper
{
    public static class HashHelper
    {
        private static string ByteArrayToHexString(byte[] Bytes)
        {
            string HexAlphabet = "0123456789ABCDEF";
            StringBuilder Result = new StringBuilder();
            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }
            return Result.ToString();
        }
        private static string getHMAC(string signatureString, string secretKey)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA1 hmac = new HMACSHA1(keyByte);
            byte[] messageBytes = encoding.GetBytes(signatureString);
            byte[] hashMessage = hmac.ComputeHash(messageBytes);
            return ByteArrayToHexString(hashMessage); //Convert Byte to String.  
        }
        //public static string GetHashValue(string SignatureString)
        //{
        //    string SecretKey = Vault.CareMeHashKey; //jc
        //    string HashValue = getHMAC(SignatureString, SecretKey);
        //    HashValue = HashValue.ToUpper();
        //    return HashValue;
        //}

        public static string GetHashValue(string SignatureString, string HashKey)
        {
            string SecretKey = HashKey;
            string HashValue = getHMAC(SignatureString, SecretKey);
            HashValue = HashValue.ToUpper();
            return HashValue;
        }

        public static string getHMACV2(string signatureString, string secretKey)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey); HMACSHA1 hmac = new
            HMACSHA1(keyByte);
            byte[] messageBytes = encoding.GetBytes(signatureString); byte[]
            hashmessage = hmac.ComputeHash(messageBytes); return
            ByteArrayToHexString(hashmessage);
        }
        public static string ByteArrayToHexStringV2(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(); string
            HexAlphabet = "0123456789ABCDEF"; foreach
            (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }
            return Result.ToString();
        }
    }
}
