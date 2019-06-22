namespace ResortERP.Core.VM
{
    public class ItemsGroupsVM
    {
        public int GroupID { get; set; }
        public string GroupCode { get; set; }

        public string GroupARName { get; set; }
        public string GroupENName { get; set; }
        public int? GroupMasterID { get; set; }
        public string GroupMasterName { get; set; }
        public string GroupRemarks { get; set; }
        public bool AppearOnSalePoint { get; set; }
        public int? CaliberID { get; set; }

        public bool DOESTHEQUANTITYISAPARTOFBARCODE { get; set; }
        public double QUANTITYLENGTHATTHEBARCODE { get; set; }
        public double QUANTITYSTARTATTHEBARCODE { get; set; }

        public double QUANTITYPARTSTARTATTHEBARCODE { get; set; }
        public double QUANTITYPARTLENGTHATTHEBARCODE { get; set; }
        public int? COST_CALCULATION_TYPE { get; set; }

        public int? ItemStatusID { get; set; }

        public int? GoldAccID { get; set; }

    }
}
