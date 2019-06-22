using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class AssetOperationDetails
    {
        [Key]
        public int ID { get; set; }
        public int RowNumber { get; set; }
        public int AssetOperationMasterID { get; set; }
        public int AssetMasterID { get; set; }
        public double? Value { get; set; }
        public int? SellerAccountID { get; set; }
        public int? FromDepartmentID { get; set; }
        public int? ToDepartmentID { get; set; }
        public int? FromEmployeeID { get; set; }
        public int? ToEmployeeID { get; set; }
        public bool? IsAssetDeliveredFromEmployee { get; set; }
        public int? CostCenterID { get; set; }
        public int? DistributorID { get; set; }
        public double? AssetExtrasSum { get; set; }
        public double? AssetEliminationSum { get; set; }
        public double? AssetDepreciationSum { get; set; }
        public string Notes { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Active { get; set; }
        public int Position { get; set; }
    }
}
