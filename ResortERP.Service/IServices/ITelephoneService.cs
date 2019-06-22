using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ITelephoneService
    {
        bool Insert(TelephoneVM entity);
        Task<int> InsertAsync(TelephoneVM entity);
        bool Update(TelephoneVM entity);
        Task<bool> UpdateAsync(TelephoneVM entity);
        bool Delete(TelephoneVM entity);
        Task<bool> DeleteAsync(TelephoneVM entity);
        Task<List<TelephoneVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<TelephoneVM> GetAll();
        List<TelephoneVM> GetBy(int CatID, int TypeID);


        bool InsertList(List<TelephoneVM> entity);
        Task<List<TelephoneVM>> InsertListAsync(List<TelephoneVM> entity);
        bool DeleteAllByTeleID(TelephoneVM entity);

        List<TelephoneVM> GetByType(int TypeID);
    }
}
