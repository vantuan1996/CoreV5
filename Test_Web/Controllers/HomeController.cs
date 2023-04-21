using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test_Service.Admin.Database;


namespace Test_Web.Controllers
{
    public class HomeController : Controller
    {
        private IMenuFunctionService _MenuFunctionService;

        public HomeController(IMenuFunctionService _MenuFunctionService)
        {
            this._MenuFunctionService = _MenuFunctionService;
        }

        //[CheckSessionCookie]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NotAuthorize()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        private IActionResult NoAuthorized()
        {
            return View();
        }


        //public async Task<IActionResult> GetLicense()
        //{
        //    var message = await FunctionHelper.FetchLicenseData();

        //    return Json(message);
        //}

        //public async Task<IActionResult> GetText(string path)
        //{
        //    var text = await LanguageHelper.GetLanguageText(path);

        //    return Json(text);
        //}
    
}
}
