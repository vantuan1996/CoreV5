using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Library.Configs;
using Test_Library.Functions;
using Test_Library.Models;
using Test_Model.Models;
using Test_Library.Helper;
namespace Test_Library.Helper
{
    public class AuthHelper
    {

        // phân quyền để lấy tất cả các menu mà k phải là admin
        public static Task<List<MenuFunction>> MenuFunctionByUserId(SessionModel model, HttpContext httpContext, string area )
        {
            var identify = string.Format(CacheConfig.Kz_User_MenuFunctionCache_Key, model.UserId, SecurityModel.Cache_Key);
            var modelCache = new List<MenuFunction>();

            var existed = CacheFunction.TryGet<List<MenuFunction>>(httpContext, identify, out modelCache);
            if (existed == false)
            {
                if (model.isAdmin)
                {
                    var str = new StringBuilder();
                    str.AppendLine("Select * from [MenuFunction]");
                    if (!string.IsNullOrEmpty(area))
                    {
                        str.AppendLine(string.Format("Where MenuGroupListId Like N'%{0}%'", area));
                    }
                    modelCache = Test_Library.Helper.DatabaseHelper.ExcuteCommandToList<MenuFunction>(str.ToString());
                }
                else
                {
                    // lấy bản ghi trong bản role theo user
                    var str = string.Format("Select * from userrole  where UserId = '{0}'", model.UserId);
                    var  userRoles = Test_Library.Helper.DatabaseHelper.ExcuteCommandToList<UserRole>(str.ToString());

                    var lstRole = new List<string>();
                    foreach (var item in userRoles)
                    {
                        lstRole.Add(item.RoleId);
                    }

                    // lấy danh sách menu theo role => danh sách menu
                    var strRoleMenu = string.Format("Select * from rolemenu  where RoleId IN ({0}) ", userRoles.Any() ? string.Join(',',lstRole) : "'0'");
                    var RoleMenus  = Test_Library.Helper.DatabaseHelper.ExcuteCommandToList<RoleMenu>(strRoleMenu.ToString());

                    //Lấy danh sách menu quyền
                    var menuids = "";
                    var count1 = 0;
                    foreach (var item in RoleMenus)
                    {
                        count1++;
                        menuids += string.Format("'{0}'{1}", item.MenuId, count1 == RoleMenus.Count ? "" : ",");
                    }
                    var strmenu = string.Format("Select * from menufunction  where  Active = 1 and  Id IN ({0}) ", string.IsNullOrWhiteSpace(menuids) ? "'0'" : menuids);
                    if (!string.IsNullOrWhiteSpace(area))
                    {
                        strmenu += string.Format(" AND MenuGroupListId LIKE '%{0}%'", area);
                    }
                    modelCache = Test_Library.Helper.DatabaseHelper.ExcuteCommandToList<MenuFunction>(strmenu.ToString());
                }

                //save vào cachec
                if (modelCache ==  null)
                {
                    modelCache = new List<MenuFunction>();
                }
                CacheFunction.Add<List<MenuFunction>>(httpContext, identify, modelCache, CacheConfig.Kz_User_MenuFunctionCache_Time);
            }

            return Task.FromResult(modelCache);
        }

        public static Task<AuthActionModel> CheckAuthAction(string controllerName, HttpContext httpContext,string area = "")
        {
            var auth = new AuthActionModel();
            var curretUser = SessionCookieHelper.CurrentUser(httpContext).Result;
            if (curretUser != null)
            {
                // lấy menu theo role của user
                var menus = MenuFunctionByUserId(curretUser, httpContext, area).Result;
                if (curretUser.isAdmin)
                {
                    auth.Create_Auth = 1;
                    auth.Delete_Auth = 1;
                    auth.Update_Auth = 1;


                }
                else
                {
                    var objCreate = menus.FirstOrDefault(n => n.ControllerName == controllerName && n.ActionName == "Create");
                    if (objCreate != null)
                    {
                        auth.Create_Auth = 1;
                    }
                    var update  = menus.FirstOrDefault(n => n.ControllerName == controllerName && n.ActionName == "Update");
                    if (update != null)
                    {
                        auth.Update_Auth = 1;
                    }
                    var delte = menus.FirstOrDefault(n => n.ControllerName == controllerName && n.ActionName == "Delete");
                    if (delte != null)
                    {
                        auth.Delete_Auth = 1;
                    }
                }
            }
            return Task.FromResult(auth); ;
        }
    }
}
