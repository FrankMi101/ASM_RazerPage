
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class CommonRepository<T>  : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private static string ConString = "";
        internal DbSet<T> dbSet;

        public CommonRepository(ApplicationDbContext db)
        {
            this._db = db;
            this.dbSet = _db.Set<T>();
            ConString = db.Database.GetDbConnection().ConnectionString;
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
           //   _db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            return query.Where(filter).ToList();
        }

        public async Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            return await query.Where(filter).ToListAsync();
        }

        //includeProp - "Category,CoverType"
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            if (tracked)
            {
                IQueryable<T> query = dbSet;

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }
            else
            {
                IQueryable<T> query = dbSet.AsNoTracking();

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }

        }



        public void Remove(T entity)
        {
            dbSet.Remove(entity);
               _db.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
               _db.SaveChanges();
        }
     

        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
            _db.SaveChanges();
        }
        public virtual void Update(T entity,int id)
        {
            dbSet.Update(entity);
            _db.SaveChanges();
        }
        public virtual object SPName(string action)
        {
            return GetDefaultSPName(action);
        }

        public string ValueOfT(string spName, object para = null)
        {
            string sp = (string)SPName(spName);
            var spCall = new SP_Call(_db);
            return spCall.ReturnValue(sp, para);

        }
        public List<T> ListOfT(string spName, object para = null)
        {
            //using (SqlConnection sqlCon = new SqlConnection(ConString))
            //{
            //    sqlCon.Open();
            //    var list = sqlCon.Query<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
            //    return list.ToList();
            //}
            string sp = (string)SPName(spName);
            var spCall = new SP_Call(_db);
            return spCall.ReturnList<T>(sp, para);
        }
        private string  GetDefaultSPName(string action)
        {
            var objType = typeof(T).Name;
            switch (objType)
            {
                case "Apps":
                    return "dbo.ASM_CommenList_App";
                case "AppsModel":
                    return "dbo.ASM_CommenList_AppModel";
                default:
                    return "dbo.ASM_GeneralSPName";

            }
        }

        public T ObjectOfT(string spName, object? para = null)
        {
            string sp = (string)SPName(spName);
            var spCall = new SP_Call(_db);
            return spCall.ExecuteReturn<T>(sp, para);
        }

        public async Task<List<T>> ListOfTAsync(string spName, object? para = null)
        {
            string sp = (string)SPName(spName);
            var spCall = new SP_Call(_db);
            return await spCall.ReturnListAsync<T>(sp, para);
        }

        public async Task<string> ValueOfTAsync(string spName, object? para = null)
        {
            string sp = (string)SPName(spName);
            var spCall = new SP_Call(_db);
            return await spCall.ReturnValueAsync<string>(sp, para);
        }

        public async Task<T> ObjectOfTAsync(string spName, object? para = null)
        {
            string sp = (string)SPName(spName);
            var spCall = new SP_Call(_db);
            return await spCall.ReturnObjectAsync<T>(sp, para);
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
