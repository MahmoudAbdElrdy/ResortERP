using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IBillDetailsService
    {
        bool Insert(BILL_DETAILSVM entity);
        Task<bool> InsertAsync(BILL_DETAILSVM entity);
        bool Update(BILL_DETAILSVM entity);
        Task<bool> UpdateAsync(BILL_DETAILSVM entity);

        bool Delete(BILL_DETAILSVM entity);
        Task<bool> DeleteAsync(BILL_DETAILSVM entity);
        Task<List<BILL_DETAILSVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        bool InsertBillDetails(List<BILL_DETAILSVM> BillDetails);
        List<BILL_DETAILSVM> GetAllBillDetailsByBill_ID(long Bill_ID);
    }
}
