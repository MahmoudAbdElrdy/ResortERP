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
    public class SELLS_TYPESService : ISELLS_TYPESService, IDisposable
    {
        IGenericRepository<SELLS_TYPES> _repo;
        public SELLS_TYPESService(IGenericRepository<SELLS_TYPES> repo)
        {
            this._repo = repo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return _repo.CountAsync();
            });
        }

        public bool Delete(SELLS_TYPESVM entity)
        {
            SELLS_TYPES emp = new SELLS_TYPES
            {
                SELL_TYPE_ID = entity.SELL_TYPE_ID
            };
            _repo.Delete(emp, entity.SELL_TYPE_ID);
            return true;
        }

        public Task<bool> DeleteAsync(SELLS_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                SELLS_TYPES emp = new SELLS_TYPES
                {
                    SELL_TYPE_ID = entity.SELL_TYPE_ID
                };
                _repo.Delete(emp, entity.SELL_TYPE_ID);
                return true;
            });
        }

        public void Dispose()
        {
            _repo.Dispose();
        }

        public List<SELLS_TYPESVM> GetAll()
        {
            var q = from entity in _repo.GetAll()
                    select new SELLS_TYPESVM
                    {
                        SELL_TYPE_ID = entity.SELL_TYPE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        SELL_TYPE_AR_NAME = entity.SELL_TYPE_AR_NAME,
                        SELL_TYPE_EN_NAME = entity.SELL_TYPE_EN_NAME,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        SHOW_OR_NOT = entity.SHOW_OR_NOT,
                        Disable = entity.Disable,
                        
                    };
            return q.ToList();
        }
        public SELLS_TYPESVM GetByID(int id)
        {
            var q = from entity in _repo.Filter(X => X.SELL_TYPE_ID == id)
                    select new SELLS_TYPESVM
                    {
                        SELL_TYPE_ID = entity.SELL_TYPE_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        SELL_TYPE_AR_NAME = entity.SELL_TYPE_AR_NAME,
                        SELL_TYPE_EN_NAME = entity.SELL_TYPE_EN_NAME,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn,
                        SHOW_OR_NOT = entity.SHOW_OR_NOT,
                        Disable = entity.Disable
                    };
            return q.FirstOrDefault();
        }

        public Task<List<SELLS_TYPESVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<SELLS_TYPESVM>>(() =>
            {
                int rowCount;
                var q = from entity in _repo.GetPaged<long>(pageNum, pageSize, p => p.SELL_TYPE_ID, false, out rowCount)
                        select new SELLS_TYPESVM
                        {
                            SELL_TYPE_ID = entity.SELL_TYPE_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            SELL_TYPE_AR_NAME = entity.SELL_TYPE_AR_NAME,
                            SELL_TYPE_EN_NAME = entity.SELL_TYPE_EN_NAME,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn,
                            SHOW_OR_NOT = entity.SHOW_OR_NOT,
                            Disable = entity.Disable
                        };
                return q.ToList();
            });
        }

        public bool Insert(SELLS_TYPESVM entity)
        {
            SELLS_TYPES emp = new SELLS_TYPES
            {
                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                SELL_TYPE_AR_NAME = entity.SELL_TYPE_AR_NAME,
                SELL_TYPE_EN_NAME = entity.SELL_TYPE_EN_NAME,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                SHOW_OR_NOT = entity.SHOW_OR_NOT,
                Disable = entity.Disable
            };
            _repo.Add(emp);
            return true;
        }

        public Task<bool> InsertAsync(SELLS_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                SELLS_TYPES emp = new SELLS_TYPES
                {
                    SELL_TYPE_ID = entity.SELL_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    SELL_TYPE_AR_NAME = entity.SELL_TYPE_AR_NAME,
                    SELL_TYPE_EN_NAME = entity.SELL_TYPE_EN_NAME,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    SHOW_OR_NOT = entity.SHOW_OR_NOT,
                    Disable = entity.Disable
                };
                _repo.Add(emp);
                return true;
            });
        }

        public bool Update(SELLS_TYPESVM entity)
        {
            SELLS_TYPES emp = new SELLS_TYPES
            {
                SELL_TYPE_ID = entity.SELL_TYPE_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                SELL_TYPE_AR_NAME = entity.SELL_TYPE_AR_NAME,
                SELL_TYPE_EN_NAME = entity.SELL_TYPE_EN_NAME,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn,
                SHOW_OR_NOT = entity.SHOW_OR_NOT,
                Disable = entity.Disable
            };
            _repo.Update(emp, emp.SELL_TYPE_ID);
            return true;
        }

        public Task<bool> UpdateAsync(SELLS_TYPESVM entity)
        {
            return Task.Run<bool>(() =>
            {
                SELLS_TYPES emp = new SELLS_TYPES
                {
                    SELL_TYPE_ID = entity.SELL_TYPE_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    SELL_TYPE_AR_NAME = entity.SELL_TYPE_AR_NAME,
                    SELL_TYPE_EN_NAME = entity.SELL_TYPE_EN_NAME,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn,
                    SHOW_OR_NOT = entity.SHOW_OR_NOT,
                    Disable = entity.Disable
                };
                _repo.Update(emp, emp.SELL_TYPE_ID);
                return true;
            });
        }
    }
}
