using System;

namespace ResortERP.Core.VM
{
    public class Entry_DetailsVM
    {
        public long ENTRY_ID { get; set; }
        public byte ENTRY_ROW_NUMBER { get; set; }
        public int ACC_ID { get; set; }
        public double ENTRY_CREDIT { get; set; }
        public double ENTRY_DEBIT { get; set; }

        public double ENTRY_GOLD24_CREDIT { get; set; }
        public double ENTRY_GOLD24_DEBIT { get; set; }
        public string ENTRY_DETAILS_REMARKS { get; set; }
        public Nullable<short> COST_CENTER_ID { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public string ACC_AR_NAME { get; set; }
        public bool? Taxable { get; set; }
        public double? TaxValue { get; set; }
        public string ACC_CODE { get; set; }

        public double? ENTRY_GOLD22_CREDIT { get; set; }
        public double? ENTRY_GOLD22_DEBIT { get; set; }

        public double? ENTRY_GOLD21_CREDIT { get; set; }
        public double? ENTRY_GOLD21_DEBIT { get; set; }

        public double? ENTRY_GOLD18_CREDIT { get; set; }
        public double? ENTRY_GOLD18_DEBIT { get; set; }

        public double? TaxRate { get; set; }

        public string CheckNumber { get; set; }
        public DateTime? CheckDate { get; set; }
        public DateTime? CheckIssueDate { get; set; }

        public string Parent_ACC_AR_NAME { get; set; }

        public bool? IsExemptOfTax { get; set; }

        public double? ExemptOfTaxValue { get; set; }
        public bool? IsMainVatValue { get; set; }
        public double? MainVatValue { get; set; }

        public bool? IsZeroVatValue { get; set; }
        public double? ZeroVatValue { get; set; }

        public double? MainVat { get; set; }
    }
}
