using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ITPAY_TYPESService
    {
        bool Insert(TPAY_TYPESVM entity);
        Task<int> InsertAsync(TPAY_TYPESVM entity);
        bool Update(TPAY_TYPESVM entity);
        Task<bool> UpdateAsync(TPAY_TYPESVM entity);
        bool Delete(TPAY_TYPESVM entity);
        Task<bool> DeleteAsync(TPAY_TYPESVM entity);
        Task<List<TPAY_TYPESVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        Task<List<TPAY_TYPESVM>> GetAll();
        string GetLastCode();
        Task<TPAY_TYPESVM> GetByID(int PayTypeID);
        Task<List<TPAY_TYPESVM>> GetAllActive();
    }
}
