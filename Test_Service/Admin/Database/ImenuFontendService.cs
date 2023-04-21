using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Model.Models;

namespace Test_Service.Admin.Database
{
    public interface ImenuFontendService
    {
        Task<menuFontend_Custom> getAllByParentId(string name);
    }
}
