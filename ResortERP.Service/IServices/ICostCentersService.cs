using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ResortERP.Service.IServices
{
    public interface ICostCentersService
    {
        bool Insert(CostCentersVM entity);
        Task<int> InsertAsync(CostCentersVM entity);
        bool Update(CostCentersVM entity);
        Task<bool> UpdateAsync(CostCentersVM entity);
        bool Delete(CostCentersVM entity);
        Task<bool> DeleteAsync(CostCentersVM entity);
        Task<List<CostCentersVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<CostCentersVM> GetAll();
        Task<List<CostCentersVM>> GetSearchForCostCenter(string Search);
        List<CostCentersVM> GetMainCostCenters();
        string GetLastCode();
        CostCentersVM getById(int COST_CENTER_ID);
        List<CostCentersGuidVM> getForTree();
    }
}
