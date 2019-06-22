using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core
{
    public class AssetDepreciationDetailsVM
    {
        public AssetDepreciationDetailsVM()
        {
            Active = true;
            Position = 1;
        }
        public int ID { get; set; }
        public int AssetMasterID { get; set; }
        public int? AssetDepreciationTypeID { get; set; }
        public bool? NotSubjectToRevaluation { get; set; }
        public bool? NotSubjectToDepreciation { get; set; }
        public double? AssetLifeSpan { get; set; }
        public int? AssetLifeSpanUnitID { get; set; }
        public DateTime? DepreciationStartDate { get; set; }
        public double? AssetScrapValue { get; set; }
        public double? InitialAssetScrapValue { get; set; }
        public double? ExtraValue { get; set; }
        public double? ExclusionsValue { get; set; }
        public double? DepreciationTotals { get; set; }
        public double? CurrentAssetValue { get; set; }
        public int? CurrencyID { get; set; }
        public double? CurrencyRate { get; set; }
        public string Notes { get; set; }
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Active { get; set; }
        public int Position { get; set; }
    }
}
