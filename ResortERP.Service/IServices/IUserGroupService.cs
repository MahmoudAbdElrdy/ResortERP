using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IUserGroupService
    {
        bool Insert(UserGroupVM entity);
        Task<int> InsertAsync(UserGroupVM entity);
        bool Update(UserGroupVM entity);
        Task<bool> UpdateAsync(UserGroupVM entity);
        bool Delete(UserGroupVM entity);
        Task<bool> DeleteAsync(UserGroupVM entity);
        Task<List<UserGroupVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<UserGroupVM> GetAll();
        string GetBy(int ID);
    }
}
