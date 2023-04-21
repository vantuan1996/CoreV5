using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Model.Models;
using Test_Service.Admin.Database;

namespace Test_Web.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuFontendController : ControllerBase
    {
        private ImenuFontendService _menuFontendService;
        public MenuFontendController(ImenuFontendService _menuFontendService)
        {
            this._menuFontendService = _menuFontendService;
        }
        [HttpGet("getAllMenuApp")]
        public async Task<menuFontend_Custom> GetAllMenuFontend(string name)
        {
            var objParent = await _menuFontendService.getAllByParentId(name);
            return await Task.FromResult(  objParent);
        }
    }
}
