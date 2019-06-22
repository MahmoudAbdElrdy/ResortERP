using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using ResortERP;
namespace ResortERP.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable
        where T : class

    {
        private IDbContext _context;

        public GenericRepository(IDbContext _context)
        {
            this._context = _context;
        }
        public ICollection<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            // var x = ((System.Data.Entity.Core.Objects.ObjectQuery)(_context.Database.SqlQuery<T>(sql, parameters)).AsQueryable())
            //.ToTraceString();
            return _context.Database.SqlQuery<T>(sql, parameters);

        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            // var x = ((System.Data.Entity.Core.Objects.ObjectQuery)(_context.Database.SqlQuery<T>(sql, parameters)).AsQueryable())
            //.ToTraceString();
            return _context.Database.ExecuteSqlCommand(sql, parameters);

        }

        public T GetByID(object ID)
        {
            return _context.Set<T>().Find(ID);
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public ICollection<T> Filter(Expression<Func<T, bool>> predicate)
        {
            var x = _context.Set<T>();
            return _context.Set<T>().Where(predicate).ToList();
        }

        public async Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetPaged<TResult>(int pageNum, int pageSize, Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount)
        {
            IQueryable<T> query = _context.Set<T>();
            if (pageSize <= 0) pageSize = 10;
            //Total result count
            rowsCount = _context.Set<T>().Count();
            //If page number should be > 0 else set to first page
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;
            //Calculate nunber of rows to skip on pagesize
            int excludedRows = (pageNum - 1) * pageSize;
            query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);
            //Skip the required rows for the current page and take the next records of pagesize count
            return query.Skip(excludedRows).Take(pageSize);
        }
        public IQueryable<T> GetPaged<TResult>(int pageNum, int pageSize, Expression<Func<T, bool>> whereCondition, Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount)
        {
            IQueryable<T> query = _context.Set<T>().Where(whereCondition);
            if (pageSize <= 0) pageSize = 10;
            //Total result count
            rowsCount = _context.Set<T>().Count();
            //If page number should be > 0 else set to first page
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;
            //Calculate nunber of rows to skip on pagesize
            int excludedRows = (pageNum - 1) * pageSize;
            query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);
            //Skip the required rows for the current page and take the next records of pagesize count
            return query.Skip(excludedRows).Take(pageSize);
        }

        public IQueryable<T> GetPaged<TResult>(int pageNum, int pageSize, Expression<Func<T, TResult>> orderByProperty, bool isAscendingOrder, out int rowsCount, Expression<Func<T, bool>> predicate = null) where TResult : struct
        {
            IQueryable<T> query = _context.Set<T>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (pageSize <= 0) pageSize = 10;
            //Total result count
            rowsCount = _context.Set<T>().Count();
            //If page number should be > 0 else set to first page
            if (rowsCount <= pageSize || pageNum <= 0) pageNum = 1;
            //Calculate nunber of rows to skip on pagesize
            int excludedRows = (pageNum - 1) * pageSize;
            //  query = isAscendingOrder ? query.OrderBy(orderByProperty) : query.OrderByDescending(orderByProperty);
            //Skip the required rows for the current page and take the next records of pagesize count
            return query.Skip(excludedRows).Take(pageSize);
        }
        public T Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();

                return entity; // something here...
            }
            catch (Exception ex)
            {
                // Handle exception
                //  T t= ex.Message;
                var x = ex.Message;
                return entity;
            }
           
        }
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public T Update(T updated, object key)
        {
            if (updated == null)
                return null;

            T existing = _context.Set<T>().Find(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                _context.SaveChanges();
            }
            return existing;
        }

        public async Task<T> UpdateAsync(T updated, object key)
        {
            if (updated == null)
                return null;

            T existing = await _context.Set<T>().FindAsync(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                await _context.SaveChangesAsync();
            }
            return existing;
        }

        public void Delete(T entity, object key)
        {
            T existing = _context.Set<T>().Find(key);
            if (existing != null)
            {
                _context.Set<T>().Remove(existing);
                _context.SaveChanges();
            }
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }
        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }


        public async Task<int> CountAsync(Expression<Func<T, bool>> whereCondition)
        {
            return await _context.Set<T>().Where(whereCondition).CountAsync();
        }

        public void Dispose()
        {
            _context.Dispose();

        }

        public T GetByIDComposite(object key1, object key2)
        {
            object[] keys = new object[2] { key1, key2 };
            return _context.Set<T>().Find(keys);
        }

        public T UpdateComposite(T updated, object[] keys)
        {
            if (updated == null)
                return null;

            //object[] keys = new object[2] { key1, key2 };

            T existing = _context.Set<T>().Find(keys);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                _context.SaveChanges();
            }
            return existing;
        }

        public void DeleteComposite(T entity, object[] keys)
        {
            //object[] keys = new object[2] { key1, key2 };

            T existing = _context.Set<T>().Find(keys);
            if (existing != null)
            {
                _context.Set<T>().Remove(existing);
                _context.SaveChanges();
            }
        }


        public string GetLoggedDatabaseName()
        {
            string databaseName = _context.Database.Connection.Database;
            return databaseName;
        }
        public ConnectionString GetLoggedDatabase()
        {
            ConnectionString conStr = null;
            try
            {
                string conn = _context.Database.Connection.ConnectionString;
                string[] connEle = conn.Split(';');
                Dictionary<string, string> con = new Dictionary<string, string>();
                foreach (string str in connEle)
                {
                    int startIndex = str.IndexOf('=') + 1;
                    int endIndex = str.Length <= 0 ? 0 : (str.Length - startIndex);
                    string key = str.Substring(0, startIndex <= 0 ? 0 : startIndex - 1);
                    string value = str.Substring(startIndex, endIndex <= 0 ? 0 : endIndex);
                    con.Add(key, value);
                }
                conStr = new ConnectionString()
                {
                    Data_Source = con["Data Source"],
                    Intial_Catalog = con["Initial Catalog"],
                    User_ID = con["user id"],
                    Password = con["password"]
                };
            }
            catch (Exception ex) { string str = ex.Message; }
            return conStr;
        }

        public void AddRange(IEnumerable<T> list)
        {
            _context.Set<T>().AddRange(list);
            _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> list)
        {
            _context.Set<T>().RemoveRange(list);
            _context.SaveChanges();
        }

    }
    public class ConnectionString
    {
        public string Intial_Catalog { get; internal set; }
        public string Data_Source { get; internal set; }
        public string User_ID { get; internal set; }
        public string Password { get; internal set; }

    }
}
