using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Library.Helper;
using Test_Library.Models;
using Test_Service.Admin.Database;

namespace Test_Web.Components.Sidebar
{
    public class SidebarViewComponent : ViewComponent
    {
        private IHttpContextAccessor HttpContextAccessor;
        private IMenuFunctionService _MenuFunctionService;

        public SidebarViewComponent (IHttpContextAccessor HttpContextAccessor , IMenuFunctionService _MenuFunctionService)
        {
            this.HttpContextAccessor = HttpContextAccessor;
            this._MenuFunctionService = _MenuFunctionService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string controllername, string actionname, string area ="") {


            var user = await SessionCookieHelper.CurrentUser(HttpContextAccessor.HttpContext);
            var model = new SidebarModel();
            model.ControllerName = controllername;
            model.ActionName = actionname;
            var data = await _MenuFunctionService.GetAllActiveByUserId(HttpContextAccessor.HttpContext, user, area);
            model.Data = data.ToList();
            model.CurrentView =  model.Data.FirstOrDefault(n => n.ControllerName.Equals( controllername) && n.ActionName.Equals(  actionname));
            model.Breadcrumb = await _MenuFunctionService.GetBreadcrumb(model.CurrentView != null ? model.CurrentView.Id : "", model.CurrentView != null ? model.CurrentView.ParentId : "", "");
            model.AreaCode = area;
            return View(model);
        }
    }
}
