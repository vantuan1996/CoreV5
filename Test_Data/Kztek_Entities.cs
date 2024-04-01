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
        public DbSet<Member> Member { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Message> Messages { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }

    }
}
