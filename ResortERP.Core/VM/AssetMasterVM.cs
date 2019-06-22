using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class AssetMasterVM
    {
        public AssetMasterVM()
        {
            Active = true;
            Position = 1;
        }
        public int ID { get; set; }
        public string Code { get; set; }
        public string ARName { get; set; }
        public string LatName { get; set; }
        public string Barcode { get; set; }
        public int? AssetGroupID { get; set; }
        public int? DepartmentID { get; set; }
        public int? ManufactureCompanyID { get; set; }
        public int? SupplierCompanyID { get; set; }
        public int? AssetTypeID { get; set; }
        public int? AssetStatusID { get; set; }
        public int? OriginNationID { get; set; }
        public double? AssetWidth { get; set; }
        public double? AssetHeight { get; set; }
        public double? AssetWeight { get; set; }
        public string AssetColor { get; set; }
        public string AssetBrand { get; set; }
        public string AssetModel { get; set; }
        public string AssetLocation { get; set; }
        public string AssetScreenNumber { get; set; }
        public int? AssetAccountID { get; set; }
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

        public string Asset_Base64_Photo { get; set; }
        public byte[] AssetPhoto { get; set; }
        public string AssetGroup { get; set; }

        public DateTime? ManufactureDate { get; set; }
        public string WarrantyNumber { get; set; }
        public DateTime? WarrantyStartDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public string ReceivingNotes { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? PurchasingDate { get; set; }
        public string CustomsStatment { get; set; }
        public DateTime? CustomsStatmentDate { get; set; }
        public string ShippingNumber { get; set; }
        public int? AssetShippingMethodID { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string ImportLicenseNumber { get; set; }
        public string ShippingDestination { get; set; }
        public DateTime? ShippingArrivalDate { get; set; }

        public AssetDepreciationDetailsVM Depreciation { get; set; }
    }
}
