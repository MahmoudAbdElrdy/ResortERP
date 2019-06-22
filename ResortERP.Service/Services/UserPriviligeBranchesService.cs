using ResortERP.Core;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class UserPriviligeBranchesService: IUserPriviligeBranchesService, IDisposable
    {
        #region var
        IGenericRepository<UserPriviligeBranches> uPrivBranRepo;
        IGenericRepository<User> userRepo;
        #endregion

        #region Const
        public UserPriviligeBranchesService(IGenericRepository<UserPriviligeBranches> _uPrivBranRepo, IGenericRepository<User> _userRepo)
        {
            uPrivBranRepo = _uPrivBranRepo;
            userRepo = _userRepo;
        }
        #endregion

        #region Method
        public Task<int> CountAsync()
        {
            return Task.Run(
                () =>{
                    return uPrivBranRepo.CountAsync();
                });
        }

        public bool Delete(UserPriviligeBranches entity)
        {
            uPrivBranRepo.Delete(entity, entity.ID);
            return true;
        }

        public Task DeleteAll(int id)
        {
            return Task.Run(
                () =>{
                    var tempResult = uPrivBranRepo.GetAll().Where(a => a.ID == id);
                    foreach (var c in tempResult)
                    {
                        object[] keys = new object[2] { c.ID,c.COM_BRN_ID };
                        uPrivBranRepo.DeleteComposite(c, keys);
                    }

                });

        }

        public Task DeleteAllByBranch(int id)
        {
            return Task.Run(
                () => {
                    var tempResult = uPrivBranRepo.GetAll().Where(a => a.COM_BRN_ID == id);
                    foreach (var c in tempResult)
                    {
                        object[] keys = new object[2] { c.ID, c.COM_BRN_ID };
                        uPrivBranRepo.DeleteComposite(c, keys);
                    }

                });

        }

        public Task<bool> DeleteAsync(UserPriviligeBranches entity)
        {
            return Task.Run(
                () =>{
                    uPrivBranRepo.Delete(entity, entity.ID);
                    return true;
                });
        }

        public void Dispose()
        {
            uPrivBranRepo.Dispose();
        }

        public List<UserPriviligeBranches> GetAll()
        {
            return uPrivBranRepo.GetAll().ToList();
        }

        public List<UserPriviligeBranches> GetById(int id)
        {
            return uPrivBranRepo.GetAll().Where(a=>a.ID==id).ToList();
        }

        public List<UserPriviligeBranches> GetByIdBransh(int id)
        {
            return uPrivBranRepo.GetAll().Where(a => a.COM_BRN_ID == id).ToList();
        }

        public Task<List<UserPriviligeBranches>> GetByCN(string name)
        {
            return Task.Run(()=> {
                var q = userRepo.GetAll().Where(a => a.UID == name).FirstOrDefault();
                if (q != null)
                {
                    return uPrivBranRepo.GetAll().Where(a => a.ID == q.ID).ToList();
                }
                return new List<UserPriviligeBranches>();
            });

        }

        public Task<List<UserPriviligeBranches>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(
                () =>{
                   return uPrivBranRepo.GetAll().ToList();
                });
        }
        
        public bool Insert(UserPriviligeBranches entity)
        {
            uPrivBranRepo.Add(entity);
            return true;
        }

        public Task<bool> InsertAsync(UserPriviligeBranches entity)
        {
            return Task.Run(
                () =>{
                    uPrivBranRepo.Add(entity);
                    return true;
                });
        }

        public int InsertGetID(UserPriviligeBranches entity)
        {
            var result = uPrivBranRepo.Add(entity);
            return result.ID;
        }

        public bool Update(UserPriviligeBranches entity)
        {
            object[] keys = new object[2] { entity.ID, entity.COM_BRN_ID };
            uPrivBranRepo.UpdateComposite(entity, keys);
            return true;
        }

        public Task<bool> UpdateAsync(UserPriviligeBranches entity)
        {
            return Task.Run(
                () => {
                    object[] keys = new object[2] { entity.ID, entity.COM_BRN_ID };
                    uPrivBranRepo.UpdateComposite(entity, keys);
                    return true;
                });
        }

        #endregion
    }
}
