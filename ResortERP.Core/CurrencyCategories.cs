using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
   public class CurrencyCategories
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryArName { get; set; }
        public string CategoryEnName { get; set; }
        public int? CurrencyID { get; set; }
        public string AddedBy { get; set; }
        public DateTime? AddenOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool Disable { get; set; }
        public int? CategoryTrans { get; set; }
    }
}
