using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Data.Infrastructure;
using Test_Model.Models;

namespace Test_Data.Repository
{
    public interface ITEMP_BAO_CAO_TONG_HONGRepository : IRepository<TEMP_BAO_CAO_TONG_HONG>
    {
    }

    public class TEMP_BAO_CAO_TONG_HONGRepository : Repository<TEMP_BAO_CAO_TONG_HONG>, ITEMP_BAO_CAO_TONG_HONGRepository
    {
        public TEMP_BAO_CAO_TONG_HONGRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
 
}
