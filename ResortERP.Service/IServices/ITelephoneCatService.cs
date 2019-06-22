using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ITelephoneCatService
    {
        bool Insert(TelephoneCatVM entity);
        Task<int> InsertAsync(TelephoneCatVM entity);
        bool Update(TelephoneCatVM entity);
        Task<bool> UpdateAsync(TelephoneCatVM entity);
        bool Delete(TelephoneCatVM entity);
        Task<bool> DeleteAsync(TelephoneCatVM entity);
        Task<List<TelephoneCatVM>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<TelephoneCatVM> GetAll();
        TelephoneCatVM GetByID(int TypeID);
    }
}
