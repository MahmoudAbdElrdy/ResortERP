using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class BILL_MASTER
    {
        [Key]
        public long BILL_ID { get; set; }
        public short BILL_SETTING_ID { get; set; }
        public System.DateTime BILL_DATE { get; set; }
        public string BILL_NUMBER { get; set; }
        public Nullable<int> CUST_ACC_ID { get; set; }
        public Nullable<int> GOLD_ACC_ID { get; set; }
        public Nullable<int> ITEM_ACC_ID { get; set; }
        public Nullable<short> CURRENCY_ID { get; set; }
        public Nullable<double> CURRENCY_RATE { get; set; }
        public string BILL_REMARKS { get; set; }
        public Nullable<double> BILL_TOTAL { get; set; }
        public Nullable<int> CUST_BRN_ID { get; set; }
        public string BILL_PAY_WAY { get; set; }
        public Nullable<double> BILL_TOTAL_DISC { get; set; }
        public Nullable<double> BILL_TOTAL_EXTRA { get; set; }
        public Nullable<double> BILL_PERCENTAGE_DISC { get; set; }
        public Nullable<short> EMP_ID { get; set; }
        public Nullable<short> COST_CENTER_ID { get; set; }
        public Nullable<short> COM_STORE_ID { get; set; }
        public Nullable<short> TO_COM_STORE_ID { get; set; }
        public Nullable<short> COM_BRN_ID { get; set; }
        public bool BILL_IS_POST { get; set; }
        public Nullable<byte> SELL_TYPE_ID { get; set; }
        public Nullable<long> ENTRY_ID { get; set; }
        public Nullable<int> SHIFT_NUMBER { get; set; }
        public Nullable<double> BILL_WIEGHT { get; set; }
        public Nullable<byte> BILL_TYPE { get; set; }
        public Nullable<double> BILL_PAIDED { get; set; }
        public Nullable<double> kest_value { get; set; }
        public string WORK_ORDER_NUMBER { get; set; }
        public string COUNTER_VALUE { get; set; }
        public Nullable<double> THE_RETURN_PERCENTAGE { get; set; }
        public Nullable<System.DateTime> THE_LAST_DATE_FOR_RETURN { get; set; }
        public Nullable<double> BILL_ANOTHER_DISC { get; set; }
        public Nullable<bool> IS_IT_RESERVATION { get; set; }
        public Nullable<double> BILLCURRENTPAIDEDVALUE { get; set; }
        public Nullable<double> BILLCURRENTREMAINVALUE { get; set; }
        public Nullable<int> PAY_ACC_ID { get; set; }
        public Nullable<double> BILL_COST { get; set; }
        public Nullable<double> BILL_PROFIT { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }

        public int? CostCenterID { get; set; }

        public double? TotalWeight { get; set; }
        public double? Total24Quantity { get; set; }
        public double? Total22Quantity { get; set; }
        public double? Total21Quantity { get; set; }
        public double? Total18Quantity { get; set; }
        public double? TotalItemWages { get; set; }
        public double? TotalManufactWages { get; set; }
        public double? TotalPaid { get; set; }
        public double? TotalQuantity { get; set; }
        public double? TotalPrice { get; set; }
        public double? TotalMustPaid { get; set; }
        public double? TotalRemaining { get; set; }
        public double? TotalVatRate { get; set; }

        public double? TransTotalGold24 { get; set; }
        public double? TransTotalGold22 { get; set; }
        public double? TransTotalGold21 { get; set; }
        public double? TransTotalGold18 { get; set; }
        public double? TransTotalGoldQuantity { get; set; }

        public double? TotalDiscPercentage { get; set; }
        public double? TotalDisc { get; set; }
        public double? TotalAllDiscPercentage { get; set; }
        public double? TotalAllDisc { get; set; }
        public double? TotalVatValue { get; set; }


        public double? TotalBeforeDiscount { get; set; }
        public double? TotalAfterDiscount { get; set; }

        public double? TotalWagesDiscValue { get; set; }
        public double? TotalWagesDiscRate { get; set; }

        public string EditReason { get; set; }
        public string DeliveryPersonName { get; set; }

        public string ExternalNumber { get; set; }



        public double? TotalExemptOfTax { get; set; }
        public double? ExemptOfTaxRate { get; set; }
        public double? TotalMainVat { get; set; }
        public double? MainVatRate { get; set; }
        public double? TotalZeroVat { get; set; }
        public double? ZeroVatRate { get; set; }

        public double? ExemptOfTaxValue { get; set; }
        public double? MainVatValue { get; set; }
        public double? ZeroVatValue { get; set; }
        public double? TotalBeforeTax { get; set; }
        public double? TotalAfterTax { get; set; }
        public double? TotalTaxableAmount { get; set; }
        public double? TotalNotTaxableAmount { get; set; }
        public double? DiscountAmount { get; set; }
        
    }
}
