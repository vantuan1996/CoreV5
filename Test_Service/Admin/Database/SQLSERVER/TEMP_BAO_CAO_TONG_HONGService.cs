using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Data.Repository;
using Test_Model.Models;
using X.PagedList;

namespace Test_Service.Admin.Database.SQLSERVER
{
    public class TEMP_BAO_CAO_TONG_HONGService : ITEMP_BAO_CAO_TONG_HONGService
    {
        private ITEMP_BAO_CAO_TONG_HONGRepository _TEMP_BAO_CAO_TONG_HONGRepository;
        public TEMP_BAO_CAO_TONG_HONGService(ITEMP_BAO_CAO_TONG_HONGRepository _TEMP_BAO_CAO_TONG_HONGRepository)
        {
            this._TEMP_BAO_CAO_TONG_HONGRepository = _TEMP_BAO_CAO_TONG_HONGRepository;
        }
        public async Task<Report_All> GetAllByFirst(string key, string fromdate, string todate, string pcid, string phuongthuc, int page, int pageSize)
        {
            var query = from n in _TEMP_BAO_CAO_TONG_HONGRepository.Table
                        select n;
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(n => n.TEN_DM.Contains(key));
            }
            var pagex = query.ToPagedList(page, pageSize);

            var model = new Report_All()
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalPage = pagex.PageCount,
                TotalItem = pagex.TotalItemCount,
                data = pagex.OrderByDescending(n => n.MA_DM).ToList()


            };

            return await Task.FromResult(model) ;
        }

      
    }
}
