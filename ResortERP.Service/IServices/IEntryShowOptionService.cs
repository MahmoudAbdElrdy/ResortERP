using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IEntryShowOptionService
    {
        bool Insert(Entry_Show_OptionsVM entity);
        Task<bool> InsertAsync(Entry_Show_OptionsVM entity);
        bool Update(Entry_Show_OptionsVM entity);
        Task<bool> UpdateAsync(Entry_Show_OptionsVM entity);
        bool Delete(Entry_Show_OptionsVM entity);
        Task<bool> DeleteAsync(Entry_Show_OptionsVM entity);
        Task<List<Entry_Show_OptionsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<Entry_Show_OptionsVM> GetAll();
    }
}
