using System;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class Currency
    {
        [Key]
        public short CURRENCY_ID { get; set; }
        public string CURRENCY_CODE { get; set; }
        public string CURRENCY_AR_NAME { get; set; }
        public string CURRENCY_EN_NAME { get; set; }
        public string CURRENCY_SUB_AR_NAME { get; set; }
        public string CURRENCY_SUB_EN_NAME { get; set; }
        public double CURRENCY_RATE { get; set; }
        public short SUB_TO_CURRENCY_TRANS { get; set; }
        public Nullable<double> CURRENCY_FIXED_RATE { get; set; }
        public string CURRENCY_SYMBOL_AR_NAME { get; set; }
        public string CURRENCY_SYMBOL_EN_NAME { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public Nullable<bool> DefaultCurrency { get; set; }
    }
}
