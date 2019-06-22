using ResortERP.Core;
using ResortERP.Core.VM;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IGoldWorldPriceService
    {
        bool Insert(GoldWorldPriceVM entity);
        Task<int> InsertAsync(GoldWorldPriceVM entity);
        bool Update(GoldWorldPriceVM entity);
        Task<bool> UpdateAsync(GoldWorldPriceVM entity);
        bool Delete(GoldWorldPriceVM entity);
        Task<bool> DeleteAsync(GoldWorldPriceVM entity);
        Task<List<GoldWorldPriceVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<GoldWorldPriceVM> getAllGoldWorldPrices();
        List<GoldWorldPriceVM> GetAllItemGroupsPos();
        Task<double> GetLastGoldWorldPrice();
        Task<bool> UpdateItemPrices(GoldWorldPrice entity);
        string GetLastCode();
        double? GetLastGoldPrice();
        Task<GoldWorldPriceVM> GetLastGoldWorldPriceData();

        Task<GoldWorldPriceVM> getById(int ID);

    }
}