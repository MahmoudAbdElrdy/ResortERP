using System;
using System.Collections.Generic;

namespace ResortERP.Core
{
    public partial class HrPslEmployeeTrainingCources
    {
        public int HrPslEmployeeTrainingCourcesID { get; set; }
        public int HrPslEmployeeID { get; set; }
        public string CourseName { get; set; }
        public string CenterName { get; set; }
        public string Grade { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
