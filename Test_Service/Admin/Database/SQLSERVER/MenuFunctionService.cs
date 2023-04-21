using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Data.Repository;
using Test_Library.Helper;
using Test_Library.Models;
using Test_Model.Models;

namespace Test_Service.Admin.Database.SQLSERVER
{
    public class MenuFunctionService : IMenuFunctionService
    {
        private IRoleMenuRepository _RoleMenuRepository;
        private IMenuFunctionRepository _MenuFunctionRepository;
        private IMenuFunctionConfigRepository _MenuFunctionConfigRepository;
        public MenuFunctionService(IRoleMenuRepository _RoleMenuRepository, IMenuFunctionRepository _MenuFunctionRepository, IMenuFunctionConfigRepository _MenuFunctionConfigRepository)
        {
            this._RoleMenuRepository = _RoleMenuRepository;
            this._MenuFunctionRepository = _MenuFunctionRepository;
            this._MenuFunctionConfigRepository = _MenuFunctionConfigRepository;
        }

        public async Task<MessageReport> CreateMap(RoleMenu model)
        {
            return await _RoleMenuRepository.Add(model);
        }

        public async Task<MessageReport> DeleteMap(string roleid, string area = "")
        {
            var ids = await GetMenuFunctionsByArea(area);

            var t = from n in _RoleMenuRepository.Table
                    where n.RoleId == roleid && ids.Contains(n.MenuId)
                    select n;

            return await _RoleMenuRepository.Remove_Multi(t);
        }

        private async Task<List<string>> GetMenuFunctionsByArea(string area = "")
        {
            var objArea = StaticList.GroupMenuList().FirstOrDefault(n => n.AreaName == area);

            var query = from n in _MenuFunctionRepository.Table
                        where n.MenuGroupListId != null
                        select n;

            if (objArea != null)
            {
                query = query.Where(n => n.MenuGroupListId.Contains(objArea.ItemValue));
            }

            var k = query.Select(n => n.Id).ToList();

            return await Task.FromResult(k);
        }

        public async Task<List<MenuFunction>> GetAllActive(string areaCode)
        {

            var objarea = StaticList.GroupMenuList().FirstOrDefault(n => n.AreaName == areaCode);
            var query = from n in _MenuFunctionRepository.Table
                        where n.Active == true
                        select n;
            if (objarea != null)
            {
                query = query.Where(n => n.MenuGroupListId.Contains(objarea.ItemValue));
            }
            return await Task.FromResult(query.ToList());
        }

        public async Task<IEnumerable<MenuFunction>> GetAllActiveByUserId(HttpContext httpContext, SessionModel model, string area)
        {
            var query = await GetMenuConfig();
            var q1 = from n in _MenuFunctionRepository.Table
                     where n.Active == true && n.Deleted == false && query.Any(m => m.MenuFunctionId == n.Id)
                     select n;
            if (!string.IsNullOrEmpty(area))
            {
                var objArea = StaticList.GroupMenuList().FirstOrDefault(n => n.AreaName == area);
                if (objArea != null)
                {
                    q1 = q1.Where(n => n.MenuGroupListId != null && n.MenuGroupListId.Contains(objArea.ItemValue));
                }
            }
            else
            {
                q1 = q1.Where(n => n.isSystem == true);

            }
            // Lấy menu theo userId
            if (model != null && model.isAdmin == false)
            {
                var auths = AuthHelper.MenuFunctionByUserId(model, httpContext, "").Result;
            }
            return await Task.FromResult(q1);
        }

        private async Task<List<MenuFunctionConfig>> GetMenuConfig()
        {
            var query = from n in _MenuFunctionConfigRepository.Table
                        select n;
            return await Task.FromResult(query.ToList());
        }

        public async Task<MessageReport> Create(MenuFunction obj)
        {
            return await _MenuFunctionRepository.Add(obj);
        }

        public async Task<List<MenuFunction_Submit>> GetAllCustomActiveOrder(string areaCode)
        {
            //
            var objArea = StaticList.GroupMenuList().FirstOrDefault(n => n.AreaName == areaCode);

            var dt = new List<MenuFunction_Submit>();

            var data = from n in _MenuFunctionRepository.Table
                       where n.Active == true
                       orderby n.OrderNumber
                       select n;

            if (objArea != null)
            {
                data = data.Where(n => n.MenuGroupListId != null && n.MenuGroupListId.Contains(objArea.ItemValue)).OrderBy(n => n.OrderNumber);
            }

            var k = data.ToList();

            foreach (var item in k)
            {
                dt.Add(GetCustomByModel(item).Result);
            }

            return await Task.FromResult(dt);
        }

        public async Task<MenuFunction_Submit> GetCustomByModel(MenuFunction model)
        {
            var obj = new MenuFunction_Submit()
            {
                ActionName = model.ActionName,
                Active = model.Active,
                ControllerName = model.ControllerName,
                Icon = model.Icon,
                Id = model.Id,
                MenuName = model.MenuName,
                MenuType = model.MenuType,
                ParentId = model.ParentId,
                SortOrder = Convert.ToInt32(model.OrderNumber),
                OrderNumber = model.OrderNumber,
                MenuGroupListId = model.MenuGroupListId
            };

            return await Task.FromResult(obj);
        }

        public async Task<MenuFunction_Submit> GetCustomById(string id)
        {
            var model = new MenuFunction_Submit();

            var obj = GetById(id).Result;
            if (obj != null)
            {
                model = GetCustomByModel(obj).Result;
            }

            return await Task.FromResult(model);
        }

        public async Task<MenuFunction> GetById(string id)
        {
            return await _MenuFunctionRepository.GetOneById(id);
        }

        public async Task<MessageReport> Update(MenuFunction oldObj)
        {
            return await _MenuFunctionRepository.Update(oldObj);
        }

        public async Task<MessageReport> Delete(string ids)
        {
            var query = from n in _MenuFunctionRepository.Table
                        where ids.Contains(n.Id)
                        select n;


            var result = await _MenuFunctionRepository.Remove_Multi(query);

            return result;
        }
        public async Task<List<MenuFunction>> GetAllActiveOrder()
        {
            var data = from n in _MenuFunctionRepository.Table
                       where n.Active == true
                       orderby n.OrderNumber
                       select n;

            return await Task.FromResult(data.ToList());
        }
        public async Task<string> GetBreadcrumb(string id, string parentid, string lastvalue)
        {
            var objMenu = await GetAllActiveOrder();
            if (string.IsNullOrEmpty(parentid))
            {
                lastvalue += id;
            }
            else
            {
                var obj = objMenu.FirstOrDefault(n => n.Id.Equals(parentid));
                if (obj != null)
                {
                    lastvalue += obj.Id + ",";
                    var str = GetBreadcrumb(obj.Id, obj.ParentId.ToString(), lastvalue);
                    lastvalue = str.Result;
                }
            }

            return await Task.FromResult(lastvalue);
        }
    }
}
