using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Library.Models;
using Test_Model.Models;

namespace Test_Service.Admin.Database
{
    public interface IMenuFunctionService
    {
        Task<IEnumerable<MenuFunction>> GetAllActiveByUserId(HttpContext httpContext, SessionModel user, string area);
        Task<List<MenuFunction>> GetAllActive(string areaCode);
        Task<MessageReport> CreateMap(RoleMenu model);
        Task<MessageReport> DeleteMap(string id, string areaCode);
        Task<MessageReport> Create(MenuFunction obj);
        Task<List<MenuFunction_Submit>> GetAllCustomActiveOrder(string areaCode);
        Task<MenuFunction_Submit> GetCustomById(string id);
        Task<MenuFunction> GetById(string id);
        Task<MessageReport> Update(MenuFunction oldObj);
        Task<MessageReport> Delete(string id);
        Task<string> GetBreadcrumb(string id, string parentid, string lastvalue);
        Task<List<MenuFunction>> GetAllActiveOrder();
    }
}
