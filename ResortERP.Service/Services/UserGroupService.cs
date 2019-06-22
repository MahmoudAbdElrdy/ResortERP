using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class UserGroupService : IUserGroupService, IDisposable
    {
        IGenericRepository<UserGroup> userGroupRepo;

        public UserGroupService(IGenericRepository<UserGroup> userGroupRepo)
        {
            this.userGroupRepo = userGroupRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return userGroupRepo.CountAsync();
            });
        }

        public bool Delete(UserGroupVM entity)
        {
            UserGroup uGroup = new UserGroup()
            {
                ID = entity.ID,
                Code = entity.Code,
                ARName = entity.ARName,
                LatName = entity.LatName,
                Notes = entity.Notes,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            userGroupRepo.Delete(uGroup, entity.ID);
            return true;
        }

        public Task<bool> DeleteAsync(UserGroupVM entity)
        {
            return Task.Run<bool>(() =>
            {
                UserGroup uGroup = new UserGroup()
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    ARName = entity.ARName,
                    LatName = entity.LatName,
                    Notes = entity.Notes,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                userGroupRepo.Delete(uGroup, entity.ID);
                return true;
            });
        }

        public void Dispose()
        {
            userGroupRepo.Dispose();
        }

        public List<UserGroupVM> GetAll()
        {
            var q = from entity in userGroupRepo.GetAll()
                    select new UserGroupVM
                    {
                        ID = entity.ID,
                        Code = entity.Code,
                        ARName = entity.ARName,
                        LatName = entity.LatName,
                        Notes = entity.Notes,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }

        public string GetBy(int ID)
        {
            return userGroupRepo.GetByID(ID).Code.ToString();
        }

        public Task<List<UserGroupVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in userGroupRepo.GetPaged<int>(pageNum, pageSize, p => p.ID, false, out rowCount)
                        select new UserGroupVM
                        {
                            ID = entity.ID,
                            Code = entity.Code,
                            ARName = entity.ARName,
                            LatName = entity.LatName,
                            Notes = entity.Notes,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public bool Insert(UserGroupVM entity)
        {
            UserGroup uGroup = new UserGroup()
            {
                ID = entity.ID,
                Code = entity.Code,
                ARName = entity.ARName,
                LatName = entity.LatName,
                Notes = entity.Notes,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            userGroupRepo.Add(uGroup);
            return true;
        }

        public Task<int> InsertAsync(UserGroupVM entity)
        {
            return Task.Run<int>(() =>
             {
                 UserGroup uGroup = new UserGroup()
                 {
                     ID = entity.ID,
                     Code = entity.Code,
                     ARName = entity.ARName,
                     LatName = entity.LatName,
                     Notes = entity.Notes,
                     AddedBy = entity.AddedBy,
                     AddedOn = entity.AddedOn,
                     Disable = entity.Disable,
                     UpdatedBy = entity.UpdatedBy,
                     updatedOn = entity.updatedOn
                 };
                 userGroupRepo.Add(uGroup);
                 return uGroup.ID;
             });
        }

        public bool Update(UserGroupVM entity)
        {
            UserGroup uGroup = new UserGroup()
            {
                ID = entity.ID,
                Code = entity.Code,
                ARName = entity.ARName,
                LatName = entity.LatName,
                Notes = entity.Notes,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            userGroupRepo.Update(uGroup, uGroup.ID);
            return true;
        }

        public Task<bool> UpdateAsync(UserGroupVM entity)
        {
            return Task.Run<bool>(() =>
            {
                UserGroup uGroup = new UserGroup()
                {
                    ID = entity.ID,
                    Code = entity.Code,
                    ARName = entity.ARName,
                    LatName = entity.LatName,
                    Notes = entity.Notes,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                userGroupRepo.Update(uGroup, uGroup.ID);
                return true;
            });
        }

    }
}
