using ResortERP.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace ResortERP.Repository
{
    public interface IContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbEntityEntry Entry(object entity);
    }

    public class DefaultContext : DbContext, IContext
    {
        public DefaultContext(ISmartConnectionStringProvider defaultConnectionStringProvider) : base(defaultConnectionStringProvider.getCurrentConnectionString()) { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("UID");
            modelBuilder.Entity<SysDatabase>().ToTable("SysDatabase");
        }
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class { return base.Set<TEntity>(); }
        public new int SaveChanges() { return base.SaveChanges(); }
        public new Task<int> SaveChangesAsync() { return base.SaveChangesAsync(); }
        public new DbEntityEntry Entry(object entity) { return base.Entry(entity); }
    }

    public class DefaultConnectionStringProvider : IDefaultConnectionStringProvider
    {
        public string getConnectionString()
        {
            var defaultConnectionString = ConfigurationManager.ConnectionStrings["smartContext"].ConnectionString;
            return defaultConnectionString;
        }
    }

    public interface IDefaultConnectionStringProvider
    {
        string getConnectionString();
    }
}
