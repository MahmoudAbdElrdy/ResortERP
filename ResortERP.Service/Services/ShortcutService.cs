using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Service.IServices;
using AutoMapper;

namespace ResortERP.Service.Services
{
    public class ShortcutService : IShortcutService, IDisposable
    {
        IGenericRepository<SHORTCUTS> shortCutRepo;
        public ShortcutService(IGenericRepository<SHORTCUTS> shortCutRepo)
        {
            this.shortCutRepo = shortCutRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return shortCutRepo.CountAsync();
            });
        }

        public bool Delete(ShortcutsVM entity)
        {
            SHORTCUTS bt = new SHORTCUTS
            {
                UID = entity.UID,
                KEY_TYPE = entity.KEY_TYPE,
                ORDER_ID = entity.ORDER_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            object[] Keys = new object[2] { entity.UID, entity.KEY_TYPE };
            shortCutRepo.DeleteComposite(bt, Keys);
            return true;
        }

        public Task<bool> DeleteAsync(ShortcutsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                SHORTCUTS bt = new SHORTCUTS
                {
                    UID = entity.UID,
                    KEY_TYPE = entity.KEY_TYPE,
                    ORDER_ID = entity.ORDER_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                object[] Keys = new object[2] { entity.UID, entity.KEY_TYPE };
                shortCutRepo.DeleteComposite(bt, Keys);
                return true;
            });
        }

        public void Dispose()
        {
            shortCutRepo.Dispose();
        }

        public Task<List<ShortcutsVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run<List<ShortcutsVM>>(() =>
            {
                int rowCount;
                var q = from entity in shortCutRepo.GetPaged<string>(pageNum, pageSize, p => p.UID, false, out rowCount)
                        select new ShortcutsVM
                        {
                            UID = entity.UID,
                            KEY_TYPE = entity.KEY_TYPE,
                            ORDER_ID = entity.ORDER_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public List<ShortcutsVM> GetAll()
        {
            var q = from entity in shortCutRepo.GetAll()
                    select new ShortcutsVM
                    {
                        UID = entity.UID,
                        KEY_TYPE = entity.KEY_TYPE,
                        ORDER_ID = entity.ORDER_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }

        public bool Insert(ShortcutsVM entity)
        {
            SHORTCUTS bt = new SHORTCUTS
            {
                UID = entity.UID,
                KEY_TYPE = entity.KEY_TYPE,
                ORDER_ID = entity.ORDER_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            shortCutRepo.Add(bt);
            return true;
        }

        public Task<bool> InsertAsync(ShortcutsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                SHORTCUTS bt = new SHORTCUTS
                {
                    UID = entity.UID,
                    KEY_TYPE = entity.KEY_TYPE,
                    ORDER_ID = entity.ORDER_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                shortCutRepo.Add(bt);
                return true;
            });
        }

        public bool Update(ShortcutsVM entity)
        {
            SHORTCUTS bt = new SHORTCUTS
            {
                UID = entity.UID,
                KEY_TYPE = entity.KEY_TYPE,
                ORDER_ID = entity.ORDER_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            object[] Keys = new object[2] { bt.UID, bt.KEY_TYPE };
            shortCutRepo.UpdateComposite(bt, Keys);
            return true;
        }

        public Task<bool> UpdateAsync(ShortcutsVM entity)
        {
            return Task.Run<bool>(() =>
            {
                SHORTCUTS bt = new SHORTCUTS
                {
                    UID = entity.UID,
                    KEY_TYPE = entity.KEY_TYPE,
                    ORDER_ID = entity.ORDER_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                object[] Keys = new object[2] { bt.UID, bt.KEY_TYPE };
                shortCutRepo.Update(bt, Keys);
                return true;
            });
        }

        public Task<List<ShortcutsVM>> getByUID(string userName)
        {
            return Task.Run<List<ShortcutsVM>>(() => {
                var entities = shortCutRepo.Filter(X => X.UID == userName).Select(x => Mapper.Map<SHORTCUTS, ShortcutsVM>(x)).ToList();
                return entities;
            });
        }
    }
}
