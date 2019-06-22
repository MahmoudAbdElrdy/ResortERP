using System;

namespace ResortERP.Core.VM
{
    public class InvoiceMotionViewVM
    {
        public Nullable<long> BILL_ID { get; set; }
        public Nullable<short> BILL_SETTING_ID { get; set; }
        public Nullable<DateTime> BILL_DATE { get; set; }
        public string BILL_NUMBER { get; set; }
        public string BILL_REMARKS { get; set; }
        public Nullable<double> BILL_TOTAL_DISC { get; set; }
        public string BILL_PAY_WAY { get; set; }
        public Nullable<double> BILL_TOTAL_EXTRA { get; set; }
        public Nullable<double> BILL_TOTAL { get; set; }
        public Nullable<bool> BILL_IS_POST { get; set; }
        public Nullable<Byte> BILL_TYPE { get; set; }
        public string COM_STORE_CODE { get; set; }
        public string COM_STORE_AR_NAME { get; set; }
        public string COM_STORE_EN_NAME { get; set; }
        public string BILL_AR_NAME { get; set; }
        public string BILL_EN_NAME { get; set; }
        public string BILL_ABRV_AR { get; set; }
        public string BILL_ABRV_EN { get; set; }
        public string ACC_AR_NAME { get; set; }
        public string ACC_CODE { get; set; }
        public string ACC_EN_NAME { get; set; }
        public Nullable<int> CUST_ACC_ID { get; set; }
        public Nullable<short> COM_STORE_ID { get; set; }
        public Nullable<short> COST_CENTER_ID { get; set; }
        public Nullable<short> EMP_ID { get; set; }
        public Nullable<double> BILL_PAIDED { get; set; }
        public Nullable<long> cust_account_id { get; set; }
        public Nullable<double> kest_value { get; set; }
        public Nullable<int> SHIFT_NUMBER { get; set; }
        public string ACC_CUSTOMER_CODE { get; set; }
        public Nullable<double> TotalPaid { get; set; }
        public Nullable<double> TotalMustPaid { get; set; }
        public Nullable<double> TotalRemaining { get; set; }
        public Nullable<short> COM_BRN_ID { get; set; }
        public string COM_BRN_AR_NAME { get; set; }
    }
}
