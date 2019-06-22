using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslNationality
    {
        //public HrPslNationality()
        //{
        //    HrPslEmployee = new HashSet<HrPslEmployee>();
        //}

        public int HrPslNationalityID { get; set; }
        public string NationalityCode { get; set; }
        public string NationalityName { get; set; }
        public string NationalityNameEng { get; set; }
        public int HrPslCountryID { get; set; }

        //public HrPslCountry HrPslCountry { get; set; }
        //public ICollection<HrPslEmployee> HrPslEmployee { get; set; }
    }
}
