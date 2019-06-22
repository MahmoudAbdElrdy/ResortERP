using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
   public class ITEMS
    {
        [Key]
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public int GROUP_ID { get; set; }
        public string ITEM_AR_NAME { get; set; }
        public string ITEM_EN_NAME { get; set; }
        public string ITEM_REMARKS { get; set; }
        public bool PRODUCTION_DATE { get; set; }
        public bool EXPIRED_DATE { get; set; }
        public bool SERIAL_IN_INPUT { get; set; }
        public bool SERIAL_IN_OUTPUT { get; set; }
        public byte[] ITEM_PIC { get; set; }
        public Nullable<double> MIN_QTY { get; set; }
        public Nullable<byte> ITEM_TYPE { get; set; }
        public Nullable<int> ITEM_COLOR { get; set; }
        public string ITEM_MODEL { get; set; }
        public string ITEM_BRAND { get; set; }
        public Nullable<int> COMPANY_ID { get; set; }
        public double SELLEDISCOUNT { get; set; }
        public double BURCHASEDISCOUNT { get; set; }
        public bool DOESTHEQUANTITYISAPARTOFBARCODE { get; set; }
        public double QUANTITYLENGTHATTHEBARCODE { get; set; }
        public double QUANTITYSTARTATTHEBARCODE { get; set; }

        public double QUANTITYPARTSTARTATTHEBARCODE { get; set; }
        public double QUANTITYPARTLENGTHATTHEBARCODE { get; set; }

        public Nullable<byte> COST_CALCULATION_TYPE { get; set; }

        public bool? SubjectToVAT { get; set; }
        public double? VATRate { get; set; }
        public int? CaliberID { get; set; }
        //public string ARName { get; set; }
        //public string LatName { get; set; }
        //public double? ClearnessRate { get; set; }

        public double? GlobalGoldPrice { get; set; }
        public double? ManufacturingWages { get; set; }
        public double? ProfitMargin { get; set; }
        public double? ItemPrice { get; set; }
        public double? ItemWeight { get; set; }
        public int? ItemStatus { get; set; }
        public double? LowestSellingPrice { get; set; }

        

        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }

        public double? KiloPrice { get; set; }
        public double? OuncePrice { get; set; }

        public double? itemGmWages { get; set; }
        public bool? IsItemCodeRelatedByGroup { get; set; }

        public int? GoldAccID { get; set; }
        public double? ClearnessRate { get; set; }

        public double? GemsWeight { get; set; }
        public double? GemsWages { get; set; }
    }
}
