using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class AssetGroupVM
    {
        
        public int ID { get; set; }
        public string Code { get; set; }
        public string ARName { get; set; }
        public string LatName { get; set; }
        public int? AssetGroupID { get; set; }
        public int? AssetGroupAccountID { get; set; }
        public int? DepreciationAccountID { get; set; }
        public int? TotalDepreciationAccountID { get; set; }
        public int? ExpensesAccountID { get; set; }
        public int? CapitalProfitAccountID { get; set; }
        public int? CapitalLossAccountID { get; set; }
        public int? AppraisalExcessAccountID { get; set; }
        public int? ApprasialDeficitAccountID { get; set; }
        public string Notes { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Active { get; set; }
        public int Position { get; set; }

    }


}
