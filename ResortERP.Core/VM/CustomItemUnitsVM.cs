namespace ResortERP.Core.VM
{
    public class CustomItemUnitsVM
    {
        public int Item_ID { get; set; }
        public int ItemUnitID { get; set; }
        public string UNIT_CODE { get; set; }
        public string UNIT_AR_NAME { get; set; }
        public double UNIT_TRANS_RATE { get; set; }
        public bool IS_DEFAULT { get; set; }
        public string UNIT_BARCODE { get; set; }
        public int UNIT_ID { get; set; }
        public double? WHOLE_PRICE { get; set; }
        public double? HALF_WHOLE_PRICE { get; set; }
        public double? EMP_PRICE { get; set; }
        public double? EXPORT_PRICE { get; set; }
        public double? RETAIL_PRICE { get; set; }
        public double? CONSUMER_PRICE { get; set; }
        public double? LAST_BUY_PRICE { get; set; }
        public string Operation { get; set; }
    }
}
