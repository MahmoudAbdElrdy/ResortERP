using System.Collections.Generic;

namespace ResortERP.Core.VM
{
    public class ItemUnitsInsertedDeleted
    {
      public  List<CustomItemUnitsVM> ItemUnitsList { get; set; }
       public int[] DeltedItemUnits { get; set; }
    }
}
