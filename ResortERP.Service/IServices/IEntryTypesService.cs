using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IEntryTypesService
    {
        bool Insert(Entry_TypesVM entity);
        Task<bool> InsertAsync(Entry_TypesVM entity);
        bool Update(Entry_TypesVM entity);
        Task<bool> UpdateAsync(Entry_TypesVM entity);

        bool Delete(Entry_TypesVM entity);
        Task<bool> DeleteAsync(Entry_TypesVM entity);
        Task<List<Entry_TypesVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<Entry_TypesVM> GetAll();
    }
}
