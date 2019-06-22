using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IShortcutService
    {
        bool Insert(ShortcutsVM entity);
        Task<bool> InsertAsync(ShortcutsVM entity);
        bool Update(ShortcutsVM entity);
        Task<bool> UpdateAsync(ShortcutsVM entity);
        bool Delete(ShortcutsVM entity);
        Task<bool> DeleteAsync(ShortcutsVM entity);
        Task<List<ShortcutsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<ShortcutsVM> GetAll();
        Task<List<ShortcutsVM>> getByUID(string userName);
    }
}
