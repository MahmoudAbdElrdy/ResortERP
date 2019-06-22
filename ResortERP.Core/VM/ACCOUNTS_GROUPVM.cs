using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
   public class ACCOUNTS_GROUPVM
    {
        [Key]
        public int GROUP_ID { get; set; }
        public string GROUP_CODE { get; set; }
        public string GROUP_AR_NAME { get; set; }
        public string GROUP_EN_NAME { get; set; }
        public int? GROUP_MASTER_ID { get; set; }
        public string GROUP_REMARKS { get; set; }
       
        public string AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime updatedOn { get; set; }
    }
}
