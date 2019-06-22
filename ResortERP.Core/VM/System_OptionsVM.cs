using System;

namespace ResortERP.Core.VM
{
    public class System_OptionsVM
    {
        public string UID { get; set; }
        public bool END_SYSTEM { get; set; }
        public bool ON_UPDATE { get; set; }
        public bool ON_SAVE { get; set; }
        public bool ON_DELETE { get; set; }
        public bool DATE_AFTER { get; set; }
        public bool SAME_ITEM_ON_BILL { get; set; }
        public bool SAME_ACC_ON_ENTRY { get; set; }
        public bool ON_POST_ENTRY { get; set; }
        public bool ON_POST_STORE { get; set; }
        public bool ON_POST_ACC { get; set; }
        public System.DateTime? FIRST_DATE { get; set; }
        public System.DateTime? END_DATE { get; set; }
        public System.DateTime? OPERATION_DATE { get; set; }
        public Nullable<int> DEFAULT_CURRENCY { get; set; }
        public Nullable<byte> QTY { get; set; }
        public Nullable<byte> PRICE { get; set; }
        public Nullable<byte> RATE { get; set; }
        public string PRINTER_ID { get; set; }
        public bool ITEM_CODE { get; set; }
        public bool BILL_CODE { get; set; }
        public bool SERVICE_CODE { get; set; }
        public bool ITEM_GROUP_CODE { get; set; }
        public bool SERVICE_GROUP_CODE { get; set; }
        public bool ENTRY_CODE { get; set; }
        public bool ON_ASK_ABOUT_CHANGE { get; set; }
        public string HOW_MANY_BILL_CAN_NAVIGATE { get; set; }
        public Nullable<bool> SUPPLIER_ITEM_CODE { get; set; }
        public Nullable<bool> CAN_SEE_THE_PRICE { get; set; }
        public Nullable<bool> UNPOSTED_BILL { get; set; }
        public Nullable<bool> GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_ENTRIES { get; set; }
        public Nullable<bool> GENERATE_THE_CURRENCY_DIFFERENCE_ENTRY_FOR_BILLS { get; set; }
        public string CURRENCYDIFFERENCEACCOUNTID { get; set; }
        public string LOSTACCOUNTID { get; set; }
        public Nullable<bool> CAN_CHANGE_BILL_DATE { get; set; }
        public Nullable<bool> CAN_SEE_THE_BILL_PROFIT { get; set; }
        public Nullable<bool> CAN_SEE_THE_CURRENT_QTY_ON_BILL { get; set; }
        public Nullable<bool> CAN_CHANGE_THE_PRICE_ON_THE_BILL { get; set; }
        public Nullable<bool> CAN_SEE_THE_CHEQUES_NOTIFICATIONS { get; set; }
        public string NUMBEROFDAYSBEFOREDUEDATE { get; set; }
        public Nullable<bool> LOAD_THE_LAST_BILLS_AFTER_SAVE { get; set; }
        public Nullable<bool> ISITORDEREDBYITEMCODEORNOT { get; set; }
        public bool DISABLE_BILL_PAY_TYPE { get; set; }
        public bool SHOW_BILL_PAY_TYPE { get; set; }
        public bool SHOW_AUTO_DISCOUNT { get; set; }
        public bool SHOW_IS_IT_RESERVATION { get; set; }
        public string RESETPRINTER_ID { get; set; }
        public Nullable<int> RESETTOPMARGIN { get; set; }
        public Nullable<int> RESETBOTTOMMARGIN { get; set; }
        public Nullable<int> RESETRIGHTMARGIN { get; set; }
        public Nullable<int> RESETLEFTMARGIN { get; set; }
        public string A4PRINTER_ID { get; set; }
        public Nullable<int> A4TOPMARGIN { get; set; }
        public Nullable<int> A4BOTTOMMARGIN { get; set; }
        public Nullable<int> A4RIGHTMARGIN { get; set; }
        public Nullable<int> A4LEFTMARGIN { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }

        public bool? IsItemCodeRelatedByGroup { get; set; }
        public string CodeSeparator { get; set; }

        public bool? IsShowPrices { get; set; }
        public bool? filterWithCompanyBranch { get; set; }
        public int? ReconstructionOfAssets { get; set; }
        public bool? DisplayPaym { get; set; }
        public bool? ApplyDueValueDate { get; set; }
        public bool? IncludePrevYear { get; set; }
        public bool? HighlightGenActs { get; set; }

    }
}
