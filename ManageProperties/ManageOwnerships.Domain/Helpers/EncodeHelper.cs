using System;
using System.Security.Cryptography;
using System.Text;

namespace ManageOwnerships.Domain.Services.Classes
{
    internal static class EncodeHelper
    {
        /// <summary>
        /// Encript password
        /// </summary>
        /// <param name="pass">Password text</param>
        /// <param name="salt">Salt or key</param>
        /// <returns></returns>
        public static string EncodePassword(string pass, string salt)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return EncodePasswordMd5(Convert.ToBase64String(inArray));
        }

        /// <summary>
        /// Encrypt using MD5
        /// </summary>
        /// <param name="pass">Password text</param>
        /// <returns></returns>
        private static string EncodePasswordMd5(string pass)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] originalBytes = Encoding.Default.GetBytes(pass);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
    }
}
