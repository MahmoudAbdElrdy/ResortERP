using ResortERP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface ISmartSysDatabaseViewService
    {
        Task<List<SmartSysDataBases_View>> GetAllAsync(int pageNum, int pageSize);
        Task<int> CountAsync();
        List<SmartSysDataBases_View> GetAll();
        List<SmartSysDataBases_View> GetBy(string DatabaseName);

    }
}
