﻿using Microsoft.AspNetCore.Mvc;
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

        //[CheckSessionCookie]
        public IActionResult Index()
        {
            return View();
        }


        private IActionResult NoAuthorized()
        {
            return View();
        }

    }
}
