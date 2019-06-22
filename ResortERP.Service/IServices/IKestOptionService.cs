using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IKestOptionService
    {
        bool Insert(Kest_OptionVM entity);
        Task<bool> InsertAsync(Kest_OptionVM entity);
        bool Update(Kest_OptionVM entity);
        Task<bool> UpdateAsync(Kest_OptionVM entity);
        bool Delete(Kest_OptionVM entity);
        Task<bool> DeleteAsync(Kest_OptionVM entity);
        Task<List<Kest_OptionVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<Kest_OptionVM> GetAll();
    }
}
