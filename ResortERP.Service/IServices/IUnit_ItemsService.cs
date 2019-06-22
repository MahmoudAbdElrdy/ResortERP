using ResortERP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.IServices
{
    public interface IUnit_ItemsService
    {
        List<ITEMS_UNITS> getByItemunits(int itemStatusID);
    }
}
