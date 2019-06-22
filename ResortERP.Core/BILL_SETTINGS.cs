using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class BILL_SETTINGS
    {
        [Key]
        public short BILL_SETTING_ID { get; set; }
        public string BILL_AR_NAME { get; set; }
        public string BILL_EN_NAME { get; set; }
        public string BILL_ABRV_AR { get; set; }
        public string BILL_ABRV_EN { get; set; }
        public int BILL_TYPE_ID { get; set; }
        public Nullable<int> ACC_ITEM_ID { get; set; }
        public Nullable<int> ACC_DISC_ID { get; set; }
        public Nullable<int> ACC_EXTRA_ID { get; set; }
        public Nullable<int> ACC_PAY_ID { get; set; }
        public Nullable<int> ACC_COST_ID { get; set; }
        public Nullable<int> ACC_STORE_ID { get; set; }
        public Nullable<int> ACC_GIFT_ID { get; set; }
        public Nullable<short> COST_CENTER_ID { get; set; }
        public Nullable<short> COM_STORE_ID { get; set; }
        public Nullable<short> TO_COM_STORE_ID { get; set; }
        public string GENERATE_ENTRY_STATE { get; set; }
        public bool IS_AUTO_POSTING { get; set; }
        public bool CONTINEOUS_INVENTORY { get; set; }
        public bool PRICE_COST_EFFECT { get; set; }
        public bool LAST_PAY_PRICE { get; set; }
        public string STORE_EFFECT_STATE { get; set; }
        public bool ABRV_BILL { get; set; }
        public bool SHOW_COST_CENTER { get; set; }
        public bool SHOW_EMPLOYEE { get; set; }
        public Nullable<short> CURRENCY_ID { get; set; }
        public Nullable<double> CURRENCY_RATE { get; set; }
        public Nullable<int> DEFAULT_COLOR { get; set; }
        public string DEFAULT_COLOR_HEXA { get; set; }
        public Nullable<int> ALTERNATE_COLOR { get; set; }
        public string ALTERNATE_COLOR_HEXA { get; set; }
        public Nullable<bool> OUT_MINUS { get; set; }
        public Nullable<bool> BILL_SHORTCUT { get; set; }
        public Nullable<bool> FIRST_EXPIRE { get; set; }
        public Nullable<bool> MIN_QTY { get; set; }
        public Nullable<bool> SERIAL_NUMBER { get; set; }
        public Nullable<byte> BILL_SELL_TYPE_ID { get; set; }
        public Nullable<byte> SEARCH_ONLY_BY_DEAULT_UNIT { get; set; }
        public Nullable<bool> PRINTHALFPAGE { get; set; }
        public Nullable<bool> PRINTLANDSCIP { get; set; }
        public string BILL_SHOW_NAME { get; set; }
        public int BILL_PAY_TYPE { get; set; }
        public bool DISABLE_BILL_PAY_TYPE { get; set; }
        public bool SHOW_BILL_PAY_TYPE { get; set; }
        public bool AUTOMATIC_DISCOUNT { get; set; }
        public bool PRINTHALFPAGEBYLONG { get; set; }
        public bool MODULE_CARS { get; set; }
        public bool PRINT_BILL_AUTOMATIC { get; set; }
        public bool SHOW_THE_RETURN_PERCENTAGE { get; set; }
        public bool SHOW_THE_LAST_DATE_FOR_RETURN { get; set; }
        public bool SHOW_THE_LAST_BALANCE_ON_THE_BILL { get; set; }
        public bool PRINT_BILL_AS_RESET_OR_AS_BILL { get; set; }
        public double NUMBEROFCOPIES { get; set; }
        public Nullable<bool> IS_IT_CASHER_BILL { get; set; }
        public Nullable<bool> REPEATETHE_ITEM_AT_THE_BILL { get; set; }
        public string FIRSTBILLMESSAGE { get; set; }
        public string SECONDBILLMESSAGE { get; set; }
        public string HEADERBILLMESSAGE { get; set; }
        public Nullable<bool> SHOW_THE_COMPANY_DATA_ON_BILL { get; set; }
        public Nullable<bool> SHOW_THE_CURRENT_QTY_ON_BILL { get; set; }
        public Nullable<bool> IS_IT_SERVICE_BILL { get; set; }
        public Nullable<bool> GENERATE_COST_ENTRY { get; set; }
        public Nullable<bool> SHOW_THE_ITEM_CODE_ON_BILL { get; set; }
        public Nullable<bool> ADD_BILL_ENTRY_WITH_THE_MAIN_CURRENCY_RATE { get; set; }
        public Nullable<bool> SHOW_THE_CURRENCY { get; set; }
        public Nullable<bool> THESECONDCELLISTHEITEMCELLNOTTHEQUANTITY { get; set; }
        public Nullable<bool> IS_IT_SALES_POINT { get; set; }
        public Nullable<double> PST { get; set; }
        public Nullable<double> GST { get; set; }
        public Nullable<bool> IS_IT_TAX_SALE_BILL { get; set; }
        public Nullable<bool> IS_IT_THREADING { get; set; }
        public Nullable<bool> SATTLEMENT_INCOME_LIST { get; set; }
        public string PSTNAME { get; set; }
        public string GSTNAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }

        public double? Tax { get; set; }
        public double? CommissionTax { get; set; }


        public bool? IsBillDate { get; set; }
        public bool? IsShiftNumber { get; set; }
        public bool? IsSellType { get; set; }
        public bool? IsPayWay { get; set; }
        public bool? IsAccount { get; set; }
        public bool? IsCustAccID { get; set; }
        public bool? IsItemAccID { get; set; }
        public bool? IsCompStoreID { get; set; }
        public bool? IsEmpID { get; set; }
        public bool? IsBillRemarks { get; set; }
        public bool? IsCurrency { get; set; }
        public bool? IsPayTypes { get; set; }
        public bool? IsCurrencyTrans { get; set; }
        public string BillAccountName { get; set; }
        public string BillEmpName { get; set; }

        public bool? IsTotalExtra { get; set; }
        public bool? IsTotalPaid { get; set; }
        // public bool? IsTotalPrice { get; set; }
        public bool? IsTotalMustPaid { get; set; }
        public bool? IsTotalRemaining { get; set; }
        public bool? IsToCompStore { get; set; }

        public int? AccWagesID { get; set; }
        public bool? IsItemsGroups { get; set; }
        public bool? IsItems { get; set; }

        public int? AccSalesID { get; set; }
        public int? AccVatRateID { get; set; }
        public int? MenuID { get; set; }


        public int? CashAccountID { get; set; }
        public int? VisaAccountID { get; set; }
        public int? NoPaidAccountID { get; set; }
        public int? PaymentAccountID { get; set; }


        public int? AccWagesTaxID { get; set; }
        public double? WagesTaxValue { get; set; }

        public int? ShowPriceTypeID { get; set; }

        public int? PurchasAccID { get; set; }
        public int? PurchasTaxAccID { get; set; }



        public int? AccCommissionID { get; set; }
        public int? AccCommissionTaxID { get; set; }


        public int? AccSalesGoldID { get; set; }
        public int? AccPurchaseGoldID { get; set; }

        public bool? Show_Wages_Discount { get; set; }

        public bool? IsQuantityCalculated { get; set; }

        public int? AccGoldID { get; set; }

        public bool? IsShowEditReason { get; set; }
        public bool? IsShowDeliveryPerson { get; set; }

        public bool? IsShowExternalNumber { get; set; }
        public bool? IsEnableTaxEdit { get; set; }
        public bool? IsShowGoldBoxBalance { get; set; }
        public bool? IsShowCustomerBalance { get; set; }
        public int? BillRowsNumber { get; set; }

        public bool? IsCalcClearnessRate { get; set; }

        public bool? IsBillDiscValue { get; set; }
        public bool? IsBillDiscRate { get; set; }
        public bool? IsTotalDiscValues { get; set; }
        public bool? IsTotalDiscRates { get; set; }

        public double? BillVatRate { get; set; }
        public bool? IsBillVatRate { get; set; }
        public bool? IsCalcBillDiscOfTotal { get; set; }

        public int? TimesNumberOfPrinting { get; set; }

        public bool? IsRepeatItem { get; set; }
        public bool? IsQuickAccount { get; set; }

        public bool? IsEntryGoldItemsAccounts { get; set; }

        public bool? IsTotalsSummary { get; set; }


        public double? ExemptVatRate { get; set; }

        public double? MainVatRate { get; set; }

        public bool? IsShowTaxesBox { get; set; }

        public bool? IsEnableWeight { get; set; }
        public bool? IsEnableGmWages { get; set; }
        public bool? IsLockPrice { get; set; }
    }
}
