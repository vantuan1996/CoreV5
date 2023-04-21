using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Library.Configs;

namespace Test_Web.Areas.Admin
{
    [Area(AreaConfig.Admin)]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(int page = 1)
        {
            return await Task.FromResult( View());
        }
    }
}
