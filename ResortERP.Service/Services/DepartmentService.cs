using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Core;
using ResortERP.Service.IServices;

namespace ResortERP.Service.Services
{
    public class DepartmentService : IDepartmentService, IDisposable
    {
        IGenericRepository<Department> departmentRepo;

        public DepartmentService(IGenericRepository<Department> departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return departmentRepo.CountAsync();
            });
        }

        public bool Delete(DepartmentVM entity)
        {
            Department cur = new Department()
            {
                DEPT_ID = entity.DEPT_ID,
                DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                DEPT_AR_NAME = entity.DEPT_AR_NAME,
                DEPT_CODE = entity.DEPT_CODE,
                DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                DEPT_EN_NAME = entity.DEPT_EN_NAME,
                COM_BRN_ID = entity.COM_BRN_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            departmentRepo.Delete(cur, entity.DEPT_ID);
            return true;
        }

        public Task<bool> DeleteAsync(DepartmentVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Department cur = new Department()
                {
                    DEPT_ID = entity.DEPT_ID,
                    DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                    DEPT_AR_NAME = entity.DEPT_AR_NAME,
                    DEPT_CODE = entity.DEPT_CODE,
                    DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                    DEPT_EN_NAME = entity.DEPT_EN_NAME,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                departmentRepo.Delete(cur, entity.DEPT_ID);
                return true;
            });
        }

        public void Dispose()
        {
            departmentRepo.Dispose();
        }
        public List<Department> getDeptByBran(int compBranId)
        {
            var q = departmentRepo.GetAll().Where(a => a.COM_BRN_ID == compBranId).ToList();
            return q;
        }
        
        public List<DepartmentVM> GetAll()
        {
            var q = from entity in departmentRepo.GetAll()
                    select new DepartmentVM
                    {
                        DEPT_ID = entity.DEPT_ID,
                        DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                        DEPT_AR_NAME = entity.DEPT_AR_NAME,
                        DEPT_CODE = entity.DEPT_CODE,
                        DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                        DEPT_EN_NAME = entity.DEPT_EN_NAME,
                        COM_BRN_ID = entity.COM_BRN_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }
        public DepartmentVM getById(int DEPT_ID)
        {
            var q = from entity in departmentRepo.GetAll().Where(a=>a.DEPT_ID== DEPT_ID)
                    select new DepartmentVM
                    {
                        DEPT_ID = entity.DEPT_ID,
                        DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                        DEPT_AR_NAME = entity.DEPT_AR_NAME,
                        DEPT_CODE = entity.DEPT_CODE,
                        DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                        DEPT_EN_NAME = entity.DEPT_EN_NAME,
                        COM_BRN_ID = entity.COM_BRN_ID,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.FirstOrDefault();
        }
        public Task<List<DepartmentVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in departmentRepo.GetPaged<int>(pageNum, pageSize, p => p.DEPT_ID, false, out rowCount)
                        select new DepartmentVM
                        {
                            DEPT_ID = entity.DEPT_ID,
                            DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                            DEPT_AR_NAME = entity.DEPT_AR_NAME,
                            DEPT_CODE = entity.DEPT_CODE,
                            DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                            DEPT_EN_NAME = entity.DEPT_EN_NAME,
                            COM_BRN_ID = entity.COM_BRN_ID,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public bool Insert(DepartmentVM entity)
        {
            Department cur = new Department()
            {
                DEPT_ID = entity.DEPT_ID,
                DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                DEPT_AR_NAME = entity.DEPT_AR_NAME,
                DEPT_CODE = entity.DEPT_CODE,
                DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                DEPT_EN_NAME = entity.DEPT_EN_NAME,
                COM_BRN_ID = entity.COM_BRN_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            departmentRepo.Add(cur);
            return true;
        }

        public Task<int> InsertAsync(DepartmentVM entity)
        {
            return Task.Run<int>(() =>
             {
                 Department cur = new Department()
                 {
                     DEPT_ID = entity.DEPT_ID,
                     DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                     DEPT_AR_NAME = entity.DEPT_AR_NAME,
                     DEPT_CODE = entity.DEPT_CODE,
                     DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                     DEPT_EN_NAME = entity.DEPT_EN_NAME,
                     COM_BRN_ID = entity.COM_BRN_ID,
                     AddedBy = entity.AddedBy,
                     AddedOn = entity.AddedOn,
                     Disable = entity.Disable,
                     UpdatedBy = entity.UpdatedBy,
                     updatedOn = entity.updatedOn
                 };
                 departmentRepo.Add(cur);
                 return cur.DEPT_ID;
             });
        }

        public bool Update(DepartmentVM entity)
        {
            Department cur = new Department()
            {
                DEPT_ID = entity.DEPT_ID,
                DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                DEPT_AR_NAME = entity.DEPT_AR_NAME,
                DEPT_CODE = entity.DEPT_CODE,
                DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                DEPT_EN_NAME = entity.DEPT_EN_NAME,
                COM_BRN_ID = entity.COM_BRN_ID,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            departmentRepo.Update(cur, cur.DEPT_ID);
            return true;
        }

        public Task<bool> UpdateAsync(DepartmentVM entity)
        {
            return Task.Run<bool>(() =>
            {
                Department cur = new Department()
                {
                    DEPT_ID = entity.DEPT_ID,
                    DEPT_AR_ABRV = entity.DEPT_AR_ABRV,
                    DEPT_AR_NAME = entity.DEPT_AR_NAME,
                    DEPT_CODE = entity.DEPT_CODE,
                    DEPT_EN_ABRV = entity.DEPT_EN_ABRV,
                    DEPT_EN_NAME = entity.DEPT_EN_NAME,
                    COM_BRN_ID = entity.COM_BRN_ID,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                departmentRepo.Update(cur, cur.DEPT_ID);
                return true;
            });
        }


        public string GetLastCode()
        {
            var lastCode = departmentRepo.GetAll().OrderByDescending(d => d.DEPT_ID).FirstOrDefault();
            return lastCode.DEPT_CODE;
        }



    }
}
