using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class AssetCompanyDetails
    {
        [Key]
        public int ID { get; set; }
        //public string ManufactureCompanyName { get; set; }
        public int? CompanyNationID { get; set; }
        public int? CompanyGovernerateID { get; set; }
        public int? CompanyTownID { get; set; }
        public int? CompanyVillageID { get; set; }
        public string CompanyAddressDetails { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyMobile { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanySite { get; set; }
        //public DateTime? ManufactureDate { get; set; }
        //public string WarrantyNumber { get; set; }
        //public DateTime? WarrantyStartDate { get; set; }
        //public DateTime? WarrantyEndDate { get; set; }
        //public string SupplierCompanyName { get; set; }
        //public int? SupplierCompanyNationID { get; set; }
        //public int? SupplierCompanyGovernerateID { get; set; }
        //public int? SupplierCompanyTownID { get; set; }
        //public int? SupplierCompanyVillageID { get; set; }
        //public string SupplierCompanyAddressDetails { get; set; }
        //public string SupplierCompanyPhone { get; set; }
        //public string SupplierCompanyMobile { get; set; }
        //public string SupplierCompanyFax { get; set; }
        //public string SupplierCompanyEmail { get; set; }
        //public string SupplierCompanySite { get; set; }
        //public DateTime? ReceivingDate { get; set; }
        //public string ReceivingNotes { get; set; }
        //public string ContractNumber { get; set; }
        //public DateTime? ContractDate { get; set; }
        //public DateTime? PurchasingDate { get; set; }
        //public string CustomsStatment { get; set; }
        //public DateTime? CustomsStatmentDate { get; set; }
        //public string ShippingNumber { get; set; }
        //public int? AssetShippingMethodID { get; set; }
        //public DateTime? ShippingDate { get; set; }
        //public string ImportLicenseNumber { get; set; }
        //public string ShippingDestination { get; set; }
        //public DateTime? ShippingArrivalDate { get; set; }
        public string Notes { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Active { get; set; }
        public int Position { get; set; }
        public string Code { get; set; }
        public string ENName { get; set; }
        public string ARName { get; set; }
        public short Type { get; set; }
    }
}
