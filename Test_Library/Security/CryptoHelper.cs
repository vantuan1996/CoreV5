using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;

namespace Test_Library.Security
{
    public class CryptoHelper
    {
        /// <summary>
        /// Mã hóa thông tin session người dùng đăng nhập
        /// </summary>
        /// <param name="userjson">Serialize model gốc</param>
        /// <returns>thông tin mã hóa để lưu</returns>
        public static string EncryptSessionCookie_User(string userjson)
        {
            var privatekey = SecurityModel.Session_Key;

            //Mã hóa lần 1
            userjson = CryptoProvider.SimpleEncryptWithPassword(userjson, privatekey);

            //Mã hóa lần 2
            return userjson;
        }
        /// <summary>
        /// Giải mã thông tin session người dùng đăng nhập
        /// </summary>
        /// <param name="cookiValue">model đã mã hóa</param>
        /// <returns></returns>
        public static string DecryptSessionCookie_User(string cookiValue)
        {
            var primarykey = SecurityModel.Session_Key;
            var userJson = CryptoProvider.SimpleDecryptWithPassword(cookiValue, primarykey);
            return userJson;           
        }

        public static string EncryptPass_User(string password, string passwordSalat)
        {
            throw new NotImplementedException();
        }
    }
}
