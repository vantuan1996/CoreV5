using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Library.Models;
using Test_Model.Models;

namespace Test_Service.Admin
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

       Task<GridModel<User>> GetPaging(string key, int pageNumber, int pageSize);

        Task<User> GetById(string id);

        Task<User> GetByUsername(string username);

        Task<User> GetByUsername_notId(string username, string id);

        User_Submit GetCustomById(string id);
    
        User_Submit GetCustomByModel(User model);

        Task<MessageReport> Create(User model);

        Task<MessageReport> Update(User model);

        Task<MessageReport> Delete(string id);

       Task<MessageReport> Login(AuthModel model, out User user);
        Task<IEnumerable<User>> GetAllActiveByListId(string ids);
        Task<MessageReport> SignIn(AuthModel_LowSecurity value);
 
    }
}
