using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Data.Repository;
using Test_Library.Helper;
using Test_Model.Models;
using X.PagedList;

namespace Test_Service.Admin.Database
{
    public class EmployeesService : IEmployeesService
    {
        private IEmployeesRepository _EmployeesRepository;
        public EmployeesService(IEmployeesRepository _EmployeesRepository)
        {
            this._EmployeesRepository = _EmployeesRepository;
        }

        public async Task<MessageReport> Create(Employees model)
        {
            return await _EmployeesRepository.Add(model);
        }

        public async Task<MessageReport> DeleteById(string id)
        {
            var result = new MessageReport(false, await LanguageHelper.GetLanguageText("MESSAGEREPORT:ERR"));

            var obj = GetById(id);
            if (obj.Result != null)
            {
                return await _EmployeesRepository.Remove(obj.Result);
            }
            else
            {
                result = new MessageReport(false, await LanguageHelper.GetLanguageText("MESSAGEREPORT:NON_RECORD"));
            }

            return await Task.FromResult(result);
        }

        public Task<List<Employees>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<GridModel<Employees>> GetAllCustomPagingByFirst(string key, string pc, int pageNumber, int pageSize)
        {
            var query = from n in _EmployeesRepository.Table
                        select n;


            if (!string.IsNullOrWhiteSpace(key))
            {
                query = query.Where(n => n.FullName.Contains(key));
            }



            var pageList = query.ToPagedList(pageNumber, pageSize);

            var model = GridModelHelper<Employees>.GetPage(pageList.OrderByDescending(n => n.FullName).ToList(), pageNumber, pageSize, pageList.TotalItemCount, pageList.PageCount);

            return await Task.FromResult(model);
        }

        public async Task<Employees> GetById(string id)
        {
            return await _EmployeesRepository.GetOneById(id);
        }

        public async Task<MessageReport> Update(Employees oldObj)
        {
            return await _EmployeesRepository.Update(oldObj);
        }
    }
}
