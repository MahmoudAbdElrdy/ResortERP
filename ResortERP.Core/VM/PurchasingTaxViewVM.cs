using System;

namespace ResortERP.Core.VM
{
    public class PurchasingTaxViewVM
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
        public string EMP_CODE { get; set; }
        public string EMP_AR_NAME { get; set; }
        public string EMP_EN_NAME { get; set; }
        public string BILL_ABRV_AR { get; set; }
        public string BILL_ABRV_EN { get; set; }
        public string ACC_AR_NAME { get; set; }
        public string ACC_CODE { get; set; }
        public string ACC_EN_NAME { get; set; }
        public Nullable<int> CUST_ACC_ID { get; set; }
        public Nullable<short> COM_STORE_ID { get; set; }
        public Nullable<short> COST_CENTER_ID { get; set; }
        public Nullable<short> EMP_ID { get; set; }
        public string BILL_AR_NAME { get; set; }
        public string BILL_EN_NAME { get; set; }
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
        public Nullable<double> TransTotalGold24 { get; set; }
        public Nullable<double> TransTotalGold22 { get; set; }
        public Nullable<double> TransTotalGold21 { get; set; }
        public Nullable<double> TransTotalGold18 { get; set; }
        public Nullable<double> TransTotalGoldQuantity { get; set; }
        public Nullable<double> TotalDiscPercentage { get; set; }
        public Nullable<double> TotalDisc { get; set; }
        public Nullable<double> TotalAllDiscPercentage { get; set; }
        public Nullable<double> TotalAllDisc { get; set; }
        public Nullable<double> TotalVatValue { get; set; }
        public Nullable<double> TotalBeforeDiscount { get; set; }
        public Nullable<double> TotalAfterDiscount { get; set; }
        public Nullable<double> TotalWagesDiscValue { get; set; }
        public Nullable<double> TotalWagesDiscRate { get; set; }
        public string DeliveryPersonName { get; set; }
        public Nullable<double> TotalBeforeTax { get; set; }
        public Nullable<double> TotalAfterTax { get; set; }
        public Nullable<double> TotalVatRate { get; set; }
        public Nullable<double> TotalExemptOfTax { get; set; }
        public Nullable<double> ExemptOfTaxRate { get; set; }
        public Nullable<double> ExemptOfTaxValue { get; set; }        
        public Nullable<double> TotalMainVat { get; set; }
        public Nullable<double> MainVatRate { get; set; }
        public Nullable<double> MainVatValue { get; set; }    
        public Nullable<double> TotalZeroVat { get; set; }
        public Nullable<double> ZeroVatValue { get; set; }
        public Nullable<double> ZeroVatRate { get; set; }
        public Nullable<byte> BILL_TYPE_ID { get; set; }
        public Nullable<double> TotalNotTaxableAmount { get; set; }
    }
}
