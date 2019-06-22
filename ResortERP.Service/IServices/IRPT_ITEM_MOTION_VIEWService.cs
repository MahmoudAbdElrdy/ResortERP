using ResortERP.Core.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IRPT_ITEM_MOTION_VIEWService
    {
        Task<List<RPT_ITEM_MOTION_VIEW_VM>> GetAllAsync(int pageNum, int pageSize);
        List<RPT_ITEM_MOTION_VIEW_VM> GetAll();
        List<RPT_ITEM_MOTION_VIEW_VM> GetingSearchItems(SearchingmainItems smItem, List<BILL_SETTINGSVM> bilSettings, List<ReportOptions> rptOptions);
    }
}
