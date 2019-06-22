using System;

namespace ResortERP.Core.VM
{
    public class Entry_SettingsVM
    {
        public short ENTRY_SETTING_ID { get; set; }
        public string ENTRY_SETTING_AR_NAME { get; set; }
        public string ENTRY_SETTING_EN_NAME { get; set; }
        public string ENTRY_SETTING_AR_ABRV { get; set; }
        public string ENTRY_SETTING_EN_ABRV { get; set; }
        public Nullable<int> ENTRY_ACC_ID { get; set; }
        public short CURRENCY_ID { get; set; }
        public bool AUTO_POSTING { get; set; }
        public byte ENTRY_TYPE_ID { get; set; }
        public bool ACCEPT_DIST_ACC { get; set; }
        public Nullable<bool> COSTCENTER_BELONG { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public int? TaxAccountID { get; set; }

        public int? MenuID { get; set; }

        public bool? IsTaxable { get; set; }
        public double? TaxRate { get; set; }

        public bool? IsEnableTaxEdit { get; set; }

        public int? TimesNumberOfPrinting { get; set; }
        public int? EntryModeID { get; set; }


        public bool? IsTaxesIncluded { get; set; }

        public bool? IsRepeatItem { get; set; }
        public bool? IsQuickAccount { get; set; }
        public bool? ShowEntryTotalsSammaryAsTable { get; set; }


        public double? ExemptVatRate { get; set; }
        public double? MainVatRate { get; set; }
        public bool? IsShowTaxesBox { get; set; }

        public bool? IsTaxAccForAccount { get; set; }
    }
}
