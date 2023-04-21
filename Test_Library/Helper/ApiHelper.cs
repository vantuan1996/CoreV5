using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Library.Models;

namespace Test_Library.Helper
{
   public class ApiHelper
    {
        public static HttpClient client;
        static ApiHelper()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
        }

        public static string GenerateJSON_MobileToken(string userid)
        {
            //
            var now = DateTime.Now;
            var expire = now.AddDays(1);

            //
            var Issuer = AppSettingHelper.GetStringFromFileJson("appsettings", "Jwt:Issuer_Mobile").Result;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityModel.Mobile_Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Issuer,
                Issuer,
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userid),
                },
                expires: expire,
                signingCredentials: credentials);

            var mo = new TokenModel()
            {
                Identifier = userid,
                Expires_In = (int)(expire - now).TotalMinutes,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return JsonConvert.SerializeObject(mo);
        }
    }
}
