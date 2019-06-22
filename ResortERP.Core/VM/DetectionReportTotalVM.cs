using System;

namespace ResortERP.Core.VM
{
    public class DetectionReportTotalVM
    {
        public Nullable<DateTime> BILL_DATE { get; set; }
        public Nullable<double> PurchasingItemWeightQTY { get; set; }
        public Nullable<double> PurchasingActualItemWeightQTY { get; set; }
        public Nullable<double> PurchasingTotalMustPaid { get; set; }
        public Nullable<double> PurchasingAveragepriceperkilogram { get; set; }
        public string PurchasingBILL_TYPE { get; set; }
        public Nullable<double> SalesItemWeightQTY { get; set; }
        public Nullable<double> SalesActualItemWeightQTY { get; set; }
        public Nullable<double> SalesTotalMustPaid { get; set; }
        public Nullable<double> SalesAveragepriceperkilogram { get; set; }
        public string SalesBILL_TYPE { get; set; }
        public string DetectionReport { get; set; }
    }
}
