using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IIncomeAccountTypesService
    {
        bool Insert(Income_Account_TypesVM entity);
        Task<bool> InsertAsync(Income_Account_TypesVM entity);
        bool Update(Income_Account_TypesVM entity);
        Task<bool> UpdateAsync(Income_Account_TypesVM entity);
        bool Delete(Income_Account_TypesVM entity);
        Task<bool> DeleteAsync(Income_Account_TypesVM entity);
        Task<List<Income_Account_TypesVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<Income_Account_TypesVM> GetAll();
    }
}
