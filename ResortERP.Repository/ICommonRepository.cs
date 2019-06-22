using ResortERP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Repository
{
    public interface ICommonRepository<T>
    {
        string GetNextDatabaseName();
        string CopyDataBase(string BackUpFile, string DatabaseFilePath, string _databaseName);
        void Dispose();
        ICollection<T> Filter(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate);
        //List<SysDatabase> getallUsers();
    }
}
