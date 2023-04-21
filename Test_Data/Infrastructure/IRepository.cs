using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;

namespace Test_Data.Infrastructure
{
   public interface IRepository<T> where T : class
    {
        Task<T> GetOneById(string id);

        Task<T> GetOneById(Guid id);

        Task<T> GetOneById(int id);

        IEnumerable<T> Table { get; }

        Task<MessageReport> Add(T model);

        Task<MessageReport> Update(T model);

        Task<MessageReport> Remove(T model);

        Task<MessageReport> Remove_Multi(IEnumerable<T> models);

        Task<List<T>> GetManyByQuery(string command);

        Task<T> GetOneByQuery(string command);
    }
}
