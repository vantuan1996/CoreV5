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
    public interface IEmployeesService
    {
        Task<List<Employees>> GetAll();
        Task<GridModel<Employees>> GetAllCustomPagingByFirst(string key, string pc, int page, int v);
        Task<Employees> GetById(string id);
        Task<MessageReport> Update(Employees oldObj);
        Task<MessageReport> Create(Employees model);
        Task<MessageReport> DeleteById(string id);
    }
}
