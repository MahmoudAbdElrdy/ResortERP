using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IBillSettingsService
    {
        bool Insert(BILL_SETTINGSVM entity);
        Task<int> InsertAsync(BILL_SETTINGSVM entity);
        bool Update(BILL_SETTINGSVM entity);
        Task<bool> UpdateAsync(BILL_SETTINGSVM entity);

        bool Delete(BILL_SETTINGSVM entity);
        Task<bool> DeleteAsync(BILL_SETTINGSVM entity);
        Task<List<BILL_SETTINGSVM>> GetAllAsync(int pageNum, int pageSize);
        List<BILL_SETTINGSVM> GetAll();
        Task<int> CountAsync();
        Task<Dictionary<string, int>> CheckExist(BILL_SETTINGSVM entity);

        Task<BILL_SETTINGSVM> GetBillSettingByID(byte typeID);
        BILL_SETTINGSVM GetBillSettingByBillID(int id);
    }
}
