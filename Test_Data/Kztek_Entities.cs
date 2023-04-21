using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Model.Models;

namespace Test_Data
{
    public class Kztek_Entities : DbContext
    {
        public Kztek_Entities(DbContextOptions<Kztek_Entities> options) : base(options)
        {

        }
        //Main
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<MenuFunction> SY_MenuFunctions { get; set; }
        public DbSet<TEMP_BAO_CAO_TONG_HONG> TEMP_BAO_CAO_TONG_HONGs { get; set; }
        public DbSet<MenuFunctionConfig> MenuFunctionConfigs { get; set; }
        public DbSet<menuFontend> ImenuFonteds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<tblSystemConfig>(entity =>
            //{
            //    entity.Ignore(e => e.SortOrder);
            //});

            //modelBuilder.Entity<tbl_Event>(entity =>
            //{

            //});
        }

    }
}
