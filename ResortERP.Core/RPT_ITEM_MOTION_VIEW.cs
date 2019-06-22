using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class RPT_ITEM_MOTION_VIEW
    {
        [Key]
        public Nullable<long> BILL_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_AR_NAME { get; set; }
        public string ITEM_EN_NAME { get; set; }
        public Nullable<int> ITEM_ID { get; set; }
        public Nullable<double> QTY { get; set; }
        public Nullable<double> UNIT_PRICE { get; set; }
        public Nullable<double> GIFT { get; set; }
        public string UNIT_AR_NAME { get; set; }
        public string UNIT_CODE { get; set; }
        public string UNIT_EN_NAME { get; set; }
        public string EMP_CODE { get; set; }
        public string EMP_AR_NAME { get; set; }
        public string EMP_EN_NAME { get; set; }
        public Nullable<DateTime> BILL_DATE { get; set; }
        public string BILL_NUMBER { get; set; }
        public string BILL_AR_NAME { get; set; }
        public string BILL_EN_NAME { get; set; }
        public string BILL_ABRV_AR { get; set; }
        public string BILL_ABRV_EN { get; set; }
        public Nullable<Byte> BILL_TYPE { get; set; }
        public Nullable<double> UNIT_TRANS_RATE { get; set; }
        public Nullable<short> BILL_SETTING_ID { get; set; }
        public string BILL_REMARKS { get; set; }
        public Nullable<double> EXTRA_VALUE { get; set; }
        public Nullable<double> EXTRA_RATE { get; set; }
        public Nullable<double> DISC_VALUE { get; set; }
        public Nullable<double> DISC_RATE { get; set; }
        public string COM_STORE_CODE { get; set; }
        public string COM_STORE_AR_NAME { get; set; }
        public string COM_STORE_EN_NAME { get; set; }
        public Nullable<short> COM_STORE_ID { get; set; }
        public Nullable<short> COST_CENTER_ID { get; set; }
        public Nullable<short> EMP_ID { get; set; }
        public Nullable<double> BILL_TOTAL_EXTRA { get; set; }
        public Nullable<double> BILL_TOTAL_DISC { get; set; }
        public string BILL_PAY_WAY { get; set; }
        public Nullable<double> BILL_TOTAL { get; set; }
        public Nullable<int> CUST_ACC_ID { get; set; }
        public string ACC_AR_NAME { get; set; }
        public string ACC_CODE { get; set; }
        public string ACC_EN_NAME { get; set; }
        public Nullable<bool> BILL_IS_POST { get; set; }
        public Nullable<short> GRID_ROW_NUMBER { get; set; }
        public Nullable<short> TO_COM_STORE_ID { get; set; }
        public Nullable<int> GROUP_ID { get; set; }
        public Nullable<short> UNIT_ID { get; set; }
    }
}
