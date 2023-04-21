using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Model.Models;

namespace Test_Service.Admin.Database
{
    public interface ITEMP_BAO_CAO_TONG_HONGService
    {
        Task<Report_All> GetAllByFirst(string key, string fromdate, string todate, string pcid, string phuongthuc, int page, int pageSize);
    }
}
