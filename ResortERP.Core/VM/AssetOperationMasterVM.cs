using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class AssetOperationMasterVM
    {
        public AssetOperationMasterVM()
        {
            Active = true;
            Position = 1;
        }
        public int ID { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public int? AssetOperationTypeID { get; set; }
        public DateTime? StartDate { get; set; }
        public int? FromAccountID { get; set; }
        public int? ToAccountID { get; set; }
        public int? CostCenterID { get; set; }
        public int? SellerAccountID { get; set; }
        public int? DepartmentID { get; set; }
        public int? DistributorID { get; set; }
        public int? CurrencyID { get; set; }
        public double? CurrencyRate { get; set; }
        public string OperationStatment { get; set; }
        public string Notes { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Active { get; set; }
        public int Position { get; set; }

        public long ENTRY_ID { get; set; }

        public List<AssetOperationDetailsVM> OperationDetails { get; set; }
    }
}
