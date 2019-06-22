using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IEntryGridColumnsService
    {
        bool Insert(Entry_Grid_ColumnsVM entity);
        Task<int> InsertAsync(Entry_Grid_ColumnsVM entity);
        bool Update(Entry_Grid_ColumnsVM entity);
        Task<bool> UpdateAsync(Entry_Grid_ColumnsVM entity);

        bool Delete(Entry_Grid_ColumnsVM entity);
        Task<bool> DeleteAsync(Entry_Grid_ColumnsVM entity);
        Task<List<Entry_Grid_ColumnsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<Entry_Grid_ColumnsVM> GetAll();
        Task<ENTRY_GRID_COLUMNS> GetByID(int EntrySettingID);
    }
}
