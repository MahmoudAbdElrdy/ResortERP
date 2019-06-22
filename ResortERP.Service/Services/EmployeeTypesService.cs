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
    public class EmployeeTypesService : IEmployeeTypesService, IDisposable
    {
        IGenericRepository<EmployeeTypes> employeeTypesRepo;

        public EmployeeTypesService(IGenericRepository<EmployeeTypes> employeeTypesRepo)
        {
            this.employeeTypesRepo = employeeTypesRepo;
        }

        public Task<int> CountAsync()
        {
            return Task.Run<int>(() =>
            {
                return employeeTypesRepo.CountAsync();
            });
        }

        public bool Delete(EmployeeTypesVM entity)
        {
            EmployeeTypes cur = new EmployeeTypes()
            {
                EMP_TYPE_ID = entity.EMP_TYPE_ID,
                EMP_TYPE_AR_NAME = entity.EMP_TYPE_AR_NAME,
                EMP_TYPE_CODE = entity.EMP_TYPE_CODE,
                EMP_TYPE_EN_NAME = entity.EMP_TYPE_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            employeeTypesRepo.Delete(cur, entity.EMP_TYPE_ID);
            return true;
        }

        public Task<bool> DeleteAsync(EmployeeTypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                EmployeeTypes cur = new EmployeeTypes()
                {
                    EMP_TYPE_ID = entity.EMP_TYPE_ID,
                    EMP_TYPE_AR_NAME = entity.EMP_TYPE_AR_NAME,
                    EMP_TYPE_CODE = entity.EMP_TYPE_CODE,
                    EMP_TYPE_EN_NAME = entity.EMP_TYPE_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                employeeTypesRepo.Delete(cur, entity.EMP_TYPE_ID);
                return true;
            });
        }

        public void Dispose()
        {
            employeeTypesRepo.Dispose();
        }

        public List<EmployeeTypesVM> GetAll()
        {
            var q = from entity in employeeTypesRepo.GetAll()
                    select new EmployeeTypesVM
                    {
                        EMP_TYPE_ID = entity.EMP_TYPE_ID,
                        EMP_TYPE_AR_NAME = entity.EMP_TYPE_AR_NAME,
                        EMP_TYPE_CODE = entity.EMP_TYPE_CODE,
                        EMP_TYPE_EN_NAME = entity.EMP_TYPE_EN_NAME,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.ToList();
        }
        public EmployeeTypesVM getById(int EMP_TYPE_ID)
        {
            var q = from entity in employeeTypesRepo.GetAll().Where(a=>a.EMP_TYPE_ID== EMP_TYPE_ID)
                    select new EmployeeTypesVM
                    {
                        EMP_TYPE_ID = entity.EMP_TYPE_ID,
                        EMP_TYPE_AR_NAME = entity.EMP_TYPE_AR_NAME,
                        EMP_TYPE_CODE = entity.EMP_TYPE_CODE,
                        EMP_TYPE_EN_NAME = entity.EMP_TYPE_EN_NAME,
                        AddedBy = entity.AddedBy,
                        AddedOn = entity.AddedOn,
                        Disable = entity.Disable,
                        UpdatedBy = entity.UpdatedBy,
                        updatedOn = entity.updatedOn
                    };
            return q.FirstOrDefault();
        }
        public Task<List<EmployeeTypesVM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in employeeTypesRepo.GetPaged<int>(pageNum, pageSize, p => p.EMP_TYPE_ID, false, out rowCount)
                        select new EmployeeTypesVM
                        {
                            EMP_TYPE_ID = entity.EMP_TYPE_ID,
                            EMP_TYPE_AR_NAME = entity.EMP_TYPE_AR_NAME,
                            EMP_TYPE_CODE = entity.EMP_TYPE_CODE,
                            EMP_TYPE_EN_NAME = entity.EMP_TYPE_EN_NAME,
                            AddedBy = entity.AddedBy,
                            AddedOn = entity.AddedOn,
                            Disable = entity.Disable,
                            UpdatedBy = entity.UpdatedBy,
                            updatedOn = entity.updatedOn
                        };
                return q.ToList();
            });
        }

        public bool Insert(EmployeeTypesVM entity)
        {
            EmployeeTypes cur = new EmployeeTypes()
            {
                EMP_TYPE_ID = entity.EMP_TYPE_ID,
                EMP_TYPE_AR_NAME = entity.EMP_TYPE_AR_NAME,
                EMP_TYPE_CODE = entity.EMP_TYPE_CODE,
                EMP_TYPE_EN_NAME = entity.EMP_TYPE_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            employeeTypesRepo.Add(cur);
            return true;
        }

        public Task<int> InsertAsync(EmployeeTypesVM entity)
        {
            return Task.Run<int>(() =>
             {
                 EmployeeTypes cur = new EmployeeTypes()
                 {
                     EMP_TYPE_ID = entity.EMP_TYPE_ID,
                     EMP_TYPE_AR_NAME = entity.EMP_TYPE_AR_NAME,
                     EMP_TYPE_CODE = entity.EMP_TYPE_CODE,
                     EMP_TYPE_EN_NAME = entity.EMP_TYPE_EN_NAME,
                     AddedBy = entity.AddedBy,
                     AddedOn = entity.AddedOn,
                     Disable = entity.Disable,
                     UpdatedBy = entity.UpdatedBy,
                     updatedOn = entity.updatedOn
                 };
                 employeeTypesRepo.Add(cur);
                 return cur.EMP_TYPE_ID;
             });
        }

        public bool Update(EmployeeTypesVM entity)
        {
            EmployeeTypes cur = new EmployeeTypes()
            {
                EMP_TYPE_ID = entity.EMP_TYPE_ID,
                EMP_TYPE_AR_NAME = entity.EMP_TYPE_AR_NAME,
                EMP_TYPE_CODE = entity.EMP_TYPE_CODE,
                EMP_TYPE_EN_NAME = entity.EMP_TYPE_EN_NAME,
                AddedBy = entity.AddedBy,
                AddedOn = entity.AddedOn,
                Disable = entity.Disable,
                UpdatedBy = entity.UpdatedBy,
                updatedOn = entity.updatedOn
            };
            employeeTypesRepo.Update(cur, cur.EMP_TYPE_ID);
            return true;
        }

        public Task<bool> UpdateAsync(EmployeeTypesVM entity)
        {
            return Task.Run<bool>(() =>
            {
                EmployeeTypes cur = new EmployeeTypes()
                {
                    EMP_TYPE_ID = entity.EMP_TYPE_ID,
                    EMP_TYPE_AR_NAME = entity.EMP_TYPE_AR_NAME,
                    EMP_TYPE_CODE = entity.EMP_TYPE_CODE,
                    EMP_TYPE_EN_NAME = entity.EMP_TYPE_EN_NAME,
                    AddedBy = entity.AddedBy,
                    AddedOn = entity.AddedOn,
                    Disable = entity.Disable,
                    UpdatedBy = entity.UpdatedBy,
                    updatedOn = entity.updatedOn
                };
                employeeTypesRepo.Update(cur, cur.EMP_TYPE_ID);
                return true;
            });
        }



        public string GetLastCode()
        {
            var lastCode = employeeTypesRepo.GetAll().OrderByDescending(e => e.EMP_TYPE_ID).FirstOrDefault();
            return lastCode.EMP_TYPE_CODE;
        }
    }
}
