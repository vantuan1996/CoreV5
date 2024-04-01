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
  

    public interface IMessageRepository : IRepository<Message>
    {
    }

    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(DbContextOptions<Kztek_Entities> options) : base(options)
        {
        }
    }
}
