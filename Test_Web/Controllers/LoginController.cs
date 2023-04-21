using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Library.Configs;
using Test_Library.Models;
using Test_Library.Security;
using Test_Model.Models;
using Test_Service.Admin;

namespace Test_Web.Controllers
{
    public class LoginController : Controller
    {
        private IUserService _UserService;
        public LoginController(IUserService _UserService)
        {
            this._UserService = _UserService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _UserService.GetAll();
            var model = new AuthModel();
            model.isAny = data.Any();
           
            //   model.Data_Service = Data_Service();
            model.AreaCode = "Admin";
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(AuthModel model)
        {
            var data = await _UserService.GetAll();
            model.isAny = data.Any();
            // model.Data_Service = Data_Service();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var objUser = new User();
            var result = await _UserService.Login(model, out objUser);
            if (result.isSuccess)
            {
                Session_Cookie(model, objUser);
                //lưu areacode vào cookie
                CookieAreaLastLogin(model.AreaCode);
                return RedirectToAction("Index", "Home" , new { Area = "Admin"});
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

        private void Session_Cookie(AuthModel model, User objUser)
        {
            var sessionUser = new SessionModel
            {
                UserId = objUser.Id,
                Avatar = objUser.UserAvatar,
                isAdmin = objUser.Admin,
                Name = objUser.Name,
                Username = objUser.Username
            };

            // mã hóa
            var serializeModel = JsonConvert.SerializeObject(sessionUser);
            var encryptModel = CryptoHelper.EncryptSessionCookie_User(serializeModel);

            //Lưu lại trong session
            HttpContext.Session.SetString(SessionConfig.Kz_UserSession, encryptModel);

            // Kiểm tra có lưu cookie k
            if (model.isRemember)
            {
                var option = new CookieOptions();
                option.Expires = DateTime.Now.AddMonths(1);
                HttpContext.Response.Cookies.Append(CookieConfig.Kz_UserCookie, encryptModel);
            }
         
        }
        //private void updateDatabase()
        //{
        //    throw new NotImplementedException();
        //}

        //private List<SelectListModel> Data_Service()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
