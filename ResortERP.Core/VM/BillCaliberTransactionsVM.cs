using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResortERP.Core.VM
{
    public class BillCaliberTransactionsVM
    {
        [Key]
        public int CaliberTransID { get; set; }
        public string ColName { get; set; }
        public double? ColQuantity { get; set; }
        public double? Col24 { get; set; }
        public double? Col22 { get; set; }
        public double? Col21 { get; set; }
        public double? Col18 { get; set; }
        public int BillMasterID { get; set; }

        public double? TransQuant { get; set; }

    }
}
