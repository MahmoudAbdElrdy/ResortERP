using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
    public class BILL_DETAILSVM
    {
        public long BILL_ID { get; set; }
        public int ITEM_ID { get; set; }
        public short GRID_ROW_NUMBER { get; set; }
        public double QTY { get; set; }
        public Nullable<double> UNIT_PRICE { get; set; }
        public Nullable<short> UNIT_ID { get; set; }
        public string BILL_DETAILS_REMARKS { get; set; }
        public Nullable<short> EMP_ID { get; set; }
        public Nullable<double> GIFT { get; set; }
        public Nullable<System.DateTime> PRODUCTION_DATE { get; set; }
        public Nullable<System.DateTime> EXPIRED_DATE { get; set; }
        public Nullable<double> DISC_RATE { get; set; }
        public Nullable<double> DISC_VALUE { get; set; }
        public Nullable<double> EXTRA_RATE { get; set; }
        public Nullable<double> EXTRA_VALUE { get; set; }
        public Nullable<int> SERVICE_ID { get; set; }
        public Nullable<double> BILL_INVENTORY_VALUE { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public string Operation { get; set; }
        public string ITEM_AR_NAME { get; set; }
        public string UnitNameAr { get; set; }

        public string ARName { get; set; }
        public string LatName { get; set; }
        public double? ClearnessRate { get; set; }

        public int? CaliberID { get; set; }
        public double? ItemWeight { get; set; }
        public double? ManufacturingWages { get; set; }
        public double? itemGmWages { get; set; }

        public int? CostCenterID { get; set; }

        public bool? SubjectToVAT { get; set; }
        public double? VATRate { get; set; }
        public double? VatValue { get; set; }

        public string ITEM_CODE { get; set; }

        public double? WagesDiscValue { get; set; }
        public double? WagesDiscRate { get; set; }

        public int? ItemStatus { get; set; }

        public double? ActualItemWeight { get; set; }
        public double? TaxTotal { get; set; }

        public bool? IsExemptOfTax { get; set; }

    }
}
