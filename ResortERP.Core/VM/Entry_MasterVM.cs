using System;

namespace ResortERP.Core.VM
{
    public class Entry_MasterVM
    {
        public long ENTRY_ID { get; set; }
        public short ENTRY_SETTING_ID { get; set; }
        public string ENTRY_NUMBER { get; set; }
        public System.DateTime ENTRY_DATE { get; set; }
        public double ENTRY_CREDIT { get; set; }
        public double ENTRY_DEBIT { get; set; }

        public double ENTRY_GOLD_CREDIT { get; set; }
        public double ENTRY_GOLD_DEBIT { get; set; }
        public Nullable<int> CustAccID { get; set; }
        public Nullable<double> EntryVal { get; set; }

        public Nullable<int> ACC_ID { get; set; }
        public short CURRENCY_ID { get; set; }
        public double CURRENCY_RATE { get; set; }
        public string COLLECT_ENTRY_CODE { get; set; }
        public bool IS_POSTED { get; set; }
        public string CHECK_NUMBER { get; set; }
        public Nullable<System.DateTime> CHECK_DATE { get; set; }
        public Nullable<long> BILL_ID { get; set; }
        public string ENTRY_REMARKS { get; set; }
        public Nullable<short> EMP_ID { get; set; }
        public Nullable<int> for_kest { get; set; }
        public Nullable<bool> PAIED { get; set; }
        public Nullable<int> ENTRY_CURRENCY_DIFFERENCE { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public string AccountName { get; set; }
        public string EmpName { get; set; }
        public string CurrencyName { get; set; }
        public int? RelatedEntryID { get; set; }
        public Nullable<short> COM_BRN_ID { get; set; }
        public int? OpeningAccID { get; set; }
        public int? CustID { get; set; }

        public double? TotalCredit { get; set; }
        public double? TotalDepit { get; set; }
        public double? TotalGoldCredit24 { get; set; }
        public double? TotalGoldDepit24 { get; set; }
        public double? TotalGoldCredit22 { get; set; }
        public double? TotalGoldDepit22 { get; set; }
        public double? TotalGoldCredit21 { get; set; }
        public double? TotalGoldDepit21 { get; set; }
        public double? TotalGoldCredit18 { get; set; }
        public double? TotalGoldDepit18 { get; set; }
        public double? TotalCurrencyCredit { get; set; }
        public double? TotalCurrenctDepit { get; set; }
        public double? TotalCurGoldCredit24 { get; set; }
        public double? TotalCurGoldDepit24 { get; set; }
        public double? TotalCurGoldCredit22 { get; set; }
        public double? TotalCurGoldDepit22 { get; set; }
        public double? TotalCurGoldCredit21 { get; set; }
        public double? TotalCurGoldDepit21 { get; set; }
        public double? TotalCurGoldCredit18 { get; set; }
        public double? TotalCurGoldDepit18 { get; set; }
        public double? TotalTaxValueCredit { get; set; }
        public double? TotalTaxValueDebit { get; set; }
        public double? TotalTaxRateCredit { get; set; }
        public double? TotalTaxRateDebit { get; set; }
        public double? TotalNotTaxCredit { get; set; }
        public double? TotalNotTaxDebit { get; set; }


        public bool? Taxable { get; set; }
        public double? TaxValue { get; set; }
        public double? TaxRate { get; set; }

        public double? TotalCreditWithTax { get; set; }
        public double? TotalDepitWithTax { get; set; }

        public string EditReason { get; set; }
        public string DeliveryPersonName { get; set; }
        public string ExternalNumber { get; set; }



        //ضرائب
        public double? TotalCreditExemptOfTax { get; set; }
        public double? ExemptCreditOfTaxValue { get; set; }
        public double? TotalDepitExemptOfTax { get; set; }
        public double? ExemptDepitOfTaxValue { get; set; }
        public double? TotalCreditTotalMainVat { get; set; }
        public double? MainCreditVatValue { get; set; }
        public double? TotalDepitTotalMainVat { get; set; }
        public double? MainDepitVatValue { get; set; }
        public double? TotalCreditTotalZeroVat { get; set; }
        public double? ZeroCreditVatValue { get; set; }
        public double? TotalDepitTotalZeroVat { get; set; }
        public double? ZeroDepitVatValue { get; set; }
        public double? ExemptVatRate { get; set; }
        public double? MainVatRate { get; set; }
        public double? ZeroVatRate { get; set; }
        public double? TaxNumber { get; set; }
        
        public bool? IsLog { get; set; }

        public int? AssetOperationMasterID { get; set; }

    }
}
