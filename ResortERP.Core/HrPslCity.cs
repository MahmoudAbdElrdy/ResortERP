using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslCity
    {
        //public HrPslCity()
        //{
        //    HrPslEmployee = new HashSet<HrPslEmployee>();
        //    HrPslUniversity = new HashSet<HrPslUniversity>();
        //}

        public int HrPslCityID { get; set; }
        public string CityName { get; set; }
        public string CityNameEn { get; set; }
        public string CityCode { get; set; }
        public int HrPslCountryID { get; set; }

        //public HrPslCountry HrPslCountry { get; set; }
        //public ICollection<HrPslEmployee> HrPslEmployee { get; set; }
        //public ICollection<HrPslUniversity> HrPslUniversity { get; set; }
    }
}
