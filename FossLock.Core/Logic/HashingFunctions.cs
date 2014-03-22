using System;
using System.Security.Cryptography;
using System.Text;


namespace FossLock.Core.Logic
{
    /// <summary>
    /// 
    /// </summary>
    public class HashingFunctions
    {

        private HashingFunctions() { }


        /// <summary>
        /// Static function that creates a hash for a given 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
