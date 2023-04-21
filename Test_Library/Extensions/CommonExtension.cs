using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Library.Extensions
{
    public static class CommonExtension
    {
        public static string PasswordHashed(this string password, string passwordSalt)
        {
            string concatSaltAndPassword = string.Concat(password, passwordSalt);

            var securityKey = Encoding.UTF8.GetBytes(concatSaltAndPassword);

            var sha1 = System.Security.Cryptography.SHA1.Create();

            var hash = sha1.ComputeHash(securityKey);
            var s = BitConverter.ToString(hash);
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
