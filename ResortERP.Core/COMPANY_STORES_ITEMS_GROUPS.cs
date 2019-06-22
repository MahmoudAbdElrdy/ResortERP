using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class COMPANY_STORES_ITEMS_GROUPS
    {
        [Key]
        public int CSIG_ID { get; set; }
        public int COM_STORE_ID { get; set; }
        public int GROUP_ID { get; set; }
    }
}
