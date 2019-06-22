using System;

namespace ResortERP.Core.VM
{
    public class LoginVM
    {
        public int? UserID { get; set; }
        public int? TraderID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? TraderTypeID { get; set; }
        public int? TraderTypeNumber { get; set; }
        public int? UserTypeID { get; set; }
        public int? CurrencyID { get; set; }
        public int? CountryID { get; set; }
        public string CurrencyName { get; set; }
        public double? CurrencyRate { get; set; }
        public Boolean? AllowRegister { get; set; }
        public int? AccountID { get; set; }
        public int? Lang { get; set; }
        public int? CodeStatus { get; set; }
        public Boolean? Active { get; set; }
        public int? LanguageID { get; set; }
        public int? DateTypeID { get; set; }
        public string ActivationCode { get; set; }
    }
}
