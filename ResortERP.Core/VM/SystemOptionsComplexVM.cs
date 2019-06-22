using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core.VM
{
    public class SystemOptionsComplexVM
    {
        public System_OptionsVM SystemOptions { get; set; }
        public List<ShortcutsVM> shortcutList { get; set; }
        public List<EmailsVM> emailsList { get; set; }
        public UserVM userDataEntity { get; set; }
        public SystemOptionsPriceType priceTypeEntity { get; set; }
        public List<Kest_OptionVM> KestOptList { get; set; }
        public List<TBudgetAccountsVM> IncomeAccountTypesList { get; set; }
        //public List<Income_Account_TypesVM> IncomeAccountTypesList { get; set; }
        public List<TBoxAccountsVM> TBoxAccountsList { get; set; }
        public List<TStoreVM> TStoresList { get; set; }
        public AddressPartsSettingsVM addPartEntity { get; set; }
    }

    public class SystemOptionsPriceType
    {
        public string Price1 { get; set; }
        public string Price2 { get; set; }
        public string Price3 { get; set; }
        public string Price4 { get; set; }
        public string Price5 { get; set; }
        public string Price6 { get; set; }
        public string Price7 { get; set; }

        public string IsShowPrice1 { get; set; }
        public string IsShowPrice2 { get; set; }
        public string IsShowPrice3 { get; set; }
        public string IsShowPrice4 { get; set; }
        public string IsShowPrice5 { get; set; }
        public string IsShowPrice6 { get; set; }
        public string IsShowPrice7 { get; set; }
    }
}
