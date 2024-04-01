using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Library.Configs;
using Test_Library.Extensions;
using Test_Library.Models;
using Test_Library.Security;
using Test_Model.Models;
using Test_Service.Admin;

namespace Test_Web.Controllers
{
    public class LoginController : Controller
    {
        private IMemberService _MemberService;
        public LoginController(IMemberService _MemberService)
        {
            this._MemberService = _MemberService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _MemberService.GetAll();
            var model = new AuthModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(AuthModel model)
        {
            var data = await _MemberService.GetAll();
       

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var objUser = new Member();
            var result = await _MemberService.Login(model, out objUser);
            if (result.isSuccess)
            {
                Session_Cookie(model, objUser);
                ////lưu areacode vào cookie
                //CookieAreaLastLogin(model.AreaCode);
                return RedirectToAction("Index", "Employees");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }

        private void CookieAreaLastLogin(string areaCode)
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Response.Cookies.Append(CookieConfig.Kz_UserCookie, areaCode);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();
            model.isAny = _MemberService.GetAll().Result.Any();
          

            return View(model);
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            model.isAny = _MemberService.GetAll().Result.Any();
       

            //Kiểm tra mật khẩu
            if (model.Password != model.RePassword)
            {
                ModelState.AddModelError("", "Mật khẩu không khớp");
                return View(model);
            }

            var salat = Guid.NewGuid().ToString();

            var obj = new Member()
            {              
                Name = model.Name,
                Username = model.Username,
                Password = model.Password.PasswordHashed(salat),
                PasswordSalat = salat
              
            };

            var result = _MemberService.Create(obj).Result;

            if (result.isSuccess)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
        }

        private void Session_Cookie(AuthModel model, Member objUser)
        {
            var sessionUser = new SessionModel
            {
                UserId = objUser.PersonID.ToString(),
                Avatar = "",     
                Name = objUser.Name,
                Username = objUser.Username
            };

            // mã hóa
            var serializeModel = JsonConvert.SerializeObject(sessionUser);
            var encryptModel = CryptoHelper.EncryptSessionCookie_User(serializeModel);

            //Lưu lại trong session
            HttpContext.Session.SetString(SessionConfig.Kz_UserSession, encryptModel);

        }
       
    }
}
