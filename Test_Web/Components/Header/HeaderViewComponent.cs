using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Library.Configs;
using Test_Library.Helper;
using Test_Library.Models;
using Test_Library.Security;

namespace Test_Web.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public HeaderViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var user = await SessionCookieHelper.CurrentUser(HttpContext);
            // trả model session user
            var sessionValue = HttpContext.Session.GetString(SessionConfig.Kz_UserSession);

            ViewBag.Lstlanguge = new SelectListModel_Chosen()
            {
                Data = await StaticList.ListLanguage(),
                IdSelectList = "langCode_Parking",
                isMultiSelect = false,
                Selecteds = !string.IsNullOrEmpty(sessionValue) ? sessionValue : "vi"
            };
            return View(user);

           
        }
    }
}
