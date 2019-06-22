using System;

namespace ResortERP.Core.VM
{

    public class AssetCompanyDetailsVM
    {
        public int ID { get; set; }
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
