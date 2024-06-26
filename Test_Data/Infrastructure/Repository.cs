﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Core.Models;
using Test_Library.Helper;

namespace Test_Data.Infrastructure
{
    public abstract class  Repository<T> where T : class
    {
        private Kztek_Entities entities;
        private readonly DbSet<T> dbset;
        public Repository(DbContextOptions<Kztek_Entities> options)
        {
            entities = entities ?? (entities = new Kztek_Entities(options));
            //entities.Database.EnsureCreatedAsync();

            dbset = entities.Set<T>();
        }
        public async Task<T> GetOneById(string id)
        {
            var model = await dbset.FindAsync(id);

            return model;
        }

        public async Task<T> GetOneById(Guid id)
        {
            var model = await dbset.FindAsync(id);

            return model;
        }

        public async Task<T> GetOneById(int id)
        {
            var model = await dbset.FindAsync(id);

            return model;
        }

        public virtual IEnumerable<T> Table
        {
            get
            {
                return dbset;
            }
        }

        public async Task<MessageReport> Add(T model)
        {
            var result = new MessageReport(false, "error");

            try
            {
                await dbset.AddAsync(model);

                await entities.SaveChangesAsync();

                result = new MessageReport(true, await LanguageHelper.GetLanguageText("MESSAGEREPORT:ADD:SUCCESS"));
            }
            catch (System.Exception ex)
            {
                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }

            return result;
        }

        public async Task<MessageReport> Update(T model)
        {
            var result = new MessageReport(false, "error");

            try
            {
                dbset.Attach(model);
                entities.Entry(model).State = EntityState.Modified;

                await entities.SaveChangesAsync();

                result = new MessageReport(true, await LanguageHelper.GetLanguageText("MESSAGEREPORT:UPDATE:SUCCESS"));
            }
            catch (System.Exception ex)
            {
                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }

            return result;
        }

        public async Task<MessageReport> Remove(T model)
        {
            var result = new MessageReport(false, "error");

            try
            {
                dbset.Remove(model);

                await entities.SaveChangesAsync();

                result = new MessageReport(true, await LanguageHelper.GetLanguageText("MESSAGEREPORT:REMOVE:SUCCESS"));
            }
            catch (System.Exception ex)
            {
                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }

            return result;
        }

        public async Task<MessageReport> Remove_Multi(IEnumerable<T> models)
        {
            var result = new MessageReport(false, "error");

            try
            {
                dbset.RemoveRange(models);

                await entities.SaveChangesAsync();

                result = new MessageReport(true, await LanguageHelper.GetLanguageText("MESSAGEREPORT:REMOVE:SUCCESS"));
            }
            catch (System.Exception ex)
            {
                result = new MessageReport(false, string.Format("Message: {0} - Details: {1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : ""));
            }

            return result;
        }

        public async Task<List<T>> GetManyByQuery(string command)
        {
            var data = await dbset.FromSqlRaw(command).ToListAsync();

            return data;
        }

        public async Task<T> GetOneByQuery(string command)
        {
            var data = await dbset.FromSqlRaw(command).FirstOrDefaultAsync();

            return data;
        }

    }
}
