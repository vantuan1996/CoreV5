using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Library.Models;

namespace Test_Web.Components.Website
{
    public class WebsiteViewComponent : ViewComponent
    {
        public WebsiteViewComponent()
        {
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new WebsiteModel() {
                WebsiteName = await Test_Library.Helper.AppSettingHelper.GetStringFromAppSetting("WebConfig:WebsiteName")
            };
            return View(model);
        }
    }
}
