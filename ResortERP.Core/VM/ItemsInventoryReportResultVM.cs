using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolversTeamERP.Core.VM
{
    public class ItemsInventoryReportResultVM
    {
        public List<ItemsInventoryReportGroupsResultVM> InputsOnGroups { get; set; }
        public List<ItemsInventoryReportGroupsResultVM> OutputOnGroups { get; set; }
        public List<ItemsInventoryReportGroupsResultVM> TransferOnGroups { get; set; }
        public List<ItemsInventoryReportGroupsResultVM> Inputs { get; set; }
        public List<ItemsInventoryReportGroupsResultVM> Outputs { get; set; }
        public List<ItemsInventoryReportGroupsResultVM> Transfer { get; set; }
        public List<ItemsInventoryReportGroupsResultVM> EmptyItems { get; set; }
        public List<ItemsInventoryReportGroupsResultVM> EmptyItemsOnGroups { get; set; }
        public List<ItemsInventoryReportGroupsResultVM> ItemsBalanceOnGroup { get; set; }
        public List<ItemsInventoryReportBalanceResultVM> ItemsBalance { get; set; }
        public ItemsInventoryReportTotalPricesVM TotalPrices { get; set; }
    }

    public class ItemsInventoryReportGroupsResultVM
    {
        public int GROUP_ID { get; set; }
        public string GROUP_CODE { get; set; }
        public string GROUP_AR_NAME { get; set; }
        public string GROUP_EN_NAME { get; set; }
        public double? QTY { get; set; }

        public string ITEM_CODE { get; set; }
        public string ITEM_AR_NAME { get; set; }
        public string ITEM_EN_NAME { get; set; }
        public DateTime? EXPIRED_DATE { get; set; }
        public int? COM_STORE_ID { get; set; }
        public int? TO_COM_STORE_ID { get; set; }
    }

    public class ItemsInventoryReportBalanceResultVM
    {
        public int ITEM_ID { get; set; }
        public double? QTY { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_AR_NAME { get; set; }
        public string ITEM_EN_NAME { get; set; }
        public double? WHOLE_PRICE { get; set; }
        public double? HALF_WHOLE_PRICE { get; set; }
        public double? EMP_PRICE { get; set; }
        public double? EXPORT_PRICE { get; set; }
        public double? RETAIL_PRICE { get; set; }
        public double? CONSUMER_PRICE { get; set; }
        public double? LAST_BUY_PRICE { get; set; }
        public double? UNIT_TRANS_RATE { get; set; }
        public DateTime? BILL_DATE { get; set; }
        public double? GIFT { get; set; }
        public double? BILL_TYPE { get; set; }
        public int? GROUP_ID { get; set; }
        public DateTime? EXPIRED_DATE { get; set; }
        public int? COM_STORE_ID { get; set; }
        public int? COST_CENTER_ID { get; set; }
        public double? MIN_QTY { get; set; }
        public string DEFAULT_UNIT_AR_NAME { get; set; }
        public int? DONE { get; set; }
        public int? TO_COM_STORE_ID { get; set; }
        public double? WHOLE_PRICEQTY { get; set; }
        public double? HALF_WHOLE_PRICEQTY { get; set; }
        public double? EMP_PRICEQTY { get; set; }
        public double? EXPORT_PRICEQTY { get; set; }
        public double? RETAIL_PRICEQTY { get; set; }
        public double? CONSUMER_PRICEQTY { get; set; }
        public double? LAST_BUY_PRICEQTY { get; set; }
    }

    public class ItemsInventoryReportPricesVM
    {
        public int ITEM_ID { get; set; }
        public string ITEM_CODE { get; set; }
        public double? WHOLE_PRICE { get; set; }
        public double? QTY { get; set; }
        public double? WHOLE_PRICEQTY { get; set; }
        public double? HALF_WHOLE_PRICEQTY { get; set; }
        public double? EMP_PRICEQTY { get; set; }
        public double? EXPORT_PRICEQTY { get; set; }
        public double? RETAIL_PRICEQTY { get; set; }
        public double? CONSUMER_PRICEQTY { get; set; }
        public double? LAST_BUY_PRICEQTY { get; set; }
    }

    public class ItemsInventoryReportTotalPricesVM
    {
        public double WHOLE_PRICE { get; set; }
        public double HALF_WHOLE_PRICE { get; set; }
        public double EMP_PRICE { get; set; }
        public double EXPORT_PRICE { get; set; }
        public double RETAIL_PRICE { get; set; }
        public double CONSUMER_PRICE { get; set; }
        public double LAST_BUY_PRICE { get; set; }
    }
}
