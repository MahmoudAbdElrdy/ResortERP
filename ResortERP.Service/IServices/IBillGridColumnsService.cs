using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IBillGridColumnsService
    {
        bool Insert(BILL_GRID_COLUMNSVM entity);
        Task<int> InsertAsync(BILL_GRID_COLUMNSVM entity);
        bool Update(BILL_GRID_COLUMNSVM entity);
        Task<bool> UpdateAsync(BILL_GRID_COLUMNSVM entity);

        bool Delete(BILL_GRID_COLUMNSVM entity);
        Task<bool> DeleteAsync(BILL_GRID_COLUMNSVM entity);
        Task<List<BILL_GRID_COLUMNSVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        Task<BILL_GRID_COLUMNSVM> GetBySettingID(int settingID);
    }
}
