using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResortERP.Repository
{
    public class CommonRepository<T> : ICommonRepository<T>, IDisposable where T : class
    {
        private IDefaultConnectionStringProvider connection;
        private IContext context;

        public CommonRepository(IDefaultConnectionStringProvider connection, IContext context)
        {
            this.connection = connection;
            this.context = context;
        }

        public ICollection<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate).ToList();
        }

        public async Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).ToListAsync();
        }
        public string CopyDataBase(string BackUpFile, string DatabaseFilePath, string _databaseName)
        {
            try
            {
                string connectionString = connection.getConnectionString();
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                Microsoft.SqlServer.Management.Common.ServerConnection conn = new Microsoft.SqlServer.Management.Common.ServerConnection(sqlConnection);
                Microsoft.SqlServer.Management.Smo.Server server = new Microsoft.SqlServer.Management.Smo.Server(conn);
                string databaseName = GetNextDatabaseName();
                string backUpFile = BackUpFile;
                string DatabaseFilesPath = DatabaseFilePath;

                try
                {
                    string Query = @"USE [master] 
                    GO
                    RESTORE DATABASE " + databaseName + @" FROM  DISK = N'" + backUpFile + @"' WITH MOVE 'Smart1' TO '" + DatabaseFilesPath + @"\" + databaseName + @".mdf',MOVE 'Smart1_Log' TO '" + DatabaseFilesPath + @"\" + databaseName + @".ldf',REPLACE,STATS=10";
                    server.ConnectionContext.ExecuteNonQuery(Query);

                }
                catch (Exception ex)
                { string str = ex.Message; }


                return databaseName;
            }
            catch (Exception ex) { string str = ex.Message; return _databaseName; }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public string LastDataBaseCreated()
        {
            var dName = from entity in context.Set<SysDatabase>().ToList().OrderByDescending(x => x.name.Length).ThenByDescending(x => x.name)
                        select new SysDatabaseVM
                        {
                            database_id = entity.database_id,
                            name = entity.name
                        };
            return dName.FirstOrDefault().name;
        }

        public string GetNextDatabaseName()
        {
            string ID = LastDataBaseCreated().Remove(0, 5);
            int counter = Convert.ToInt32(ID) + 1;
            return "Smart" + counter;
        }



    }
}
