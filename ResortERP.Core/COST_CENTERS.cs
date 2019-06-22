using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class COST_CENTERS
    {
        [Key]
        public short COST_CENTER_ID { get; set; }
        public string COST_CENTER_CODE { get; set; }
        public string COST_CENTER_AR_NAME { get; set; }
        public string COST_CENTER_EN_NAME { get; set; }
        public Nullable<short> COST_CENTER_MASTER_ID { get; set; }
        public string COST_CENTER_REMARKS { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
        public Nullable<bool> Disable { get; set; }
        public int? OpeningExpenses { get; set; }
        public int? OpeningIncome { get; set; }
        public int? ExpectedExpenses { get; set; }
        public int? ExpectedIncome { get; set; }
    }
}
