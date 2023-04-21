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

    public interface IUserRoleRepository : IRepository<UserRole>
    {
    }

    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
}
