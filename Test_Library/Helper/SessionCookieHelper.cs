
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Library.Configs;
using Test_Library.Models;
using Test_Library.Security;

namespace Test_Library.Helper
{
    public class SessionCookieHelper
    {
        public static Task<SessionModel> CurrentUser(HttpContext HttpContext)
        {
            var model = new SessionModel();

            //Lấy session
            var sessionValue = HttpContext.Session.GetString(SessionConfig.Kz_UserSession);

            //Kiểm tra tồn tại => chuyển sang lấy cookie
            if (string.IsNullOrWhiteSpace(sessionValue))
            {
                //Kiểm tra cookie lấy cookie từ client gửi về
                var cookieValue = HttpContext.Request.Cookies[CookieConfig.Kz_UserCookie];

                if (string.IsNullOrWhiteSpace(cookieValue))
                {
                    model = null;
                }
                else
                {
                    //Giải mã
                    var decryptModel = CryptoHelper.DecryptSessionCookie_User(cookieValue);

                    if (!string.IsNullOrWhiteSpace(decryptModel))
                    {
                        model = JsonConvert.DeserializeObject<SessionModel>(decryptModel);

                        //Lưu lại thằng session, mã hóa lại thông tin
                        var encryptModel = CryptoHelper.EncryptSessionCookie_User(JsonConvert.SerializeObject(model));

                        HttpContext.Session.SetString(SessionConfig.Kz_UserSession, encryptModel);
                    }
                    else
                    {
                        model = null;
                    }
                }
            }
            else
            {
                //Giải mã
                var decryptModel = CryptoHelper.DecryptSessionCookie_User(sessionValue);

                if (!string.IsNullOrWhiteSpace(decryptModel))
                {
                    model = JsonConvert.DeserializeObject<SessionModel>(decryptModel);
                }
                else
                {
                    model = null;
                }


            }

            return Task.FromResult(model);
        }
    }
      

}

