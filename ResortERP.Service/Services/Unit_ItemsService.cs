using ResortERP.Core;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class Unit_ItemsService : IUnit_ItemsService, IDisposable
    {
        IGenericRepository<ITEMS_UNITS> ItemUnitsRepo;
        public Unit_ItemsService(IGenericRepository<ITEMS_UNITS> ItemUnitsRepo)
        {
            this.ItemUnitsRepo = ItemUnitsRepo;
        }
        public void Dispose()
        {
            ItemUnitsRepo.Dispose();
        }

        public List<ITEMS_UNITS> getByItemunits(int unitID)
        {
            var q = ItemUnitsRepo.GetAll().Where(a => a.UNIT_ID == unitID).ToList();
            return q;
        }
    }
}
