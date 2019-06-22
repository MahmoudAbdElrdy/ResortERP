using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class AssetDepreciationType
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string ARName { get; set; }
        public string LatName { get; set; }        
        public string Notes { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Active { get; set; }
        public int Position { get; set; }
    }
}
