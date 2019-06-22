using ResortERP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IDashBoardService
    {
        DashBoard InsertDashBoardPer(DashBoard id);
        DashBoard get(int id);
    }
}
