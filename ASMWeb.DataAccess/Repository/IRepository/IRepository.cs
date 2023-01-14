using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
   
        T GetById(int id);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

        void Update(T entity);


        List<T> ListOfT(string spName, object? para= null);
        string ValueOfT( string spName,object? para = null);
        T ObjectOfT<T>(string spName, object? para = null);
    }
}
