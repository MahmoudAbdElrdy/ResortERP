using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IBillCaliberTransactionsService
    {

        Task<int> CountAsync();
        Task<bool> DeleteAsync(BillCaliberTransactionsVM entity);
        Task<List<BillCaliberTransactionsVM>> GetAllAsync(int pageNum, int pageSize);
        Task<bool> InsertAsync(BillCaliberTransactionsVM entity);
        Task<bool> UpdateAsync(BillCaliberTransactionsVM entity);
        Task<List<BillCaliberTransactionsVM>> getByMasterID(int masterID);
        Task<bool> UpdateWithMasterID(List<BillCaliberTransactionsVM> entityList, int masterID);


        bool DeleteByMasterID(int? MasterID);
        Task<bool> InsertWithMasterID(List<BillCaliberTransactionsVM> caliberTransList);
    }
}
