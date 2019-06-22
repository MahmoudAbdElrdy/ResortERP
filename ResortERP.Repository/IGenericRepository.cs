using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Repository
{
    public interface IGenericRepository<T>
    {
        ICollection<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        ICollection<T> Filter(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetPaged<TResult>(int pageNum, int pageSizeout, Expression<Func<T, bool>> whereCondition, Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount);

        IQueryable<T> GetPaged<TResult>(int pageNum, int pageSizeout, Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount);
        T GetByID(object key);
        T Add(T entity);
        //  Task<T> AddAsync(T entity);
        T Update(T updated, object key);
        // Task<T> UpdateAsync(T updated, object key);
        void Delete(T entity, object key);
        // Task<int> DeleteAsync(T entity);
        int Count();
        Task<int> CountAsync();

         Task<int> CountAsync(Expression<Func<T, bool>> whereCondition);
        void Dispose();
        DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, params object[] parameters);

        T GetByIDComposite(object key1, object key2);
        T UpdateComposite(T updated, object[] key);
        void DeleteComposite(T entity, object[] key);
        string GetLoggedDatabaseName();
        ConnectionString GetLoggedDatabase();

        void AddRange(IEnumerable<T> list);
        void DeleteRange(IEnumerable<T> list);
    }
}
