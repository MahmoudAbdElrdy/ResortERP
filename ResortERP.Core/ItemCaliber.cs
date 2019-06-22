using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class ItemCaliber
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string ARName { get; set; }
        public string LatName { get; set; }
        public double? ClearnessRate { get; set; }
        public double? CaliberNumber { get; set; }
        public string Notes { get; set; }
        public int? AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public int? Active { get; set; }
        public int? Position { get; set; }



    }
}
