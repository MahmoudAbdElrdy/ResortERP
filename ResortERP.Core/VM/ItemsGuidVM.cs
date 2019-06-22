using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
    public class ItemsGuidVM
    {
        public int id { get; set; }
        public string text { get; set; }
        public Nullable<int> PARENT_ACC_ID { get; set; }
        public ItemsDataGuidVM data { get; set; }
        public List<ItemsGuidVM> children { get; set; }

    }

    public class ItemsDataGuidVM
    {
        public int data { get; set; }
    }
    //{
    //   public int id { set; get; }
    //   public string text { set; get; }

    //    public List<childItem> children = new List<childItem>(); 
    //}

    //public class childItem
    //{
    //    public int id { set; get; }
    //    public string text { set; get; }

    //}
}
