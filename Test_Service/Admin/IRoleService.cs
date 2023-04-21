using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Model.Models;

namespace Test_Service.Admin
{
    public interface IRoleService
    {
        Task<List<Role>> GetAll();

        Task<List<Role>> GetAllActiveOrder();

        Task<Role> GetById(string id);

        Role_Submit GetCustomById(string id);

        Role_Submit GetCustomByModel(Role model);

        Task<MessageReport> Create(Role model);

        Task<MessageReport> Update(Role model);

        Task<MessageReport> Delete(string id);

        Task<MessageReport> CreateMap(UserRole model);
        Task<List<Role_Custom>> GetAllUserRoles();

        Task<MessageReport> DeleteMap(string userid);
    }
}