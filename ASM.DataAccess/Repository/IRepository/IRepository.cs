using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
   
        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);

        IEnumerable<T> GetAll();
        Task<IEnumerable<T?>> GetAllAsync(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        void Add(T entity);
        Task AddAsync(T entity);
        void Remove(T entity);
        Task RemoveAsync(T entity);
        void RemoveRange(IEnumerable<T> entity);

        void Update(T entity);
        void Update(T entity, int id);

        Task UpdateAsync(T entity);
        Task UpdateAsync(T entity, int id);

        List<T> ListOfT(string spName, object? para= null);
        string ValueOfT( string spName,object? para = null);
        T ObjectOfT(string spName, object? para = null);

        Task<List<T>> ListOfTAsync(string spName, object? para = null);
        Task<string> ValueOfTAsync(string spName, object? para = null);
        Task<T> ObjectOfTAsync(string spName, object? para = null);
    }
}
