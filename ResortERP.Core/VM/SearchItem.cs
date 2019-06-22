using System.Collections.Generic;

namespace ResortERP.Core.VM
{
    public class SearchItem
    {
        public SearchingmainItems searchItm { get; set; }
        public List<BILL_SETTINGSVM> bilSettings { get; set; }
        public List<ReportOptions> rptOptions { get; set; }

    }
}
