using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Core
{
    public class ITEMS_GROUPS
    {
        [Key]
        public int GROUP_ID { get; set; }
        public string GROUP_CODE { get; set; }
        public string GROUP_AR_NAME { get; set; }
        public string GROUP_EN_NAME { get; set; }
        public int?  GROUP_MASTER_ID { get; set; }
        public string GROUP_REMARKS { get; set; }
        public bool GROUP_APPEAREONTHESALEPOINTBILL { get; set; }
        public int? CaliberID { get; set; }

        public bool DOESTHEQUANTITYISAPARTOFBARCODE { get; set; }
        public double QUANTITYLENGTHATTHEBARCODE { get; set; }
        public double QUANTITYSTARTATTHEBARCODE { get; set; }

        public double QUANTITYPARTSTARTATTHEBARCODE { get; set; }
        public double QUANTITYPARTLENGTHATTHEBARCODE { get; set; }

        public int? COST_CALCULATION_TYPE { get; set; }
        public int? ItemStatusID { get; set; }

        public int? GoldAccID { get; set; }
    }

}
