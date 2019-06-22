using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslCountry
    {
        //public HrPslCountry()
        //{
        //    HrPslCity = new HashSet<HrPslCity>();
        //    HrPslEmployee = new HashSet<HrPslEmployee>();
        //    HrPslUniversity = new HashSet<HrPslUniversity>();
        //}

        public int HrPslCountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryCode { get; set; }

        //public HrPslNationality HrPslNationality { get; set; }
        //public ICollection<HrPslCity> HrPslCity { get; set; }
        //public ICollection<HrPslEmployee> HrPslEmployee { get; set; }
        //public ICollection<HrPslUniversity> HrPslUniversity { get; set; }
    }
}
