using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IEntrySettingService
    {
        bool Insert(Entry_SettingsVM entity);
        Task<int> InsertAsync(Entry_SettingsVM entity);
        bool Update(Entry_SettingsVM entity);
        Task<bool> UpdateAsync(Entry_SettingsVM entity);

        bool Delete(Entry_SettingsVM entity);
        Task<bool> DeleteAsync(Entry_SettingsVM entity);
        Task<List<Entry_SettingsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<Entry_SettingsVM> GetAll();

        Task<Entry_SettingsVM> GetEntrySettingByID(int typeID);
        Entry_SettingsVM GetEntrySettingBySettingID(int settingID);
        int GetEntryTypeBySettingID(int settingID);
    }
}
