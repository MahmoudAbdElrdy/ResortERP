using System;

namespace ResortERP.Core.VM
{
    public class AccountVM
    {
        public int ACC_ID { get; set; }
        public string ACC_CODE { get; set; }
        public string ACC_AR_NAME { get; set; }
        public string ACC_EN_NAME { get; set; }
        public int? GROUP_ID { get; set; }
        public byte ACC_TYPE_ID { get; set; }
        public System.DateTime ACC_DATE { get; set; }
        public System.DateTime? ACC_CHECK_DATE { get; set; }
        public byte ACC_STATE { get; set; }
        public double ACC_CREDIT { get; set; }
        public double ACC_DEBIT { get; set; }
        public Nullable<double> ACC_MAX_DEBIT { get; set; }
        public Nullable<double> ACC_MAX_CREDIT { get; set; }
        public short CURRENCY_ID { get; set; }
        public Nullable<int> PARENT_ACC_ID { get; set; }
        public Nullable<int> FINAL_ACC_ID { get; set; }
        public Nullable<int> ACC_LEVEL { get; set; }
        public string ACC_REMARKS { get; set; }
        public short ACC_NSONS { get; set; }
        public string ACC_TURNNING_YES_OR_NO { get; set; }
        public byte ACC_DEBIT_OR_CREDIT_OR_WITHOUT { get; set; }
        public string ACC_CUSTOMER_CODE { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Deactivate { get; set; }
        public double? VATRate { get; set; }
        public bool? SubjectToVAT { get; set; }

        public double? CreditOpeningAccount { get; set; }
        public double? DepitOpeningAccount { get; set; }

        public string SonCode { get; set; }

        public int? Company_Branch_ID { get; set; }

        public int? TaxAccountID { get; set; }
    }
}
