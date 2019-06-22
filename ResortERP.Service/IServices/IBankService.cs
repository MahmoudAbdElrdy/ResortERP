using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
   public interface IBankService
    {
        Task<List<BankVM>> getBankAsync(int pageNum, int pageSize);
        Task<int> insertBankSync(BankVM entity);
        Task<bool> updateBankSync(BankVM entity);
        Task<bool> deleteBankSync(BankVM entity);
        Task<int> countBankAsync();
        string GetLastCode();
        List<Bank> getByCurrency(int currencyId);
        List<Bank> getByNationID(int nationId);
        List<Bank> getByGOVID(int govId);
        List<Bank> getByTownID(int townId);
        List<Bank> getByVilID(long villageId);

        Task<BankVM> getById(int ID);
        Task<List<BankVM>> GetSearchResultAsync(BankVM Bank, int pageNum, int pageSize);
        List<BankVM> getAll();
    }
}
