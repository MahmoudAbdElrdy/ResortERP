using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class BILL_GRID_COLUMNS
    {
        [Key]
        public int BILL_GRID_ID { get; set; }
        public short BILL_SETTING_ID { get; set; }
        public byte ITEM_INDEX { get; set; }
        public short ITEM_WIDTH { get; set; }
        public byte QTY_INDEX { get; set; }
        public short QTY_WIDTH { get; set; }
        public byte UNIT_COST_INDEX { get; set; }
        public short UNIT_COST_WIDTH { get; set; }
        public byte TOTAL_INDEX { get; set; }
        public short TOTAL_WIDTH { get; set; }
        public byte GIFTS_INDEX { get; set; }
        public short GIFTS_WIDTH { get; set; }
        public byte DISC_RATE_INDEX { get; set; }
        public short DISC_RATE_WIDTH { get; set; }
        public byte DISC_VALUE_INDEX { get; set; }
        public short DISC_VALUE_WIDTH { get; set; }
        public byte EXTRA_RATE_INDEX { get; set; }
        public short EXTRA_RATE_WIDTH { get; set; }
        public byte EXTRA_VALUE_INDEX { get; set; }
        public short EXTRA_VALUE_WIDTH { get; set; }
        public byte PRODUCTION_DATE_INDEX { get; set; }
        public short PRODUCTION_DATE_WIDTH { get; set; }
        public byte EXPIRED_DATE_INDEX { get; set; }
        public short EXPIRED_DATE_WIDTH { get; set; }
        public byte BILL_REMARKS_INDEX { get; set; }
        public short BILL_REMARKS_WIDTH { get; set; }
        public byte UNIT_INDEX { get; set; }
        public short UNIT_WIDTH { get; set; }
        public Nullable<byte> ITEM_REMAIN_QTY_INDEX { get; set; }
        public Nullable<short> ITEM_REMAIN_QTY_WIDTH { get; set; }
        public Nullable<byte> CTOTALCURR_INDEX { get; set; }
        public Nullable<short> CTOTALCURR_WIDTH { get; set; }
        public Nullable<byte> CEXTERNALLEXPENSES_INDEX { get; set; }
        public Nullable<short> CEXTERNALLEXPENSES_WIDTH { get; set; }
        public Nullable<byte> CTOTALWITHEXPENSES_INDEX { get; set; }
        public Nullable<short> CTOTALWITHEXPENSES_WIDTH { get; set; }
        public Nullable<byte> CINVENTORYVALUE_INDEX { get; set; }
        public Nullable<short> CINVENTORYVALUE_WIDTH { get; set; }
        public Nullable<byte> WHOLE_INDEX { get; set; }
        public Nullable<short> WHOLE_WIDTH { get; set; }
        public Nullable<byte> HALF_WHOLE_INDEX { get; set; }
        public Nullable<short> HALF_WHOLE_WIDTH { get; set; }
        public Nullable<byte> RETAIL_INDEX { get; set; }
        public Nullable<short> RETAIL_WIDTH { get; set; }
        public Nullable<byte> END_USERS_INDEX { get; set; }
        public Nullable<short> END_USERS_WIDTH { get; set; }
        public Nullable<byte> EMPLOYEE_INDEX { get; set; }
        public Nullable<short> EMPLOYEE_WIDTH { get; set; }
        public Nullable<byte> EXPORT_INDEX { get; set; }
        public Nullable<short> EXPORT_WIDTH { get; set; }
        public Nullable<byte> LAST_BUY_INDEX { get; set; }
        public Nullable<short> LAST_BUY_WIDTH { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }

        public Nullable<byte> Cost_Center_INDEX { get; set; }
        public Nullable<short> Cost_Center_WIDTH { get; set; }


        public Nullable<byte> Item_Weight_INDEX { get; set; }
        public Nullable<short> Item_Weight_WIDTH { get; set; }

        public Nullable<byte> Item_Wages_INDEX { get; set; }
        public Nullable<short> Item_Wages_WIDTH { get; set; }

        public Nullable<byte> Manufact_Wages_INDEX { get; set; }
        public Nullable<short> Manufact_Wages_WIDTH { get; set; }

        public Nullable<byte> ARName_INDEX { get; set; }
        public Nullable<short> ARName_WIDTH { get; set; }

        public Nullable<byte> Clearness_Rate_INDEX { get; set; }
        public Nullable<short> Clearness_Rate_WIDTH { get; set; }

        public Nullable<byte> Caliber24_INDEX { get; set; }
        public Nullable<short> Caliber24_WIDTH { get; set; }

        public Nullable<byte> Caliber22_INDEX { get; set; }
        public Nullable<short> Caliber22_WIDTH { get; set; }

        public Nullable<byte> Caliber21_INDEX { get; set; }
        public Nullable<short> Caliber21_WIDTH { get; set; }

        public Nullable<byte> Caliber18_INDEX { get; set; }
        public Nullable<short> Caliber18_WIDTH { get; set; }

        public Nullable<byte> Subject_To_VAT_INDEX { get; set; }
        public Nullable<short> Subject_To_VAT_WIDTH { get; set; }

        public Nullable<byte> VAT_Rate_INDEX { get; set; }
        public Nullable<short> VAT_Rate_WIDTH { get; set; }

        public Nullable<byte> VatValue_INDEX { get; set; }
        public Nullable<short> VatValue_WIDTH { get; set; }


        public Nullable<byte> WagesDiscValue_INDEX { get; set; }
        public Nullable<short> WagesDiscValue_WIDTH { get; set; }

        public Nullable<byte> WagesDiscRate_INDEX { get; set; }
        public Nullable<short> WagesDiscRate_WIDTH { get; set; }


        public Nullable<byte> ActualWeight_INDEX { get; set; }
        public Nullable<short> ActualWeight_WIDTH { get; set; }


        public Nullable<byte> TaxTotal_INDEX { get; set; }
        public Nullable<short> TaxTotal_WIDTH { get; set; }


        public Nullable<byte> TotalItemGmWages_INDEX { get; set; }
        public Nullable<short> TotalItemGmWages_WIDTH { get; set; }

        public Nullable<byte> TotalGoldManufact_INDEX { get; set; }
        public Nullable<short> TotalGoldManufact_WIDTH { get; set; }

        public Nullable<byte> ExemptOfTax_INDEX { get; set; }
        public Nullable<short> ExemptOfTax_WIDTH { get; set; }


        public Nullable<byte> ItemPrice_INDEX { get; set; }
        public Nullable<short> ItemPrice_WIDTH { get; set; }


        public Nullable<byte> AfterDiscount_INDEX { get; set; }
        public Nullable<short> AfterDiscount_WIDTH { get; set; }

        public Nullable<byte> LockPrice_INDEX { get; set; }
        public Nullable<short> LockPrice_WIDTH { get; set; }

        
        public string ARCode { get; set; }
        public string ENCode { get; set; }
        public string ARItem { get; set; }
        public string ENItem { get; set; }
        public string ARQuantity { get; set; }
        public string ENQuantity { get; set; }
        public string ARItemWeight { get; set; }
        public string ENItemWeight { get; set; }
        public string ARItemGmWages { get; set; }
        public string ENItemGmWages { get; set; }
        public string ARManufacturingWages { get; set; }
        public string ENManufacturingWages { get; set; }
        public string ARUnit { get; set; }
        public string ENUnit { get; set; }
        public string ARCaliberName { get; set; }
        public string ENCaliberName { get; set; }
        public string ARPrice { get; set; }
        public string ENPrice { get; set; }
        public string ARDiscount { get; set; }
        public string ENDiscount { get; set; }
        public string ARDiscRate { get; set; }
        public string ENDiscRate { get; set; }
        public string ARTotal { get; set; }
        public string ENTotal { get; set; }
        public string ARClearnessRate { get; set; }
        public string ENClearnessRate { get; set; }
        public string ARCaliber24 { get; set; }
        public string ENCaliber24 { get; set; }
        public string ARCaliber22 { get; set; }
        public string ENCaliber22 { get; set; }
        public string ARCaliber21 { get; set; }
        public string ENCaliber21 { get; set; }
        public string ARCaliber18 { get; set; }
        public string ENCaliber18 { get; set; }
        public string ARCostCenter { get; set; }
        public string ENCostCenter { get; set; }
        public string ARSubjectToVat { get; set; }
        public string ENSubjectToVat { get; set; }
        public string ARVatRate { get; set; }
        public string ENVatRate { get; set; }

        public string ARVatValue { get; set; }
        public string ENVatValue { get; set; }

        public string ARWagesDiscValue { get; set; }
        public string ENWagesDiscValue { get; set; }

        public string ARWagesDiscRate { get; set; }
        public string ENWagesDiscRate { get; set; }

        public string ARActualWeight { get; set; }
        public string ENActualWeight { get; set; }


        public string ARTaxTotal { get; set; }
        public string ENTaxTotal { get; set; }

        public string ARTotalItemGmWages { get; set; }
        public string ENTotalItemGmWages { get; set; }

        public string ARTotalGoldManufact { get; set; }
        public string ENTotalGoldManufact { get; set; }

        public string ARExemptOfTax { get; set; }
        public string ENExemptOfTax { get; set; }

        public string ARItemPrice { get; set; }
        public string ENItemPrice { get; set; }

        public string ARAfterDiscount { get; set; }
        public string ENAfterDiscount { get; set; }

        public string ARLockPrice { get; set; }
        public string ENLockPrice { get; set; }
    }
}
