using ResortERP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IUIDViewService
    {
        Task<List<UID_View>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<UID_View> GetAll();
        Task<UID_View> GetBy(string DatabaseName);
        Task<byte[]> GetByLOGO(string DatabaseName);
        Task<UID_View> GetCompanyAsync(string DatabaseName);
        UID_View GetCompany(string DatabaseName);
    }
}
