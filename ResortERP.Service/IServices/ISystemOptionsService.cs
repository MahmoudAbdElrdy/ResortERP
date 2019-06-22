using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ISystemOptionsService
    {
        Task<bool> Insert(System_OptionsVM entity);
        Task<string> InsertAsync(System_OptionsVM entity);
        bool Update(System_OptionsVM entity);
        Task<bool> UpdateAsync(System_OptionsVM entity);
        bool Delete(System_OptionsVM entity);
        Task<bool> DeleteAsync(System_OptionsVM entity);
        Task<List<System_OptionsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<System_OptionsVM> GetAll();
        Task<System_OptionsVM> GetByUserName(string userName);
        Task<bool> SaveAll(SystemOptionsComplexVM ComplexObj);
        bool insertFilterForCompany(string[] entity);
        bool? getFilterCompValue();
    }
}
