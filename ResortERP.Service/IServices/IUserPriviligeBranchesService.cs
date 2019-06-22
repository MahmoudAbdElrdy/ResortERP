using ResortERP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IUserPriviligeBranchesService
    {
        bool Insert(UserPriviligeBranches entity);
        int InsertGetID(UserPriviligeBranches entity);
        Task<bool> InsertAsync(UserPriviligeBranches entity);
        bool Update(UserPriviligeBranches entity);
        Task<bool> UpdateAsync(UserPriviligeBranches entity);
        bool Delete(UserPriviligeBranches entity);
        Task DeleteAll(int id);
        Task DeleteAllByBranch(int id);
        Task<bool> DeleteAsync(UserPriviligeBranches entity);
        Task<List<UserPriviligeBranches>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<UserPriviligeBranches> GetAll();
        List<UserPriviligeBranches> GetById(int id);
        List<UserPriviligeBranches> GetByIdBransh(int id);
        Task<List<UserPriviligeBranches>> GetByCN(string name);
    }
}
